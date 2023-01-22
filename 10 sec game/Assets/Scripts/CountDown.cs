using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;
    public TextMeshProUGUI countdownText; 
    public GameObject loseText;
    bool gameOver;
    void Start()
    {
        currentTime = startingTime;
        loseText.SetActive(false);
        gameOver = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                gameOver = false;
            }
        }
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
        if (currentTime < 0 )
        {
            currentTime = 0;
            loseText.SetActive(true);
            gameOver = true;
        }
    }
}
