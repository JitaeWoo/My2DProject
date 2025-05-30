using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Walk : PlayerState
{
    private PlayerInput _input;

    public PlayerState_Walk(StateMachine stateMachine) : base(stateMachine)
    {
        _input = new PlayerInput();
    }

    public override void Enter()
    {
        Manager.Player.Stats.IsWalk.Value = true;
    }

    public override void Update()
    {
        if(_input.MoveInput() == Vector2.zero)
        {
            StateMachine.ChangeState(new PlayerState_Idle(StateMachine));
        }
    }


    public override void Exit()
    {
        Manager.Player.Stats.IsWalk.Value = false;
    }
}
