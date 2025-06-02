using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerState_Jump : PlayerState
{
    private PlayerStats _stats => Manager.Player.Stats;
    private Transform _playerTransform => Manager.Player.Transform;
    private PlayerMovement _movement;
    private PlayerInput _input = new PlayerInput();

    private Collider2D[] _cols = new Collider2D[10];
    private LayerMask _wallLayerMask;
    private float _stateChangeDelay;

    public PlayerState_Jump(StateMachine stateMachine) : base(stateMachine)
    {
        _movement = Manager.Player.Transform.GetComponent<PlayerMovement>();
        _wallLayerMask = (1 << LayerMask.NameToLayer("Wall"));
    }

    public override void Enter()
    {
        if(_stats.CurJumpCount == 0)
        {
            _stats.CurJumpCount++;
        }
        _stateChangeDelay = 0.05f;
    }

    public override void Update()
    {
        if (_input.JumpInput() && _stats.CurJumpCount < _stats.MaxJumpCount)
        {
            _movement.Jump();
            _stats.CurJumpCount++;
        }

        if (_stateChangeDelay > 0)
        {
            _stateChangeDelay -= Time.deltaTime;
            return;
        }

        if (_stats.IsGround.Value && _stats.Velocity.y < 0.01f)
        {
            StateMachine.ChangeState("Idle");
        }
        else if (CheckIsWall())
        {
            StateMachine.ChangeState("StayWall");
        }
    }

    private bool CheckIsWall()
    {
        Vector2 point = (Vector2)_playerTransform.position + (Vector2.up * (_playerTransform.localScale.y * 2 - 0.2f));
        Vector2 range = new Vector2(_playerTransform.localScale.x, 0.1f);

        int count = Physics2D.OverlapBoxNonAlloc(point, range, 0f, _cols, _wallLayerMask);

        if (count <= 0 || _stats.IsDash.Value) return false;

        _stats.IsWallLaft.Value = Physics2D.Raycast(point, Vector2.left, _playerTransform.localScale.x / 2 + 0.1f, _wallLayerMask);

        float x = _stats.IsWallLaft.Value ? -1 : 1;

        if (_stats.MoveInput.Value.x != x) return false;

        return true;
    }
}
