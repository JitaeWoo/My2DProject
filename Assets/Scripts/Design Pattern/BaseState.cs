using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected StateMachine StateMachine;
    public bool HasPhysics;

    public BaseState(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public abstract void Enter();

    public abstract void Update();

    public virtual void FixedUpdate() { }

    public abstract void Exit();
}
