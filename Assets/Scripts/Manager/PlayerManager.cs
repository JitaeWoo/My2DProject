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
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _wallLayerMask;
    private Collider2D[] _cols = new Collider2D[10];

    private void Awake()
    {
        Stats = Instantiate(_statsTemplate);
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

    public void ReturnSafePosition()
    {
        Transform.position = Stats.LastSafePosition.Value;
    }

    private bool CheckIsGround()
    {
        Vector2 point = Transform.position;
        Vector2 range = new Vector2(Transform.localScale.x, 0.2f);

        int count = Physics2D.OverlapBoxNonAlloc(point, range, 0f, _cols, _groundLayerMask);

        if (count > 0)
        {
            if (IsSafe())
            {
                Stats.LastSafePosition.Value = Transform.position;
            }
            return true;
        }

        return false;
    }

    private bool IsSafe()
    {
        float scaleX = Transform.localScale.x;
        Vector2 laftPoint = Transform.position;
        laftPoint.x -= scaleX / 2;
        Vector2 rightPoint = Transform.position;
        rightPoint.x += scaleX / 2;

        bool checkLaft = Physics2D.Raycast(laftPoint, Vector2.down, 0.1f, _groundLayerMask);
        bool checkRight = Physics2D.Raycast(rightPoint, Vector2.down, 0.1f, _groundLayerMask);

        return checkLaft && checkRight;
    }

    private bool CheckIsWall()
    {
        Vector2 point = (Vector2)Transform.position + (Vector2.up * (Transform.localScale.y * 2 - 0.2f));
        Vector2 range = new Vector2(Transform.localScale.x, 0.1f);

        int count = Physics2D.OverlapBoxNonAlloc(point, range, 0f, _cols, _wallLayerMask);

        if (count > 0 && _input.MoveInput().x != 0 && Stats.IsJump.Value && !Stats.IsDash.Value)
        {
            Stats.IsWallLaft.Value = Physics2D.Raycast(point, Vector2.left, Transform.localScale.x / 2 + 0.1f, _wallLayerMask);

            return true;
        }

        return false;
    }
}
