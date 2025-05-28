using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public BaseState CurState;

    public void ChangeState(BaseState changedStats)
    {
        if (CurState == changedStats) return;

        // ó���� ���� ���� ������ null üũ
        CurState?.Exit();
        CurState = changedStats;
        CurState.Enter();
    }

    public void Update() => CurState.Update();

    public void FixedUpdate()
    {
        if (CurState.HasPhysics)
        {
            CurState.FixedUpdate();
        }
    }
}
