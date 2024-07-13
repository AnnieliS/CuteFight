using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Timer : MonoBehaviour
{
    [SerializeField] float timeForRound = 30;
    float timeRemaining = 0;
    public bool timerIsRunning = false;
    public bool rotateRing = true;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject outerRing;
    float rotationSteps;
    float currentRotation = 0f;
    Quaternion outerRingRotation;

    private void Start()
    {
        if (outerRing != null)
        {
            outerRingRotation = outerRing.transform.rotation;
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                if (timerText != null)
                    timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
                if (outerRing != null && rotateRing)
                {
                    currentRotation -= rotationSteps * Time.deltaTime;
                    outerRing.transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

                }

            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                RoundFinished();
            }
        }
    }

    public void StartTimer()
    {
        timeRemaining = timeForRound;
        timerIsRunning = true;
        outerRing.transform.rotation = outerRingRotation;
        rotationSteps = 360 / timeRemaining;
    }

    public bool RoundFinished()
    {
        GameManager.Instance().ChooseRoundWinner();
        return true;
    }
}