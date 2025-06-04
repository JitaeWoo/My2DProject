using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _btn.onClick.AddListener(ClickButton);
    }

    private void OnDisable()
    {
        _btn.onClick.RemoveListener(ClickButton);
    }

    private void ClickButton()
    {
        SceneManager.LoadScene("Stage");
    }
}
