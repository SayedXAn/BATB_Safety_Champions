using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class Level3Controller : MonoBehaviour
{
    public GameObject mapPanel, levelEndPanel;

    [Header("Progress Meter")]
    public GameObject progressMeter;
    public GameObject step1, step2, step3, step4, step5, step6;
    public Image step1Img, step2Img, step3Img, step4Img, step5Img, step6Img;
    public Sprite red, green, yellow;

    [Header("Task UI")]
    public GameObject task1Promt;
    public GameObject task1, task2Promt, task2, task3Promt, task3, task4Promt, task4, task5Promt, task5, task6Promt, task6;

    [Header("Task End")]
    public GameObject task1End, task2End, task3End, task4End, task5End, task6End;


    public Button task3Next;
    public Text answerText;

    [Header("Hint Detectors")]
    public GameObject[] rights;
    public GameObject[] wrongs;
    public GameObject[] points;

    [HideInInspector]
    public int taskNum;

    [HideInInspector]
    public int task1HintCount, task2HintCount, task3HintCount, task4HintCount, task5HintCount, task6HintCount;
    public int task1TasksCount, task2TasksCount, task3TasksCount, task4TasksCount, task5TasksCount, task6TasksCount;
    public float task1NegativePoints, task2NegativePoints, task3NegativePoints, task4NegativePoints, task5NegativePoints, task6NegativePoints;
    [Header("Next Buttons")]
    public GameObject next1, next2, next3, next4, next5, next6;

    public Button option1Btn, option2Btn, option3Btn;

    public TMP_Text timerText;
    public GameObject taskTimerObj;

    Coroutine _taskTimerCoroutineRef;

    public GameObject goodJobPanel, backBtn;

    bool task2Loaded = false, task3Loaded = false, task4Loaded = false, task5Loaded = false, task6Loaded = false, deactivateCurrentTasks = false;

    public GameObject level1ScoreObj, level2ScoreObj, level3ScoreObj, level4ScoreObj, level5ScoreObj;

    GameManager gameManager;
    public PostScore postScore;
    public Text timeText;
    public Text scoreText;

    void Start()
    {
        backBtn.SetActive(false);
        taskNum = 0;
        task1HintCount = 0;
        task2HintCount = 0;
        task3HintCount = 0;
        task4HintCount = 0;
        task5HintCount = 0;
        task6HintCount = 0;

        task2Loaded = false;
        task3Loaded = false;
        task4Loaded = false;
        task5Loaded = false;
        task6Loaded = false;

        // next1.SetActive(false);
        next2.SetActive(false);
        // next3.SetActive(false);
        // next4.SetActive(false);

        gameManager = GameManager.Instance;
        ShowScore();
    }

    #region Task 1

    public void OnTask1NextClicked()
    {
        Task1();
    }
    void Task1()
    {
        GameManager.Instance.Level3Score = 0;
        task1Promt.SetActive(false);
        task1.SetActive(true);

        progressMeter.SetActive(true);

        taskNum = 1;
        TaskCountStarsManager.Instance.InitiateStars(task1TasksCount);
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task1TasksCount * 5));
    }

    public IEnumerator LoadTask2()
    {
        task1End.SetActive(true);
        // yield return new WaitForSeconds(1f);
        task1End.SetActive(false);
        if (task2Loaded)
        {
            yield break;
        }

        task2Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        TaskCountStarsManager.Instance.ClearStars();

        // if (task1HintCount == 0)
        //     step1Img.sprite = red;
        // else if (task1HintCount == 1)
        //     step1Img.sprite = yellow;
        // else if (task1HintCount == 2)
        // {
        //     step1Img.sprite = green;
        //     goodJobPanel.SetActive(true);
        // }
        if (task1HintCount > 0 && task1NegativePoints > 0)
        {
            step1Img.sprite = yellow;
        }
        else if (task1HintCount == task1TasksCount)
        {
            step1Img.sprite = green;
        }
        else if (task1HintCount == 0)
        {
            step1Img.sprite = red;
        }
        else
        {
            step1Img.sprite = yellow;
        }

        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        Debug.Log("Task 2 is loading");
        task1.SetActive(false);
        //progressMeter.SetActive(false);
        task2Promt.SetActive(true);

        step1.SetActive(true);
    }

    #endregion

    #region Task 2

    public void OnTask2NextClicked()
    {
        Task2();
    }
    void Task2()
    {
        next2.SetActive(true);
        task2Promt.SetActive(false);
        task2.SetActive(true);

        taskNum = 2;
        TaskCountStarsManager.Instance.InitiateStars(task2TasksCount);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task2TasksCount * 5));
        deactivateCurrentTasks = false;
    }

    public IEnumerator LoadTask3()
    {
        if (task3Loaded)
        {
            yield break;
        }

        task3Loaded = true;

        Debug.Log("task 3 loading");
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        TaskCountStarsManager.Instance.ClearStars();

        if (task2HintCount > 0 && task2NegativePoints > 0)
        {
            step2Img.sprite = yellow;
        }
        else if (task2HintCount == task2TasksCount)
        {
            step2Img.sprite = green;
        }
        else if (task2HintCount == 0)
        {
            step2Img.sprite = red;
        }
        else
        {
            step2Img.sprite = yellow;
        }

        // yield return new WaitForSeconds(2f);
        // Debug.Log("Task 3 is loading");
        goodJobPanel.SetActive(false);
        step2.SetActive(true);
        task2.SetActive(false);
        //progressMeter.SetActive(false);
        task3.SetActive(true);
        yield return new WaitForSeconds(5f);
        task3.SetActive(false);
        task3Promt.SetActive(true);

        taskNum = 3;

        Debug.Log("Task 3 load called multiple times?");
        deactivateCurrentTasks = false;
        TaskCountStarsManager.Instance.InitiateStars(task3TasksCount);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task3TasksCount * 5));
    }

    #endregion

    #region Task 3

    public void OnOption3Clicked(GameObject btn)
    {
        // btn.GetComponent<Animation>().Play();
        Color c = Color.red;
        c.a = 0.5f;

        btn.GetComponent<Image>().color = c;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound("wrong");
        }
        //answer is wrong
        answerText.text = "mwVK DËit dv÷© GBW > W±i WvKv > wi‡cvU© (cÖ_g Ackb)";
        GameManager.Instance.Level3Score -= 0.25f;
        // task3Next.interactable = true;
        //DisableAllOptions();
    }
    public void OnOption2Clicked(GameObject btn)
    {
        Color c = Color.red;
        c.a = 0.5f;
        btn.GetComponent<Image>().color = c;
        // btn.GetComponent<Animation>().Play();
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound("wrong");
        }
        GameManager.Instance.Level3Score -= 0.25f;
        //answer is wrong
        answerText.text = "mwVK DËit dv÷© GBW > W±i WvKv > wi‡cvU© (cÖ_g Ackb)";
        // task3Next.interactable = true;
        //DisableAllOptions();
    }

    public void OnOption1Clicked(GameObject btn)
    {
        Color c = Color.green;
        c.a = 0.5f;
        TaskCountStarsManager.Instance.FillStar();
        btn.GetComponent<Image>().color = c;
        // btn.GetComponent<Animation>().Play();
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound("right");
        }
        answerText.text = "";
        GameManager.Instance.Level3Score += 1;
        // task3Next.interactable = true;
        task3HintCount++;
        DisableAllOptions();
    }

    void DisableAllOptions()
    {
        next3.SetActive(true);
        option1Btn.interactable = false;
        option2Btn.interactable = false;
        option3Btn.interactable = false;
    }
    public void OnTask3NextClicked()
    {
        StartCoroutine(LoadTask4());
    }
    #endregion

    #region Task 4

    IEnumerator LoadTask4()
    {
        if (task4Loaded)
        {
            yield break;
        }

        task4Loaded = true;
        Debug.Log("Loading 4");
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        TaskCountStarsManager.Instance.ClearStars();

        //progressMeter.SetActive(false);

        if (task3HintCount > 0 && task3NegativePoints > 0)
        {
            step3Img.sprite = yellow;
        }
        else if (task3HintCount == task3TasksCount)
        {
            step3Img.sprite = green;
        }
        else if (task3HintCount == 0)
        {
            step3Img.sprite = red;
        }
        else
        {
            step3Img.sprite = yellow;
        }

        step3.SetActive(true);
        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        task3Promt.SetActive(false);
        task4Promt.SetActive(true);
        progressMeter.SetActive(true);
        taskNum = 4;
        deactivateCurrentTasks = false;
        TaskCountStarsManager.Instance.InitiateStars(task4TasksCount);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task4TasksCount * 5));
    }



    #endregion

    #region Task 5

    void Task5()
    {
        GameManager.Instance.Level3Score = 0;
        task5Promt.SetActive(false);
        task5.SetActive(true);

        progressMeter.SetActive(true);

        taskNum = 5;
        TaskCountStarsManager.Instance.InitiateStars(task5TasksCount);
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task5TasksCount * 5));
    }
    public void OnTask5NextClicked()
    {
        //StartCoroutine(LoadTask5());
        Task5();
    }

    IEnumerator LoadTask5()
    {
        if (task5Loaded)
        {
            yield break;
        }

        task5Loaded = true;
        Debug.Log("Loading 5");
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        TaskCountStarsManager.Instance.ClearStars();

        //progressMeter.SetActive(false);

        if (task4HintCount > 0 && task4NegativePoints > 0)
        {
            step4Img.sprite = yellow;
        }
        else if (task4HintCount == task4TasksCount)
        {
            step4Img.sprite = green;
        }
        else if (task4HintCount == 0)
        {
            step4Img.sprite = red;
        }
        else
        {
            step4Img.sprite = yellow;
        }

        step4.SetActive(true);
        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        task5Promt.SetActive(true);
        task5.SetActive(false);
        progressMeter.SetActive(true);
        taskNum = 5;
        deactivateCurrentTasks = false;
        
    }

    #endregion

    #region Task 6  
    public void OnTask6NextClicked()
    {
        StartCoroutine(LoadTask6());
    }

    IEnumerator LoadTask6()
    {
        if (task6Loaded)
        {
            yield break;
        }

        task6Loaded = true;
        Debug.Log("Loading 6");
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        TaskCountStarsManager.Instance.ClearStars();

        //progressMeter.SetActive(false);

        if (task5HintCount > 0 && task5NegativePoints > 0)
        {
            step5Img.sprite = yellow;
        }
        else if (task5HintCount == task5TasksCount)
        {
            step5Img.sprite = green;
        }
        else if (task5HintCount == 0)
        {
            step5Img.sprite = red;
        }
        else
        {
            step5Img.sprite = yellow;
        }

        step5.SetActive(true);
        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        task6Promt.SetActive(false);
        task6.SetActive(true);
        progressMeter.SetActive(true);
        taskNum = 6;
        deactivateCurrentTasks = false;
        TaskCountStarsManager.Instance.InitiateStars(task5TasksCount);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task5TasksCount * 5));
    }

    #endregion

    #region Hints Handling
    public void OnRightHintClicked(GameObject go)
    {
        if (deactivateCurrentTasks) return;
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("right");

        TaskCountStarsManager.Instance.FillStar();

        go.GetComponentInParent<Button>().interactable = false;
        GameObject right = go.transform.GetChild(0).gameObject;
        GameObject point = go.transform.GetChild(1).gameObject;

        if (taskNum == 1)
            task1HintCount++;
        if (taskNum == 2)
            task2HintCount++;
        if (taskNum == 3)
            task3HintCount++;
        if (taskNum == 4)
            task4HintCount++;
        if (taskNum == 5)
            task5HintCount++;
        if (taskNum == 6)
            task6HintCount++;

        GameManager.Instance.Level3Score += 1;

        //Debug.Log(taskHintCount);

        right.SetActive(true);
        point.SetActive(true);

        StartCoroutine(WaitForRightWrong(go));
    }

    public void OnWrongHintClicked(GameObject go)
    {
        if (deactivateCurrentTasks) return;
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("wrong");

        go.GetComponentInParent<Button>().interactable = false;
        GameObject wrong = go.transform.GetChild(0).gameObject;
        GameObject point = go.transform.GetChild(1).gameObject;

        if (taskNum == 1)
            task1NegativePoints += 0.25f;
        if (taskNum == 2)
            task2NegativePoints += 0.25f;
        if (taskNum == 3)
            task3NegativePoints += 0.25f;
        if (taskNum == 4)
            task4NegativePoints += 0.25f;
        if (taskNum == 5)
            task5NegativePoints += 0.25f;
        if (taskNum == 6)
            task6NegativePoints += 0.25f;

        GameManager.Instance.Level3Score -= 0.25f;

        wrong.SetActive(true);
        point.SetActive(true);
        StartCoroutine(WaitForRightWrong(go));
    }
    IEnumerator WaitForRightWrong(GameObject obj)
    {
        if (taskNum == 4)
        {
            obj.GetComponent<Button>().interactable = false;
        }
        yield return new WaitForSeconds(2f);

        if (taskNum == 1)
        {
            if (task1HintCount == task1TasksCount)
            {
                // next1.SetActive(true);
                deactivateCurrentTasks = true;
                StartCoroutine(LoadTask2());
            }
        }

        if (taskNum == 2)
        {
            if (task2HintCount == task2TasksCount)
            {
                // next2.SetActive(true);
                deactivateCurrentTasks = true;
                StartCoroutine(LoadTask3());
            }
        }

        if (taskNum == 3)
        {
            if (task3HintCount == task3TasksCount)
            {
                // next3.SetActive(true);
                deactivateCurrentTasks = true;
                StartCoroutine(LoadTask4());
            }
        }

        if (taskNum == 4)
        {
            Debug.Log("Task 4 hint count: " + task4HintCount);
            if (task4HintCount == task4TasksCount)
            {
                // next4.SetActive(true);
                deactivateCurrentTasks = true;
                //StartCoroutine(WaitForLevelCompletion());
                StartCoroutine(LoadTask5());
            }
            yield break;
        }

        if (taskNum == 5)
        {
            Debug.Log("Task 5 hint count: " + task5HintCount);
            if (task5HintCount == task5TasksCount)
            {
                // next4.SetActive(true);
                deactivateCurrentTasks = true;
                //StartCoroutine(WaitForLevelCompletion());
                Debug.Log("Task 5 sesh");
                StartCoroutine(LoadTask6());
            }
            yield break;
        }
        if (taskNum == 6)
        {
            Debug.Log("Task 6 hint count: " + task6HintCount);
            if (task6HintCount == task6TasksCount)
            {
                // next4.SetActive(true);
                deactivateCurrentTasks = true;
                //StartCoroutine(WaitForLevelCompletion());
                Debug.Log("Task 5 sesh");
                //StartCoroutine(LoadTask6());
                StartCoroutine(WaitForLevelCompletion());
            }
            yield break;
        }

        obj.SetActive(false);

        // if (taskNum == 1 && task1HintCount == 2)
        //     StartCoroutine(LoadTask2());
        // else if (taskNum == 2 && task2HintCount == 3)
        //     StartCoroutine(LoadTask3());
    }

    #endregion

    public void OnMapButtonClicked()
    {
        GameManager.Instance.Level3Score = 0;
        next1.SetActive(true);
        mapPanel.SetActive(false);
        task1Promt.SetActive(true);
        backBtn.SetActive(true);
    }

    public void OnMapSkipClicked()
    {
        //SceneManager.LoadScene(3); //Game End
    }

    public void OnNextClicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        if (taskNum == 1)
        {
            StartCoroutine(LoadTask2());
        }
        else if (taskNum == 2)
        {
            StartCoroutine(LoadTask3());
        }
        else if (taskNum == 3)
        {
            StartCoroutine(LoadTask4());
        }
        else if (taskNum == 4)
        {
            StartCoroutine(LoadTask5());
        }
        else if (taskNum == 5)
        {
            StartCoroutine(LoadTask6());
        }
        else if (taskNum == 6)
        {
            LevelCompleted();
        }
    }

    public void LevelCompleted()
    {
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();

        // PlayerPrefs.SetInt("score", playerScore);

        StartCoroutine(WaitForLevelCompletion());
    }

    

    IEnumerator TaskTimerCoroutine(int time = 25)
    {
        taskTimerObj.SetActive(true);
        int i = time;
        while (i > 0)
        {
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1);
            i--;
        }
        timerText.text = "";
        taskTimerObj.SetActive(false);

        if (taskNum == 1)
        {
            StartCoroutine(LoadTask2());
            // next1.SetActive(true);
        }
        else if (taskNum == 2)
        {
            StartCoroutine(LoadTask3());
            // next2.SetActive(true);
        }
        else if (taskNum == 3)
        {
            StartCoroutine(LoadTask4());
            // next3.SetActive(true);
        }
        else if (taskNum == 4)
        {
            // next4.SetActive(true);
            //LevelCompleted();
            StartCoroutine(LoadTask5());
        }
        else if (taskNum == 5)
        {
            StartCoroutine(LoadTask6());
        }
        else if(taskNum == 6)
        {
            LevelCompleted();
        }

    }
    public void OnLevelEndNextClicked()
    {
        //SceneManager.LoadScene(3);//Game End
        //Application.Quit();
    }

    public void OnBackBtnClicked()
    {
        GameManager.Instance.OnBackBtnClicked();
    }

    public void ShowScore()
    {
        level1ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level1Score / GameManager.Instance.Level1TotalScore) * 100).ToString("0.00") + "%";
        level1ScoreObj.gameObject.SetActive(true);

        level2ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level2Score / GameManager.Instance.Level2TotalScore) * 100).ToString("0.00") + "%";
        level2ScoreObj.gameObject.SetActive(true);

        // level3ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level3Score / GameManager.Instance.Level3TotalScore) * 100).ToString("0.00") + "%";
        // level3ScoreObj.gameObject.SetActive(true);

        // level4ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level4Score / GameManager.Instance.Level4TotalScore) * 100).ToString("0.00") + "%";
        // level4ScoreObj.gameObject.SetActive(true);

        // level5ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level5Score / GameManager.Instance.Level5TotalScore) * 100).ToString("0.00") + "%";
        // level5ScoreObj.gameObject.SetActive(true);
    }

    public void ResetAll()
    {
        GameManager.Instance.ResetScores();
        SceneManager.LoadScene(0);
    }

    //IEnumerator WaitForLevelCompletion()
    //{
    //    yield return new WaitForSeconds(f);
    //    if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
    //    taskTimerObj.SetActive(false);
    //    TaskCountStarsManager.Instance.ClearStars();

    //    if (task6HintCount > 0 && task6NegativePoints > 0)
    //    {
    //        step6Img.sprite = yellow;
    //    }
    //    else if (task6HintCount == task6TasksCount)
    //    {
    //        step6Img.sprite = green;
    //    }
    //    else if (task6HintCount == 0)
    //    {
    //        step6Img.sprite = red;
    //    }
    //    else
    //    {
    //        step6Img.sprite = yellow;
    //    }
    //    step6.SetActive(true);

    //    yield return new WaitForSeconds(0.5f);

    //    task6Promt.SetActive(false);
    //    progressMeter.SetActive(false);
    //    levelEndPanel.SetActive(true);
    //    Debug.Log("Level 3 end reached");
    //}

    IEnumerator WaitForLevelCompletion()
    {
        
        yield return new WaitForSeconds(2f);
        goodJobPanel.SetActive(false);
        task6.SetActive(false);

        gameManager.gameEndTime = System.DateTime.Now;
        double totalScore = CalculateFinalScore();
        double roundedValue = Math.Round(totalScore, 2);
        scoreText.text = totalScore.ToString("F2") + "%";
        timeText.text = "Avcbvi †gvU mgq †j‡M‡Qt " + CalculateFinalTime();
        levelEndPanel.SetActive(true);

        postScore.CallPostAPI(gameManager.userID, gameManager.userName, totalScore);
    }

    float CalculateFinalScore()
    {
        float allLevelsScore = gameManager.Level1Score + gameManager.Level2Score + gameManager.Level3Score /*+ gameManager.Level4Score + gameManager.Level5Score*/;
        float allLevelsTotalScore = gameManager.Level1TotalScore + gameManager.Level2TotalScore + gameManager.Level3TotalScore /*+ gameManager.Level4TotalScore + gameManager.Level5TotalScore*/;
        return (allLevelsScore / allLevelsTotalScore) * 100;
    }

    string CalculateFinalTime()
    {
        string totalTime = (gameManager.gameEndTime - gameManager.gameStartTime).ToString();
        totalTime = totalTime.Substring(3, 2) + " wgwbU " + totalTime.Substring(6, 2) + " †m‡KÛ";
        return totalTime;
    }
}
