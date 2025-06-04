using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearButton : MonoBehaviour
{
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _btn.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _btn.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
