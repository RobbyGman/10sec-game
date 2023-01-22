using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birdcontroller : MonoBehaviour
{
    private PlayerMovement controller;
    public GameObject player;
    public float speed;
    private float distance;
    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 5)
        {
            Vector2 direction = player.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    void OnCollisionStay2D(Collision2D other)
    {
        PlayerMovement controller = other.collider.GetComponent<PlayerMovement>();
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
