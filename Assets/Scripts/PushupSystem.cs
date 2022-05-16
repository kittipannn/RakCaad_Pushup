using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushupSystem : MonoBehaviour
{
    ArduinoRead arduino;
    [SerializeField] UIManager uIManager;
    [SerializeField] Stopwatch stopwatch;
    [SerializeField] float minValue;
    [SerializeField] float maxValue;
    bool pushUp = false;
    
    int mode; // 0 = practice, 1 = freestyle
    bool play = true;
    int count;
    public int Count { get { return count; } set { count = value; } }
    int pace;
    public int Pace { get { return pace; } set { pace = value; } }

    private void Start()
    {
        arduino = ArduinoRead.arduino;
    }

    void Update()
    {
        if (Manager.appManager.play)
        {
            if (arduino.Connected)
            {
                switch (Manager.appManager.mode)
                {
                    case 0: // Practice

                        if (arduino.valueArduino == minValue && !pushUp) { pushUp = true; }
                        else if (arduino.valueArduino >= maxValue && pushUp) { pushUp = false; count--; }
                        if (count <= 0) { count = 0; Debug.Log("Pass"); EventsInApp.appEvents.FinishPractice(); }
                        break;
                    case 1: // Freestyle
                        
                        if (arduino.valueArduino <= minValue && !pushUp) { pushUp = true; }
                        else if (arduino.valueArduino >= maxValue && pushUp) { pushUp = false; count++; }
                        break;
                }
            }
            else
            {
                switch (Manager.appManager.mode)
                {
                    case 0: // Practice
                        if (Input.GetKeyDown(KeyCode.Space))
                            count--;
                        if (count <= 0) { count = 0; Debug.Log("Pass"); EventsInApp.appEvents.FinishPractice(); }
                        break;
                    case 1: // Freestyle
                        if (Input.GetKeyDown(KeyCode.Space))
                            count++;
                        break;
                }
            }
            stopwatch.stopWatch(Manager.appManager.mode);
        }
        
    }
}
