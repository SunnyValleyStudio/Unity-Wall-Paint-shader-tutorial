using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerPainter : MonoBehaviour
{
    RaycastHit hit;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float rayDistance;

    [SerializeField]
    private Color colorToPaint;

    [SerializeField]
    private GameObject paintIcon;

    PaintableObject paintable;

    [SerializeField]
    private AudioClip paintingClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetColor(Color color)
    {
        color.a = 1;
        colorToPaint = color;
    }

    void Update()
    {
        if (paintable != null)
        {
            paintable.ToggleHighlight(false);
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance,layerMask)
            )
        {
            
            paintable = hit.collider.GetComponent<PaintableObject>();
            if(paintable.IsPainting == false && !paintable.NewColor.Equals(colorToPaint))
            {
                paintable.ToggleHighlight(true);
                paintIcon.SetActive(true);
                paintIcon.GetComponent<Image>().color = colorToPaint;
                if (Mouse.current.leftButton.isPressed)
                {
                    paintable.SetColor(colorToPaint);
                    paintable.StartPainting();
                    paintable.ToggleHighlight(false);
                    paintable.OnDone += HandlePaintingDone;
                    audioSource.PlayOneShot(paintingClip);
                }
            }
            else
            {
                paintIcon.SetActive(false);
            }
            
        }
        else
        {
            paintIcon.SetActive(false);
        }
    }

    private void HandlePaintingDone()
    {
        audioSource.Stop();
    }
}
