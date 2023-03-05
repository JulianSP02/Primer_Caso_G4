using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    Slider slider;

    [SerializeField]
    float maximumTime = 180.0F;

    float currentTime = 0.0F;

    bool timerEnabled;

    LevelManager levelManager;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

    }

    void Start()
    {
        ActivateTimer();
    }

    void Update()
    {
        ChangeTimer();
    }

    void ActivateTimer()
    {
        currentTime = maximumTime;
        slider.maxValue = currentTime;
        EnableTimer(true);
    }

    void EnableTimer(bool enabled)
    {
        timerEnabled = enabled;
    }

    void ChangeTimer()
    {
        if (!timerEnabled)
        {
            return;
        }

        currentTime -= Time.deltaTime;
        if(currentTime > 0.0F)
        {
            slider.value = currentTime;
        }
        else
        {
            // El personaje debe morir => Game Over
            EnableTimer(false);
            levelManager.LastScene();
        }
    }
}
