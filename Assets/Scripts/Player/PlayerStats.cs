using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public Stat<Vector2> MoveInput { get; private set; } = new();
    public Stat<Vector2> LastSafePosition { get; private set; } = new();
    [HideInInspector] public Vector2 Velocity;
    // 플레이어 내부 구조에서 강제로 속도를 줄 때 사용
    [HideInInspector] public Vector2 ForcedVelocity;
    // 외부에서 플레이어에 속도를 더하거나 뺄 때 사용
    [HideInInspector] public Vector2 ExternalVelocity;

    public Stat<int> MaxHp { get; private set; } = new();
    [SerializeField] private  int _maxHp;
    public Stat<int> CurHp { get; private set; } = new();
    public int MaxJumpCount;
    [HideInInspector] public int CurJumpCount;
    public float MoveSpeed;
    public float JumpPower;

    public Stat<bool> IsControl { get; private set; } = new();
    public Stat<bool> IsWalk { get; private set; } = new();
    public Stat<bool> IsDash { get; private set; } = new();
    public Stat<bool> IsGround { get; private set; } = new();
    public Stat<bool> IsWall { get; private set; } = new();
    public Stat<bool> IsWallLaft { get; private set; } = new();
    public Stat<bool> IsDamage { get; private set; } = new();
    public Stat<bool> IsDied { get; private set; } = new();

    private void Awake()
    {
        if (Application.isPlaying)
        {
            MaxHp.Value = _maxHp;
            CurHp.Value = MaxHp.Value;
        }
    }
}
