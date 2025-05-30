using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private int _curDashCount;
    private int _maxDashCount = 1;

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

    private void OnEnable()
    {
        Manager.Player.Stats.IsWall.OnChanged += OnIsWall;
        Manager.Player.Stats.IsGround.OnChanged += OnIsGround;
    }

    private void OnDisable()
    {
        Manager.Player.Stats.IsWall.OnChanged -= OnIsWall;
        Manager.Player.Stats.IsGround.OnChanged -= OnIsGround;
    }

    private void OnIsWall(bool value)
    {
        if (value)
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, 0);
        }
    }

    private void OnIsGround(bool value)
    {
        if (value)
        {
            _curDashCount = 0;
        }
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

    public void Dash(Vector2 direction)
    {
        if(_curDashCount < _maxDashCount)
        {
            _curDashCount++;
            StartCoroutine(DashCoroutine(direction));
        }
    }

    private IEnumerator DashCoroutine(Vector2 direction)
    {
        float curGravity = _rigid.gravityScale;

        Manager.Player.Stats.IsControl.Value = false;
        _rigid.gravityScale = 0;
        _rigid.velocity = direction * Manager.Player.Stats.MoveSpeed * 4;
        yield return new WaitForSeconds(0.2f);
        _rigid.gravityScale = curGravity;
        // �뽬 �� ��������� ������ ���� ���� �ڿ������� ������.
        _rigid.velocity = _rigid.velocity.normalized * 2;
        Manager.Player.Stats.IsControl.Value = true;
    }
}
