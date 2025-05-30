using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public Stat<Vector2> MoveInput { get; private set; } = new();
    [HideInInspector] public Vector2 Velocity;
    // �÷��̾� ���� �������� ������ �ӵ��� �� �� ���
    [HideInInspector] public Vector2 ForcedVelocity;
    // �ܺο��� �÷��̾ �ӵ��� ���ϰų� �� �� ���
    [HideInInspector] public Vector2 ExternalVelocity;

    public float MoveSpeed;
    public float JumpPower;

    public Stat<bool> IsControl { get; private set; } = new();
    public Stat<bool> IsJump { get; private set; } = new();
    public Stat<bool> IsWalk { get; private set; } = new();
    public Stat<bool> IsGround { get; private set; } = new();
    public Stat<bool> IsWall { get; private set; } = new();
    public Stat<bool> IsWallLaft { get; private set; } = new();
}
