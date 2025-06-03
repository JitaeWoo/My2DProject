using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Directions
{
    UP, DOWN, LEFT, RIGHT
}

public static class DirectionsExtensions
{
    public static Vector2 ToVector(this Directions dir)
    {
        return dir switch
        {
            Directions.UP => Vector2.up,
            Directions.DOWN => Vector2.down,
            Directions.LEFT => Vector2.left,
            Directions.RIGHT => Vector2.right,
            _ => Vector2.zero
        };
    }
}