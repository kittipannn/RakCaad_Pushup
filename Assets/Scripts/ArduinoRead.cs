using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoRead : MonoBehaviour
{
    public static ArduinoRead arduino;
    [SerializeField] UIManager uIManager;
    public bool Connected = false;
    [Header("ArduinoValue")]
    SerialPort stream;
    [SerializeField] string port; //Change port 

    string valuefromArduino; //2
    public float valueArduino { get { if (Connected) return float.Parse(valuefromArduino); else return 0; } }
    private void Awake()
    {

        if (arduino == null)
        {
            arduino = this;
        }
        else
        {
            Destroy(this);
        }

        foreach (string mysps in SerialPort.GetPortNames())
        {
            print(mysps);
            if (mysps != "COM6") { port = mysps; break; }
            if (mysps != null) { Connected = true; }
        }
        
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Connected)
        {
            stream = new SerialPort(port, 9600);

            if (!stream.IsOpen)
            {
                print("Opening " + port + ", baud 9600");
                stream.Open();
                stream.ReadTimeout = 100;
                stream.Handshake = Handshake.None;
                if (stream.IsOpen) { print("Open"); }
            }
        }
        else
        {
            uIManager.warning();
            print("Please Connect Your Device");
            //InvokeRepeating("reconnectDevice", 0, 1);
        }
    }
    void reconnectDevice()
    {
        Debug.Log("Reconnect");
        foreach (string mysps in SerialPort.GetPortNames())
        {
            print(mysps);
            if (mysps != "COM6") { port = mysps; break; }
            if (mysps != null) { Connected = true; }
        }
        if (Connected)
        {
            stream = new SerialPort(port, 9600);

            if (!stream.IsOpen)
            {
                print("Opening " + port + ", baud 9600");
                stream.Open();
                stream.ReadTimeout = 100;
                stream.Handshake = Handshake.None;
                if (stream.IsOpen) { print("Open"); }
                CancelInvoke("reconnectDevice");
            }
            else
            {
                stream.Close();
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Connected)
        {
            try
            {
                valuefromArduino = stream.ReadLine();
            }
            catch { }

        }
    }
}
