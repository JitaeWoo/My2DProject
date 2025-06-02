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
        _stats.IsGround.OnChanged += ChangeIsGround;
        _stats.MoveInput.OnChanged += ChangeMoveInput;
        _stats.IsDash.OnChanged += ChangeIsDash;
        _stats.IsWall.OnChanged += ChangeIsWall;
        _stats.IsDamage.OnChanged += ChangeIsDamage;
        _stats.IsDied.OnChanged += ChangeIsDied;
    }

    private void OnDisable()
    {
        _stats.IsWalk.OnChanged -= ChangeIsWalk;
        _stats.IsGround.OnChanged -= ChangeIsGround;
        _stats.MoveInput.OnChanged -= ChangeMoveInput;
        _stats.IsDash.OnChanged -= ChangeIsDash;
        _stats.IsWall.OnChanged -= ChangeIsWall;
        _stats.IsDamage.OnChanged -= ChangeIsDamage;
        _stats.IsDied.OnChanged -= ChangeIsDied;
    }

    private void ChangeIsWalk(bool value)
    {
        _animator.SetBool("IsWalk", value);
    }

    private void ChangeIsGround(bool value)
    {
        _animator.SetBool("IsGround", value);
    }

    private void ChangeIsDash(bool value)
    {
        _animator.SetBool("IsDash", value);
    }

    private void ChangeIsWall(bool value)
    {
        _animator.SetBool("IsWall", value);
    }

    private void ChangeIsDamage(bool value)
    {
        if (value)
        {
            _animator.SetTrigger("DamageTrigger");
            _stats.IsDamage.Value = false;
        }   
    }

    private void ChangeIsDied(bool value)
    {
        _animator.SetBool("IsDied", value);
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
