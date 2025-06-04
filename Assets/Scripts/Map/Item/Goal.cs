using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject _clearUIPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(_clearUIPrefab);
            Time.timeScale = 0f;
            gameObject.SetActive(false);
        }
    }
}
