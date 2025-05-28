using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private PlayerStats _statsTemplate;

    [HideInInspector] public PlayerStats Stats;

    private void Awake()
    {
        Stats = Instantiate(_statsTemplate);
    }
}
