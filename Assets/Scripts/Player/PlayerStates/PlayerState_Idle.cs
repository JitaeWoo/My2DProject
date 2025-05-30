using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Idle : PlayerState
{
    private PlayerInput _input;
    public PlayerState_Idle(StateMachine stateMachine) : base(stateMachine)
    {
        _input = new PlayerInput();
    }

    public override void Update()
    {
        if(_input.MoveInput() != Vector2.zero)
        {
            StateMachine.ChangeState(new PlayerState_Walk(StateMachine));
        }
    }
}
