using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key _key;

    private void OnEnable()
    {
        _key.OnGot += OnKeyGot;
    }

    private void OnDisable()
    {
        _key.OnGot -= OnKeyGot;
    }

    private void OnKeyGot()
    {
        gameObject.SetActive(false);
    }
}
