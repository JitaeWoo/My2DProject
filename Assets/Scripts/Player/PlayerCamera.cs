using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineConfiner2D _confiner;

    private void Awake()
    {
        _confiner = GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        _confiner.InvalidateCache();
    }
}
