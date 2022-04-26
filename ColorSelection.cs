using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    private Image image;
    public Key key;
    public UnityEvent<Color> ColorSelectionEvent;
    [SerializeField]
    private float duration = 0.3f;
    [SerializeField]
    private Vector3 endScale = Vector3.one;

    [SerializeField]
    private AudioSource audioSource;
    private void Start()
    {
        image = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Keyboard.current[key].wasPressedThisFrame)
        {
            audioSource.Stop();
            ColorSelectionEvent?.Invoke(image.color);
            StartCoroutine(Animate());
            audioSource.Play();
        }
    }

    private IEnumerator Animate()
    {
        float currentTIme = 0;
        while(currentTIme< duration)
        {
            currentTIme += Time.deltaTime;
            image.rectTransform.localScale = Vector3.Lerp(Vector3.one, endScale, currentTIme);
            yield return null;
        }
        image.rectTransform.localScale = Vector3.one;
    }
}
