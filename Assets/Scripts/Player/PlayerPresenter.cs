using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPresenter : MonoBehaviour
{
    private PlayerStats _stats => Manager.Player.Stats;

    private SpriteRenderer _sprite;
    private Animator _animator;
    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetFloat("MoveVectorY", _stats.Velocity.y);
    }

    private void OnEnable()
    {
        _stats.IsWalk.OnChanged += ChangeIsWalk;
        _stats.IsJump.OnChanged += ChangeIsJump;
        _stats.MoveInput.OnChanged += ChangeMoveInput;
        _stats.IsDash.OnChanged += ChangeIsDash;
    }

    private void OnDisable()
    {
        _stats.IsWalk.OnChanged -= ChangeIsWalk;
        _stats.IsJump.OnChanged -= ChangeIsJump;
        _stats.MoveInput.OnChanged -= ChangeMoveInput;
        _stats.IsDash.OnChanged -= ChangeIsDash;
    }

    private void ChangeIsWalk(bool value)
    {
        _animator.SetBool("IsWalk", value);
    }

    private void ChangeIsJump(bool value)
    {
        _animator.SetBool("IsJump", value);
    }

    private void ChangeIsDash(bool value)
    {
        _animator.SetBool("IsDash", value);
    }

    private void ChangeMoveInput(Vector2 value)
    {
        if(value.x < 0)
        {
            _sprite.flipX = true;
        }
        else if(value.x > 0)
        {
            _sprite.flipX = false;
        }
    }
}
