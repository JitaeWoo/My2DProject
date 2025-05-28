using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager
{
    public static GameManager Game => GameManager.GetInstance();
    public static PlayerManager Player => PlayerManager.GetInstance();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initailize()
    {
        GameManager.CreateInstance();
        PlayerManager.CreateInstance();
    }
}
