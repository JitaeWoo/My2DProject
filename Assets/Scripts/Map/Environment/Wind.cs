using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private Directions _direction;
    [SerializeField] private float _windPower;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Manager.Player.Stats.ExternalVelocity += _direction.ToVector() * _windPower;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Manager.Player.Stats.ExternalVelocity -= _direction.ToVector() * _windPower;
        }
    }
}
