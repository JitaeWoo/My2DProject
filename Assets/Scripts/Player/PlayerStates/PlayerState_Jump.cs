using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_Jump : PlayerState
{
    private Collider2D[] _cols = new Collider2D[10];
    private LayerMask _layerMask = 1 << LayerMask.NameToLayer("Ground");

    public PlayerState_Jump(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Manager.Player.Stats.IsJump.Value = true;
    }

    public override void Update()
    {
        Vector2 point = Manager.Player.Transform.position;
        Vector2 range = new Vector2(Manager.Player.Transform.localScale.x, 0.1f);

        int count = Physics2D.OverlapBoxNonAlloc(point, range, 0f, _cols, _layerMask);

        if(count > 0 && Manager.Player.Stats.Velocity.y < 0.01f)
        {
            StateMachine.ChangeState(new PlayerState_Idle(StateMachine));
        }
    }

    public override void Exit()
    {
        Manager.Player.Stats.IsJump.Value = false;
    }
}
