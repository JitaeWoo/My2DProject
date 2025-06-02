using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState_Jump : PlayerState
{
    private PlayerStats _stats => Manager.Player.Stats;
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
        _stats.IsJump.Value = true;
        _stats.CurJumpCount++;
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
            else if (_stats.CurJumpCount < _stats.MaxJumpCount)
            {
                _movement.Jump();
                _stats.CurJumpCount++;
            }
        }


        if (_stats.IsGround.Value && _stats.Velocity.y < 0.01f)
        {
            StateMachine.ChangeState("Idle");
        }
    }

    public override void Exit()
    {
        _stats.IsJump.Value = false;
    }


}
