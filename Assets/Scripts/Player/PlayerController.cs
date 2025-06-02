using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerStats _stats => Manager.Player.Stats;
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
        if (!_stats.IsControl.Value) return;

        _stateMachine.Update();

        Vector2 moveInput = _input.MoveInput();
        _stats.MoveInput.Value = moveInput;

        _movement.Move(moveInput);

        if (_input.JumpInput() && !_stats.IsJump.Value)
        {
            _movement.Jump();
            _stateMachine.ChangeState(new PlayerState_Jump(_stateMachine));
        }

        if (_input.DashInput())
        {
            _movement.Dash(moveInput.normalized);
        }

        if(!_stats.IsGround.Value && !_stats.IsJump.Value)
        {
            _stateMachine.ChangeState(new PlayerState_Jump(_stateMachine));
        }
    }

    private void FixedUpdate()
    {
        if (!_stats.IsControl.Value) return;

        _stateMachine.FixedUpdate();
    }
}
