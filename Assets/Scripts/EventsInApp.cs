using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsInApp : MonoBehaviour
{
    public static EventsInApp appEvents;
    private void Awake()
    {
        appEvents = this;
    }

    public event Action onTimeOut;
    public void timeOut() 
    {
        if (onTimeOut != null)
        {
            onTimeOut();
        }
    }
    public event Action onPractice;
    public void practice()
    {
        if (onPractice != null)
        {
            onPractice();
        }
    }
    public event Action onFreestyle;
    public void freestyle()
    {
        if (onFreestyle != null)
        {
            onFreestyle();
        }
    }
    public event Action onPause;
    public void Pause()
    {
        if (onPause != null)
        {
            onPause();
        }
    }
    public event Action onStartExercise;
    public void StartExercise()
    {
        if (onStartExercise != null)
        {
            onStartExercise();
        }
    }
    public event Action onHome;
    public void home()
    {
        if (onHome != null)
        {
            onHome();
        }
    }
    public event Action onFinishPractice;
    public void FinishPractice()
    {
        if (onFinishPractice != null)
        {
            onFinishPractice();
        }
    }
}
