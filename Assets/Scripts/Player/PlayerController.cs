using System;
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
        _stateMachine.StateDict["Idle"] = new PlayerState_Idle(_stateMachine);
        _stateMachine.StateDict["Walk"] = new PlayerState_Walk(_stateMachine);
        _stateMachine.StateDict["Jump"] = new PlayerState_Jump(_stateMachine);
        _stateMachine.StateDict["StayWall"] = new PlayerState_StayWall(_stateMachine);
        _stateMachine.ChangeState("Idle");
    }

    private void OnEnable()
    {
        _stats.IsGround.OnChanged += SetJumpState;
        _stats.IsDied.OnChanged += Died;
    }

    private void OnDisable()
    {
        _stats.IsGround.OnChanged -= SetJumpState;
        _stats.IsDied.OnChanged -= Died;
    }

    private void SetJumpState(bool value)
    {
        if (!value)
        {
            _stateMachine.ChangeState("Jump");
        }
    }
    private void Died(bool value)
    {
        if (value)
        {
            _stats.IsControl.Value = false;
            StartCoroutine(DiedCoroutine());
        }
    }

    private IEnumerator DiedCoroutine()
    {
        yield return new WaitForSeconds(4f);
        _stats.IsDied.Value = false;
        Manager.Player.ReturnSafePosition();
        _stats.CurHp.Value = _stats.MaxHp.Value;
        _stats.IsControl.Value = true;
    }

    void Update()
    {
        if (!_stats.IsControl.Value) return;

        _stateMachine.Update();

        Vector2 moveInput = _input.MoveInput();
        _stats.MoveInput.Value = moveInput;

        _movement.Move(moveInput);

        if (_input.JumpInput() && _stats.IsGround.Value)
        {
            _movement.Jump();
        }

        if (_input.DashInput())
        {
            _movement.Dash(moveInput.normalized);
        }
    }

    private void FixedUpdate()
    {
        if (!_stats.IsControl.Value) return;

        _stateMachine.FixedUpdate();
    }
}
