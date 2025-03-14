using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ExtendedButton : Button
{
    [Header("Audio Properties")]
    [SerializeField] private AudioClip HoverSound;
    [SerializeField] private AudioClip ClickSound;
    public AudioSource audioSource;

    [Header("Hover Events")]
    [SerializeField] private UnityEvent onHoverEnter = new UnityEvent();
    [SerializeField] private UnityEvent onHoverExit = new UnityEvent();

    public UnityEvent OnHoverEnter => onHoverEnter;
    public UnityEvent OnHoverExit => onHoverExit;

    private bool isHovering;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

    }

    public override void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if (!isHovering)
        {
            isHovering = true;

            if (HoverSound != null && audioSource != null)
            {
                audioSource.clip = HoverSound;
                audioSource.Play();
            }
            onHoverEnter.Invoke();

        }
    }

    public override void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (isHovering)
        {
            isHovering = false;
            onHoverExit.Invoke();
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if(ClickSound != null && audioSource != null)
        {
            audioSource.clip = ClickSound;
            audioSource.Play();
        }
    }
}
