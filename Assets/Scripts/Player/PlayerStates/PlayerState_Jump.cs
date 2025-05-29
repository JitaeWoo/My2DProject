using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Jump : PlayerState
{
    private PlayerMovement _movement;
    private PlayerInput _input;
    private int _curJumpCount;
    private int _maxJumpCount;

    public PlayerState_Jump(StateMachine stateMachine) : base(stateMachine)
    {
        _movement = Manager.Player.Transform.GetComponent<PlayerMovement>();
        _input = new PlayerInput();
        _maxJumpCount = 1;
    }

    public override void Enter()
    {
        Manager.Player.Stats.IsJump.Value = true;
    }

    public override void Update()
    {
        if(_curJumpCount < _maxJumpCount && _input.JumpInput())
        {
            _movement.Jump();
            _curJumpCount++;
        }

        if(Manager.Player.Stats.IsGround && Manager.Player.Stats.Velocity.y < 0.01f)
        {
            StateMachine.ChangeState(new PlayerState_Idle(StateMachine));
        }
    }

    public override void Exit()
    {
        Manager.Player.Stats.IsJump.Value = false;
    }
}
