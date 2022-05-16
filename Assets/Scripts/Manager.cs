using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager appManager;
    public bool play = false;
    public int mode; // 0 = practice, 1 = freestyle
    public bool resultPractice = false;
    private void Awake()
    {
        appManager = this;
    }
    private void Start()
    {
        EventsInApp.appEvents.onTimeOut += timeOut;
        EventsInApp.appEvents.onPause += onPause;
        EventsInApp.appEvents.onStartExercise += onStart;
        EventsInApp.appEvents.onHome += Home;
    }
    void timeOut()
    {
        play = false;
        resultPractice = false;
    }

    public void OnResult() 
    {
        int count = PushupSystem.FindObjectOfType<PushupSystem>().Count;
        if (mode == 0)
        {
            if (count == 0)
                resultPractice = true;
            else
                resultPractice = false;
        }
    }
    void onPause() 
    {
        play = !play;
    }
    public void onStart() 
    {
        play = true;
    }

    public void Home() 
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
