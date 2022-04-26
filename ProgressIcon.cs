using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIcon : MonoBehaviour
{
    private Image progressIcon;
    [SerializeField]
    private GameObject parent;

    private void Start()
    {
        progressIcon = GetComponent<Image>();
        ToggleIcon(false);
    }

    public void ToggleIcon(bool val)
    {
        if (val)
        {
            parent.SetActive(true);
            SetFillAmount(0);
        }
        else
        {
            parent.SetActive(false);
        }
    }

    public void SetFillAmount(float value)
    {
        progressIcon.fillAmount = Mathf.Clamp01(value);
    }
}
