using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    Rigidbody2D rb;
    Animator anim;
    Vector2 currentInput;
    Vector2 lookDirection = new Vector2(0, 1);
    public int maxHealth;
    int currentHealth;
    public int health
    {
        get
        {
            return currentHealth;
        }
    }
    public float timeInvincible = 2.0f;
    public Transform respawnPosition;
    public ParticleSystem hitEffect;
    public AudioClip hitSound;
    float invincibleTimer;
    bool isInvincible;
    AudioSource audioSource;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    bool gameOver;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        invincibleTimer = -1.0f;
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        gameOver = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        currentInput = move;

        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.y);
        anim.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                speed = 3.0f;
                gameOver = false;
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position;
        position = position + currentInput * speed * Time.deltaTime;
        rb.MovePosition(position);
    }
    
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        { 
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            
            anim.SetTrigger("Hit");
            audioSource.PlayOneShot(hitSound);

            Instantiate(hitEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        }
        
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        if(currentHealth == 0)
        {
            loseTextObject.SetActive(true);
            gameOver = true;
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            speed = 0;        }
        Health.Instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition.position;
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
