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
                float x = -_input.MoveInput().x;

                Manager.Player.StartCoroutine(AddVelocity(new Vector2(x, 0), 0.8f));
                
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

    private IEnumerator AddVelocity(Vector2 direction, float time)
    {
        Vector2 addVelocity = direction * 10f;
        float curMoveSpeed = Manager.Player.Stats.MoveSpeed;
        float count = time;

        Manager.Player.Stats.AdditionalVelocity += addVelocity;
        Manager.Player.Stats.MoveSpeed = 0;

        while (count > 0f)
        {
            count -= Time.deltaTime;
            Manager.Player.Stats.AdditionalVelocity -= addVelocity * Time.deltaTime / time;
            Manager.Player.Stats.MoveSpeed += curMoveSpeed * Time.deltaTime / time;
            yield return null;
        }
    }
}
