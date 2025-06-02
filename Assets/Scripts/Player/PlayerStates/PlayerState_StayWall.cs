using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_StayWall : PlayerState
{
    private PlayerStats _stats => Manager.Player.Stats;
    private PlayerInput _input;
    private PlayerMovement _movement;

    public PlayerState_StayWall(StateMachine stateMachine) : base(stateMachine)
    {
        _input = new PlayerInput();
        _movement = Manager.Player.Transform.GetComponent<PlayerMovement>();
    }

    public override void Enter()
    {
        _stats.IsWall.Value = true;
    }

    public override void Update()
    {
        if (_input.JumpInput())
        {
            float x = _stats.IsWallLaft.Value ? 1 : -1;

            _stats.ForcedVelocity = new Vector2(x, 0) * 10f;
            _movement.SetForceTime(0.8f);

            _movement.Jump();
            StateMachine.ChangeState("Jump");
        }

        // 더이상 벽 쪽으로 이동키를 누르고 있지 않으면 떨어진다.
        float wallDirection = _stats.IsWallLaft.Value ? -1 : 1;
        if (_stats.MoveInput.Value.x != wallDirection)
        {
            StateMachine.ChangeState("Jump");
        }
    }

    public override void Exit()
    {
        _stats.IsWall.Value = false;
    }
}
