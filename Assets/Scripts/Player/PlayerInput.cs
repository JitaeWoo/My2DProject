using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public Vector2 MoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        return new Vector2(x, y);
    }
}
