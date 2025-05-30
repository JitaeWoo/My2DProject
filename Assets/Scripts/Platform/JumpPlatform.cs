using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    [SerializeField] float _jumpPower = 18;
    private PlayerMovement _movement;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!Manager.Player.Stats.IsGround.Value || Manager.Player.Stats.Velocity.y >= 0.01f) return;

        if (_movement == null)
        {
            _movement = Manager.Player.Transform.GetComponent<PlayerMovement>();
        }

        float curJumpPower = Manager.Player.Stats.JumpPower;

        Manager.Player.Stats.JumpPower = _jumpPower;
        _movement.Jump();

        Manager.Player.Stats.JumpPower = curJumpPower;
    }
}
