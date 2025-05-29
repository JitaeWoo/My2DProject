using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public Stat<bool> IsJump = new();

    public Vector2 Velocity;

    public float MoveSpeed;
    public float JumpPower;

    public bool IsGround => CheckIsGround();
    private LayerMask _groundLayerMask;
    private Collider2D[] _cols = new Collider2D[10];
    

    private void Awake()
    {
        // LayerMask.NameToLayer�� ScriptableObject���� ����ÿ� �ҷ��� �� �����Ƿ�
        // Awake���� �����Ͽ����ϴ�.
        if (Application.isPlaying)
        {
            _groundLayerMask = 1 << LayerMask.NameToLayer("Ground");
        }
    }

    private bool CheckIsGround()
    {
        Vector2 point = Manager.Player.Transform.position;
        Vector2 range = new Vector2(Manager.Player.Transform.localScale.x - 0.1f, 0.1f);

        int count = Physics2D.OverlapBoxNonAlloc(point, range, 0f, _cols, _groundLayerMask);

        if (count > 0)
        {
            return true;
        }

        return false;
    }
}
