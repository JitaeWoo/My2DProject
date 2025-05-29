using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerStats _statsTemplate;

    public PlayerStats Stats { get; private set; }
    public Transform Transform { get; private set; }

    // IsWall, IsGround 판정에 필요한 변수들
    private PlayerInput _input = new PlayerInput();
    private LayerMask _groundLayerMask;
    private Collider2D[] _cols = new Collider2D[10];

    private void Awake()
    {
        Stats = Instantiate(_statsTemplate);
        _groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
    }

    private void Update()
    {
        if(Transform != null)
        {
            Stats.IsGround.Value = CheckIsGround();
            Stats.IsWall.Value = CheckIsWall();
        }
    }

    public void SetTransform(Transform transfrom)
    {
        Transform = transfrom;
    }
    private bool CheckIsGround()
    {
        Vector2 point = Transform.position;
        Vector2 range = new Vector2(Transform.localScale.x - 0.1f, 0.1f);

        int count = Physics2D.OverlapBoxNonAlloc(point, range, 0f, _cols, _groundLayerMask);

        if (count > 0)
        {
            return true;
        }

        return false;
    }

    private bool CheckIsWall()
    {
        Vector2 point = (Vector2)Transform.position + (Vector2.up * (Transform.localScale.y * 2 - 0.2f));
        Vector2 range = new Vector2(Transform.localScale.x, 0.1f);

        int count = Physics2D.OverlapBoxNonAlloc(point, range, 0f, _cols, _groundLayerMask);

        if (count > 0 && _input.MoveInput().x != 0)
        {
            return true;
        }

        return false;
    }
}
