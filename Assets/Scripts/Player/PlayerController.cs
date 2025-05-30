using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerMovement _movement;
    private StateMachine _stateMachine;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        Manager.Player.SetTransform(transform);
        Manager.Player.Stats.IsControl.Value = true;
        _input = new PlayerInput();
        _movement = GetComponent<PlayerMovement>();
        _stateMachine = new StateMachine();
        _stateMachine.ChangeState(new PlayerState_Idle(_stateMachine));
    }

    void Update()
    {
        if (!Manager.Player.Stats.IsControl.Value) return;

        _stateMachine.Update();

        _movement.Move(_input.MoveInput());

        if (_input.JumpInput() && !Manager.Player.Stats.IsJump.Value)
        {
            _movement.Jump();
            _stateMachine.ChangeState(new PlayerState_Jump(_stateMachine));
        }

        if(!Manager.Player.Stats.IsGround.Value && !Manager.Player.Stats.IsJump.Value)
        {
            _stateMachine.ChangeState(new PlayerState_Jump(_stateMachine));
        }
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
}
