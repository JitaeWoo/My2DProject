using System.Collections;
using Unity.VisualScripting;
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
        if (_input.JumpInput())
        {
            if (Manager.Player.Stats.IsWall.Value)
            {
                float x = Manager.Player.Stats.IsWallLaft.Value ? 1 : -1;

                Manager.Player.Stats.ForcedVelocity = new Vector2(x, 0) * 10f;
                _movement.SetForceTime(0.8f);
                
                _movement.Jump();
            }
            else if (_curJumpCount < _maxJumpCount)
            {
                _movement.Jump();
                _curJumpCount++;
            }
        }


        if (Manager.Player.Stats.IsGround.Value && Manager.Player.Stats.Velocity.y < 0.01f)
        {
            StateMachine.ChangeState(new PlayerState_Idle(StateMachine));
        }
    }

    public override void Exit()
    {
        Manager.Player.Stats.IsJump.Value = false;
    }
}
