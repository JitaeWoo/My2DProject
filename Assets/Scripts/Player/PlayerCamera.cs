using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _cam;
    private CinemachineConfiner2D _confiner;
    private bool _isInvalidate;

    private void Awake()
    {
        _confiner = GetComponent<CinemachineConfiner2D>();
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void LateUpdate()
    {
        if (!_isInvalidate)
        {
            float screenRatio = (float)Screen.height / Screen.width;
            _cam.m_Lens.OrthographicSize = 26f * screenRatio * 0.5f;

            _confiner.InvalidateCache();
            _isInvalidate = true;
        }

        
    }
}
