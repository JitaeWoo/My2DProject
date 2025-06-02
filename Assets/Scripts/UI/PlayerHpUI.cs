using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpUI : MonoBehaviour
{
    [SerializeField] private Image _hpImagePrefab;
    private Image[] _hpImages;

    private int _maxHpImageCount = 10;
    private float _interval = 130f;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _hpImages = new Image[_maxHpImageCount];

        float x = 20f;

        for (int i = 0; i < _maxHpImageCount; i++)
        {
            _hpImages[i] = Instantiate(_hpImagePrefab, transform);
            _hpImages[i].transform.localPosition = new Vector2(x, 0);
            _hpImages[i].gameObject.SetActive(false);
            x += _interval;
        }
    }

    private void OnEnable()
    {
        UpdateHpUI(Manager.Player.Stats.CurHp.Value);
        Manager.Player.Stats.CurHp.OnChanged += UpdateHpUI;
    }

    private void OnDisable()
    {
        Manager.Player.Stats.CurHp.OnChanged -= UpdateHpUI;
    }

    private void UpdateHpUI(int curHp)
    {
        foreach(Image hpImage in _hpImages)
        {
            hpImage.gameObject.SetActive(false);
        }

        for(int i = 0; i < curHp; i++)
        {
            _hpImages[i].gameObject.SetActive(true);
        }
    }
}
