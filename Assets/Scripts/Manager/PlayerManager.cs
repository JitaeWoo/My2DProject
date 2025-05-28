using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerStats _statsTemplate;

    public PlayerStats Stats { get; private set; }
    public Transform Transform { get; private set; }

    private void Awake()
    {
        Stats = Instantiate(_statsTemplate);
    }

    public void SetTransform(Transform transfrom)
    {
        Transform = transfrom;
    }
}
