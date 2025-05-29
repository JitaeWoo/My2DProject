using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public Stat<bool> IsJump { get; private set; } = new();

    public Vector2 Velocity;
    public Vector2 AdditionalVelocity;

    public float MoveSpeed;
    public float JumpPower;

    public Stat<bool> IsGround { get; private set; } = new();
    public Stat<bool> IsWall { get; private set; } = new();
}
