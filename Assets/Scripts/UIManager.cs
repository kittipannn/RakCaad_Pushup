using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Stopwatch stopwatch;
    [SerializeField] PushupSystem pushupSystem;
    [Header("Main")]
    [SerializeField] GameObject warningPanel;
    [SerializeField] Button practiceBtn;
    [SerializeField] Button freestyleBtn;

    [Header("Practice")]
    [SerializeField] GameObject practicePanel;
    [SerializeField] Image practicePanelBtn;
    [SerializeField] Button backPracBtn;
    [SerializeField] TMP_InputField minutesinputField;
    [SerializeField] TMP_InputField secondsinputField;
    int minutes, seconds;
    [SerializeField] TMP_InputField CountinputField;
    int count = 0;
    [SerializeField] GameObject inputFields;
    [SerializeField] TMP_Text timePractxt;
    [SerializeField] TMP_Text countPractxt;
    [SerializeField] Button giveUpBtn;
    [SerializeField] int minutesDefualt = 2, secondsDefualt = 0 , countDefualt = 22;
    [SerializeField] Button resetBtn;
    [SerializeField] Button startPracBtn;

    [Header("Freestyle")]
    [SerializeField] Button backFreeBtn;
    [SerializeField] Image freePanelBtn;
    [SerializeField] TMP_Text timeFreetxt;
    [SerializeField] TMP_Text countFreetxt;
    [SerializeField] GameObject freestylePanel;
    [SerializeField] Button startFreeBtn;
    [SerializeField] GameObject BtnplayFreeUI;
    [SerializeField] Button stopFreeBtn;
    [SerializeField] Button pauseFreeBtn;
    bool pause = false;
    [Header("CountdownTime")]
    [SerializeField] GameObject countdownPanel;
    [SerializeField] TMP_Text countdownText;
    float timeCountdown = 4;
    [Header("ResultPanel")]
    [SerializeField] GameObject resultPanel;
    [SerializeField] TMP_Text timeResulttxt;
    [SerializeField] TMP_Text paceResulttxt;
    [SerializeField] GameObject pracResult;
    [SerializeField] TMP_Text resultPractxt;
    [SerializeField] GameObject freeResult;
    [SerializeField] TMP_Text countResultFreetxt;
    [SerializeField] Button homeBtn;
    [SerializeField] Image symbol;

    void Start()
    {
        setBtn();

        EventsInApp.appEvents.onPractice += practiceMode;
        EventsInApp.appEvents.onFreestyle += freestyleMode;
        EventsInApp.appEvents.onStartExercise += setUIInPanelPlay;
        EventsInApp.appEvents.onStartExercise += numOfCount;
        EventsInApp.appEvents.onPause += setPauseBtn;
        EventsInApp.appEvents.onFinishPractice += showResult;
        EventsInApp.appEvents.onTimeOut += showResult;

    }
    void setBtn() 
    {
        //Main
        practiceBtn.onClick.AddListener(() => EventsInApp.appEvents.practice());
        freestyleBtn.onClick.AddListener(() => EventsInApp.appEvents.freestyle());
        
        //Practice
        resetBtn.onClick.AddListener(() => OnResetBtn());
        backPracBtn.onClick.AddListener(() => EventsInApp.appEvents.home());
        startPracBtn.onClick.AddListener(() => InvokeRepeating("OnCountDown", 0, 1));
        giveUpBtn.onClick.AddListener(() => showResult());
        //Freestyle
        backFreeBtn.onClick.AddListener(() => EventsInApp.appEvents.home());
        startFreeBtn.onClick.AddListener(() => InvokeRepeating("OnCountDown", 0, 1));
        pauseFreeBtn.onClick.AddListener(() => EventsInApp.appEvents.Pause());
        stopFreeBtn.onClick.AddListener(() => showResult());
        //result
        homeBtn.onClick.AddListener(() => EventsInApp.appEvents.home());
    }

    void setUIInPanelPlay() 
    {
        if (Manager.appManager.mode == 0)
        {
            inputFields.SetActive(false);
            startPracBtn.gameObject.SetActive(false);
            resetBtn.gameObject.SetActive(false);
            timePractxt.gameObject.SetActive(true);
            countPractxt.gameObject.SetActive(true);
            giveUpBtn.gameObject.SetActive(true);
        }
        else
        {
            BtnplayFreeUI.SetActive(true);
            startFreeBtn.gameObject.SetActive(false);
        }
    }
    void OnCountDown()  // invoke ใน Countdown Event
    {
        countdownPanel.SetActive(true);
        timeCountdown--;
        countdownText.text = timeCountdown.ToString();
        if (timeCountdown < 1)
        {
            countdownText.text = "Start";
            backFreeBtn.gameObject.SetActive(false);
            backPracBtn.gameObject.SetActive(false);
            StartCoroutine(delayCountdown());
        }
    }

    IEnumerator delayCountdown()
    {
        yield return new WaitForSeconds(0.5f);
        CancelInvoke("OnCountDown");
        setColorPanel(Manager.appManager.mode);
        countdownPanel.SetActive(false);

        Debug.Log("Start Game");
        EventsInApp.appEvents.StartExercise();
    }
    void setColorPanel(int mode) 
    {
        if (mode == 0)
        {
            Sprite sprite = Resources.Load<Sprite>("UI/Panel/Pink");
            practicePanel.GetComponent<Image>().sprite = sprite;
            sprite = Resources.Load<Sprite>("UI/Shape/Train_White");
            Debug.Log(sprite);
            practicePanelBtn.sprite = sprite;
            practicePanelBtn.gameObject.GetComponentInChildren<TMP_Text>().color = new Color32(255, 128, 178, 255); ;

        }
        else
        {
            Sprite sprite = Resources.Load<Sprite>("UI/Panel/Blue");
            freestylePanel.GetComponent<Image>().sprite = sprite;
            sprite = Resources.Load<Sprite>("UI/Shape/Train_White");
            freePanelBtn.sprite = sprite;
            freePanelBtn.gameObject.GetComponentInChildren<TMP_Text>().color = new Color32(85, 153, 255, 255);
        }
        timePractxt.color = Color.white;
        countPractxt.color = Color.white;
        timeFreetxt.color = Color.white;
        countFreetxt.color = Color.white;
    }
    void setPauseBtn()
    {
        pause = !pause;
        if (pause)
        {
            pauseFreeBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Button/PlayBlue");
        }
        else
        {
            pauseFreeBtn.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Button/Pause");
        }

    }
    void Update()
    {
        timePractxt.text = stopwatch.displayTimer();
        countPractxt.text = pushupSystem.Count.ToString();
        timeFreetxt.text = stopwatch.displayTimer();
        countFreetxt.text = pushupSystem.Count.ToString();

    }
    void practiceMode()
    {
        Manager.appManager.mode = 0;
        practicePanel.SetActive(true);
        minutesinputField.onEndEdit.AddListener(delegate { checkTime(minutesinputField); });
        secondsinputField.onEndEdit.AddListener(delegate { checkTime(secondsinputField); });
        CountinputField.onEndEdit.AddListener(delegate { checkCount(CountinputField); });
        setTimeAndCount();
    }
    void setTimeAndCount() 
    {
        int min, sec;
        min = PlayerPrefs.GetInt("minutes", minutesDefualt);
        sec = PlayerPrefs.GetInt("seconds", secondsDefualt);
        minutesinputField.text = string.Format("{0:00} ", min);
        secondsinputField.text = string.Format("{0:00} ", sec);
        CountinputField.text = PlayerPrefs.GetInt("count", countDefualt).ToString();
        minutes = int.Parse(minutesinputField.text);
        seconds = int.Parse(secondsinputField.text);
        count = int.Parse(CountinputField.text);
    }
    void freestyleMode()
    {
        Manager.appManager.mode = 1;
        freestylePanel.SetActive(true);
    }
    void checkTime(TMP_InputField input)
    {
        if (input == minutesinputField)
        {
            minutes = int.Parse(input.text);
            if (minutes > 60)
                minutes = 60;
            input.text = string.Format("{0:00} ", minutes);
            PlayerPrefs.SetInt("minutes", minutes);
        }
        else
        {
            seconds = int.Parse(input.text);
            if (seconds >= 60)
                seconds = 0;
            if (minutes == 60 && seconds > 0)
                seconds = 0;
            input.text = string.Format("{0:00} ", seconds);
            PlayerPrefs.SetInt("seconds", seconds);
        }
    }
    void checkCount(TMP_InputField input) 
    {
        count = int.Parse(input.text);
        if (count >= 100)
            count = 100;
        input.text = count.ToString(); ;
        PlayerPrefs.SetInt("count", count);
    }
    public int timeCount()
    {
        int time = minutes * 60;
        time += seconds;
        return time;
    }

    public void numOfCount() 
    {
        pushupSystem.Count = count;
        stopwatch.startcount = count;
    }
    void showResult()
    {
        Manager.appManager.play = false;
        resultPanel.SetActive(true);
        timeResulttxt.text = stopwatch.displayTimer();
        paceResulttxt.text = stopwatch.speedPersec();
        switch (Manager.appManager.mode)
        {
            case 0:
                Manager.appManager.OnResult();
                countResultFreetxt.text = count.ToString();
                symbol.sprite = ResultPractice();
                break;
            case 1:
                countResultFreetxt.text = pushupSystem.Count.ToString();
                GameObject.Find("HomeBtn").GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Button/HomeBlue");
                TMP_Text text = GameObject.Find("TextResult").GetComponent<TMP_Text>();
                text.text = "As Many As Possible";
                text.color = new Color32(85, 153, 255, 255);
                break;
        }

    }
    Sprite ResultPractice()
    {
        Sprite resultStr;
        string name = "Pass";
        bool resultBool = Manager.appManager.resultPractice;
        if (!resultBool)
        {
            resultPanel.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Panel/Red");
            name = "Fail";
        }
        resultStr = Resources.Load<Sprite>("UI/Shape/"+ name);
        return resultStr;
    }
    void OnResetBtn()
    {
        minutes = minutesDefualt;
        seconds = secondsDefualt;
        count = countDefualt;
        PlayerPrefs.SetInt("minutes", minutes);
        PlayerPrefs.SetInt("seconds", seconds);
        PlayerPrefs.SetInt("count", count);
        minutesinputField.text = string.Format("{0:00} ", minutes);
        secondsinputField.text = string.Format("{0:00} ", seconds);
        CountinputField.text = count.ToString();
    }

    public void warning() 
    {
        warningPanel.SetActive(true);
    }
}
