using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintableObject : MonoBehaviour
{
    private Renderer objectRenderer;

    public float paintDelay = 1.5f;

    private ProgressIcon progressIcon;

    private bool isPainting;
    private Color newColor;

    public bool IsPainting { get => isPainting; private set => isPainting = value; }
    public Color NewColor { get => newColor; private set => newColor = value; }

    public event Action OnDone;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.SetFloat("_PaintAmount", 0);
        progressIcon = FindObjectOfType<ProgressIcon>();
    }

    public void StartPainting()
    {
        IsPainting = true;
        StartCoroutine(PaintCoroutine());
    }

    public void SetColor(Color colorToPaint)
    {
        NewColor = colorToPaint;
    }

    public void ToggleHighlight(bool val)
    {
        objectRenderer.material.SetFloat("_Highlight", val ? 1 : 0);
    }

    private IEnumerator PaintCoroutine()
    {
        float currentTime = 0;
        objectRenderer.material.SetColor("_PaintColor", NewColor);
        progressIcon.ToggleIcon(true);
        while(currentTime < paintDelay)
        {
            currentTime += Time.deltaTime;
            float amount = Mathf.Clamp01(currentTime / paintDelay); 
            progressIcon.SetFillAmount(amount);
            objectRenderer.material.SetFloat("_PaintAmount", amount);
            yield return null;
        }
        objectRenderer.material.SetColor("_Color", NewColor);
        objectRenderer.material.SetFloat("_PaintAmount", 0);
        IsPainting = false;
        progressIcon.ToggleIcon(false);
        OnDone?.Invoke();
    }
}
