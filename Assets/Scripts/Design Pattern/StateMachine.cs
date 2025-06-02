using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public BaseState CurState;
    public Dictionary<string, BaseState> StateDict = new Dictionary<string, BaseState>();

    public void ChangeState(string changedStateName)
    {
        if (!StateDict.ContainsKey(changedStateName)) return;

        BaseState changedState = StateDict[changedStateName];

        if (CurState == changedState) return;

        // ó���� ���� ���� ������ null üũ
        CurState?.Exit();
        CurState = changedState;
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
