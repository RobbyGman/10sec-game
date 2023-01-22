using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pickupclip, dropclip;
    private bool dragging;
    private Vector2 offset, originalPosition;

    void Awake()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (! dragging) return;

        var mousePosition = GetMousePos();
        transform.position = mousePosition - offset;
    }

    void OnMouseDown()
    {
        dragging = true;
        source.PlayOneShot( pickupclip);
        offset = GetMousePos() - (Vector2)transform.position;
    }

    void OnMouseUp()
    {
        transform.position = originalPosition;
        dragging = false;
        source.PlayOneShot( dropclip);
    }

    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
