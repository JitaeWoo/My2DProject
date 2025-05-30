using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private int _curDashCount;
    private int _maxDashCount = 1;
    private float _forceRate;

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
            _forceRate = 0;
        }
    }

    private void OnIsGround(bool value)
    {
        if (value)
        {
            _curDashCount = 0;
            _forceRate = 0;
        }
    }

    public void Move(Vector2 direction)
    {
        Vector2 moveVector = direction * Manager.Player.Stats.MoveSpeed;
        moveVector = moveVector * (1 - _forceRate) + Manager.Player.Stats.ForcedVelocity * _forceRate;

        moveVector.y = _rigid.velocity.y;

        moveVector += Manager.Player.Stats.ExternalVelocity;

        _rigid.velocity = moveVector;
    }

    public void Jump()
    {
        Vector2 jumpVector = Vector2.up * Manager.Player.Stats.JumpPower;
        jumpVector.x = _rigid.velocity.x;

        jumpVector += Manager.Player.Stats.ExternalVelocity;

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
        // 대쉬 후 어느정도는 관성이 남는 편이 자연스러워 보였다.
        _rigid.velocity = _rigid.velocity.normalized * 2;
        Manager.Player.Stats.IsControl.Value = true;
    }

    public void SetForceTime(float time)
    {
        StartCoroutine(ForceVectorCoroutine(time));
    }

    private IEnumerator ForceVectorCoroutine (float time)
    {
        _forceRate = 1;
        while (_forceRate > 0)
        {
            _forceRate -= Time.deltaTime / time;
            yield return null;
        }
        _forceRate = 0;
    }
}
