using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerMovement _movement;

    private bool _isControl = true;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _input = new PlayerInput();
        _movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!_isControl) return;

        _movement.Move(_input.MoveInput());

        if (_input.JumpInput())
        {
            _movement.Jump();
        }
    }
}
