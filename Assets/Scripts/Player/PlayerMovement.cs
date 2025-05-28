using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Rigidbody2D _rigid;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        Vector2 moveVector = direction * _moveSpeed;
        moveVector.y = _rigid.velocity.y;

        _rigid.velocity = moveVector;
    }
}
