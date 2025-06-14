using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats _stats => Manager.Player.Stats;
    private Rigidbody2D _rigid;
    private int _curDashCount;
    private int _maxDashCount = 1;
    private float _forceRate;
    private float _gravityScale = 3;
    private bool _isDashCancle;

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
        _rigid.velocity = _stats.Velocity + _stats.ExternalVelocity;
    }

    private void OnEnable()
    {
        _stats.IsWall.OnChanged += OnIsWall;
        _stats.IsGround.OnChanged += OnIsGround;
    }

    private void OnDisable()
    {
        _stats.IsWall.OnChanged -= OnIsWall;
        _stats.IsGround.OnChanged -= OnIsGround;
    }

    private void OnIsWall(bool value)
    {
        if (value)
        {
            _rigid.velocity = new Vector2(_stats.Velocity.x, 0);
            _rigid.gravityScale = 0;
            _forceRate = 0;
        }
        else
        {
            _rigid.gravityScale = _gravityScale;
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
        Vector2 moveVector = direction * _stats.MoveSpeed;
        moveVector = moveVector * (1 - _forceRate) + _stats.ForcedVelocity * _forceRate;

        moveVector.y = _rigid.velocity.y;

        _stats.Velocity = moveVector;
    }

    public void Jump()
    {
        Vector2 jumpVector = Vector2.up * _stats.JumpPower;
        jumpVector.x = _rigid.velocity.x;

        _rigid.velocity = jumpVector;
        _stats.Velocity = _rigid.velocity;

        _isDashCancle = true;
    }

    public void Stop()
    {
        _stats.Velocity = Vector2.zero;
    }

    public void Dash(Vector2 direction)
    {
        if (_curDashCount < _maxDashCount)
        {
            StartCoroutine(DashCoroutine(direction));
        }
    }

    private IEnumerator DashCoroutine(Vector2 direction)
    {
        float curGravity = _rigid.gravityScale;
        float dashTime = 0.2f;
        _isDashCancle = false;

        _stats.IsControl.Value = false;
        _stats.IsDash.Value = true;
        _rigid.gravityScale = 0;
        _stats.Velocity = direction * _stats.MoveSpeed * 4;

        while(dashTime > 0f)
        {
            if (_isDashCancle)
            {
                _rigid.gravityScale = curGravity;
                break;
            }

            dashTime -= Time.deltaTime;
            yield return null;
        }

        if (!_stats.IsGround.Value)
        {
            _curDashCount++;
        }

        if (!_isDashCancle)
        {
            _rigid.gravityScale = curGravity;
            // 대쉬 후 어느정도는 관성이 남는 편이 자연스러워 보였다.
            _rigid.velocity = _stats.Velocity * 0.2f;
        }
        _stats.IsDash.Value = false;
        _stats.IsControl.Value = true;
    }

    public void SetForceTime(float time)
    {
        StartCoroutine(ForceVectorCoroutine(time));
    }

    private IEnumerator ForceVectorCoroutine(float time)
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
