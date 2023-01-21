using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
public float speed = 5f;
    public int maxHealth = 5;
    public AudioClip hitSound;
    public AudioClip Music;
    public AudioClip Win;
    public AudioClip Lose;

    public int health
    {
        get 
        { 
            return currentHealth; 
        }
    }
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    public Rigidbody2D rb;

    Vector2 currentInput;
    
    public Animator anim;
    Vector2 lookDirection = new Vector2(0, -1);
    AudioSource audioSource;
    public ParticleSystem hitParticle;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    bool gameOver;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
                
        invincibleTimer = -1.0f;
        currentHealth = maxHealth;
        
        anim = GetComponent<Animator>();
        
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Music;
        audioSource.Play();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);

        gameOver = false;
    }

    void Update(){
        if (isInvincible){
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0){
                isInvincible = false;
            }
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)){
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        currentInput = move;
        anim.SetFloat("Look X", lookDirection.x);
        anim.SetFloat("Look Y", lookDirection.x);
        anim.SetFloat("Speed", move.magnitude);
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameOver == true)
            {
                speed = 3.0f;
                gameOver = false;
            }
        }
    }

    void FixedUpdate(){
        Vector2 position = rb.position;
        position = position + currentInput * speed * Time.deltaTime;
        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount){
        if (amount < 0){
            if (isInvincible){
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
            anim.SetTrigger("Hit");
            audioSource.PlayOneShot(hitSound);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth == 0)
        {
            loseTextObject.SetActive(true);
            gameOver = true;
            audioSource.Stop();

            audioSource.PlayOneShot(Lose);

            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            speed = 0;
        }
    }

    public void PlaySound(AudioClip clip){
        audioSource.PlayOneShot(clip);
    }
}
