using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public Stat<bool> IsJump;

    public Vector2 Velocity;

    public float MoveSpeed;
    public float JumpPower;
}
