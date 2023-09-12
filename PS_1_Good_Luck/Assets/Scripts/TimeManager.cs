using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public TMP_Text textBox;
    public float timeRemaining = 5;
    private bool timerIsRunning = true;

    private void Start()
    {
        timeRemaining = 30;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                GameManager.Instance.isGameEnd = true;
                GameManager.Instance.OnGameEnds();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        textBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
