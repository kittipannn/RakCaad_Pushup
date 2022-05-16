using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    float timer;
    float minutes;
    float seconds;
    float miliseconds;

    float startTime;
    public int startcount;
    
    private void Start()
    {
        EventsInApp.appEvents.onStartExercise += setTimer;
    }
    public void stopWatch(int mode)
    {
        switch (mode)
        {
            case 0: // Practice
                
                timer -= Time.deltaTime;
                if (timer <= 0) { timer = 0; Debug.Log("Time Out"); EventsInApp.appEvents.timeOut(); }
                break;
            case 1: // Freestyle
                timer += Time.deltaTime;
                break;
        }
        miliseconds = (int)((timer - (int)timer) * 100);
        seconds = (int)(timer % 60);
        minutes = (int)(timer / 60 % 60);
    }
    public string displayTimer()
    {
        string showtime = string.Format("{0:00} : {1:00}", minutes, seconds);
        return showtime;
    }

    public string speedPersec()
    {
        int count = PushupSystem.FindObjectOfType<PushupSystem>().Count;
        
        string speedpersec;
        float time = timer;
        if (Manager.appManager.mode == 0)
        {
            time = startTime - time;
            Debug.Log(time);
            count = startcount - count;
            Debug.Log(count);
        }
        speedpersec = string.Format(("{0:0.00}"), count / time);
        return speedpersec;
    }
    void setTimer() 
    {
        if (Manager.appManager.mode == 0)
        {
            timer = uIManager.timeCount();
            startTime = timer;
        }
        else
            timer = 0;
    }

}
