using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScaler : MonoBehaviour
{
    public float hoverScale = 1.1f;
    public Vector2 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnHover()
    {
        transform.localScale = new Vector3(hoverScale, hoverScale, hoverScale);
    }

    public void OnExit()
    {
        transform.localScale = originalScale;
    }
}