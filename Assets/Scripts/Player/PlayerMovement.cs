using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Manager.Player.Stats.Velocity = _rigid.velocity;
    }

    public void Move(Vector2 direction)
    {
        Vector2 moveVector = direction * Manager.Player.Stats.MoveSpeed;
        moveVector.y = _rigid.velocity.y;

        moveVector += Manager.Player.Stats.AdditionalVelocity;

        _rigid.velocity = moveVector;
    }

    public void Jump()
    {
        Vector2 jumpVector = Vector2.up * Manager.Player.Stats.JumpPower;
        jumpVector.x = _rigid.velocity.x;

        jumpVector += Manager.Player.Stats.AdditionalVelocity;

        _rigid.velocity = jumpVector;
    }
}
