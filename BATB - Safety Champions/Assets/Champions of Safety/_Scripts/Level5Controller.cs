using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class Level5Controller : MonoBehaviour
{
    public GameObject mapPanel, levelEndPanel;

    [Header("Task UI")]

    int maxScore, taskNum;
    float percentage;
    public Text scoreText;
    public TMP_Text timerText;
    public GameObject taskTimerObj;
    Coroutine _taskTimerCoroutineRef;
    public GameObject goodJobPanel, backBtn;
    bool deactivateCurrentTasks = false;
    public GameObject progressMeter;
    public GameObject step1, step2, step3, step4;
    public Image step1Img, step2Img, step3Img, step4Img;
    public Sprite red, green, yellow;

    [Header("Task UI")]
    public GameObject task1Promt, task1, task2Promt, task2, task3Promt, task3, task4Promt, task4;

    [Header("Hint Detectors")]
    public GameObject[] rights;
    public GameObject[] wrongs;
    public GameObject[] points;

    [Header("Next Buttons")]
    public GameObject next1, next2, next3, next4;

    [Header("Score Objects")]
    public GameObject level1ScoreObj, level2ScoreObj, level3ScoreObj, level4ScoreObj, level5ScoreObj;


    [SerializeField] int task1TasksCount, task2TasksCount, task3TasksCount, task4TasksCount;
    public int task1CompletedCount, task2CompletedCount, task3CompletedCount, task4CompletedCount;
    public float task1NegativePoints, task2NegativePoints, task3NegativePoints, task4NegativePoints;

    public PostScore postScore;
    public Text timeText;

    // bool task2Loaded = false, task3Loaded = false, task4Loaded = false;
    GameManager gameManager;

    void Start()
    {
        backBtn.SetActive(false);
        // playerScore = PlayerPrefs.GetInt("score");
        maxScore = 40;


        ShowScore();
        gameManager = GameManager.Instance;

        //GameManager.Instance.gameStartTime = System.DateTime.Now; //for Testing only
    }

    #region Task 1
    public void Task1PromptNextClicked()
    {
        task1Promt.SetActive(false);
        TaskCountStarsManager.Instance.InitiateStars(task1TasksCount);
        task1.SetActive(true);
        Task1();
    }
    public void Task2PromptNextClicked()
    {
        task2Promt.SetActive(false);
        TaskCountStarsManager.Instance.InitiateStars(task2TasksCount);
        task2.SetActive(true);
        Task2();
    }
    #endregion


    public void OnRightHintClicked(GameObject obj)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound("right");
        }
        obj.GetComponent<Image>().color = new Color(255, 255, 255, 0); //Making the images alpha 0
        GameObject right = obj.transform.GetChild(0).gameObject;
        GameObject points = obj.transform.GetChild(1).gameObject;

        right.GetComponentInParent<Button>().interactable = false;
        TaskCountStarsManager.Instance.FillStar();

        if (taskNum == 1)
            task1CompletedCount++;
        if (taskNum == 2)
            task2CompletedCount++;
        if (taskNum == 3)
            task3CompletedCount++;
        if (taskNum == 4)
            task4CompletedCount++;

        GameManager.Instance.Level5Score++;

        //Debug.Log(taskHintCount);

        right.SetActive(true);
        points.SetActive(true);

        StartCoroutine(WaitForRightWrong(right));
    }

    public void OnWrongHintClicked(GameObject obj)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound("wrong");
        }
        GameObject wrong = obj.transform.GetChild(0).gameObject;
        GameObject point = obj.transform.GetChild(1).gameObject;
        obj.GetComponent<Button>().interactable = false;

        GameManager.Instance.Level5Score -= 0.25f;

        if (taskNum == 1)
            task1NegativePoints += 0.25f;
        if (taskNum == 2)
            task2NegativePoints += 0.25f;
        if (taskNum == 3)
            task3NegativePoints += 0.25f;
        if (taskNum == 4)
            task4NegativePoints += 0.25f;

        wrong.SetActive(true);
        point.SetActive(true);
        StartCoroutine(WaitForRightWrong(obj));
    }

    IEnumerator WaitForRightWrong(GameObject obj)
    {
        yield return new WaitForSeconds(2f);

        obj.SetActive(false);

        if (taskNum == 1 && task1CompletedCount == task1TasksCount)
        {
            // goodJobPanel.SetActive(true);

            StartCoroutine(LoadTask2());
            // next1.SetActive(true);

        }
        else if (taskNum == 2 && task2CompletedCount == task2TasksCount)
        {
            // goodJobPanel.SetActive(true);
            // StartCoroutine(LoadTask3());
            // next2.SetActive(true);
            LevelCompleted();
        }
        else if (taskNum == 3 && task3CompletedCount == task3TasksCount)
        {
            // goodJobPanel.SetActive(true);
            // StartCoroutine(LoadTask4());
            next3.SetActive(true);
        }
        else if (taskNum == 4 && task4CompletedCount == task4TasksCount)
        {
            // goodJobPanel.SetActive(true);
            // LevelCompleted();
            next4.SetActive(true);
        }
    }

    public void OnTask1NextClicked()
    {
        StartCoroutine(LoadTask2());
    }
    void Task1()
    {
        task1Promt.SetActive(false);
        task1.SetActive(true);
        TaskCountStarsManager.Instance.InitiateStars(task1TasksCount);
        deactivateCurrentTasks = false;

        progressMeter.SetActive(true);

        taskNum = 1;
        if (_taskTimerCoroutineRef != null)
        {
            StopCoroutine(_taskTimerCoroutineRef);
        }
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task1TasksCount * 5));
    }

    public void OnTask2NextClicked()
    {
        Task2();
    }

    IEnumerator LoadTask2()
    {
        if(task1CompletedCount == task1TasksCount && task1NegativePoints == 0)
        {
            step1Img.sprite = green;
            step1.SetActive(true);
        }
        else if((task1CompletedCount > 0 && task1CompletedCount < task1TasksCount) || task1NegativePoints > 0)
        {
            step1Img.sprite = yellow;
            step1.SetActive(true);
        }else
        {
            step1Img.sprite = red;
            step1.SetActive(true);
        }

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();
        yield return new WaitForSeconds(1f);
        task2Promt.SetActive(true);
    }
    void Task2()
    {
        task2Promt.SetActive(false);
        TaskCountStarsManager.Instance.InitiateStars(task2TasksCount);
        deactivateCurrentTasks = false;
        task2.SetActive(true);

        taskNum = 2;
        if (_taskTimerCoroutineRef != null)
        {
            StopCoroutine(_taskTimerCoroutineRef);
        }
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task2TasksCount * 5));
    }

    IEnumerator TaskTimerCoroutine(int time = 15)
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
            // next1.SetActive(true);
            deactivateCurrentTasks = true;
            StartCoroutine(LoadTask2());
        }
        else if (taskNum == 2)
        {
            // next2.SetActive(true);
            deactivateCurrentTasks = true;
            LevelCompleted();
        }
    }

    public void OnMapButtonClicked()
    {
        GameManager.Instance.Level5Score = 0;
        mapPanel.SetActive(false);
        task1Promt.SetActive(true);
        backBtn.SetActive(true);

        // if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        // _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine());
    }

    public void OnMapSkipClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void LevelCompleted()
    {
        if(task1CompletedCount == task1TasksCount && task1NegativePoints == 0)
        {
            step2Img.sprite = green;
            step2.SetActive(true);
        }
        else if((task1CompletedCount > 0 && task1CompletedCount < task1TasksCount) || task1NegativePoints > 0)
        {
            step2Img.sprite = yellow;
            step2.SetActive(true);
        }else
        {
            step2Img.sprite = red;
            step2.SetActive(true);
        }
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);

        TaskCountStarsManager.Instance.ClearStars();
        //PlayerPrefs.SetInt("score", playerScore);

        StartCoroutine(WaitForLevelCompletion());
    }

    IEnumerator WaitForLevelCompletion()
    {
        yield return new WaitForSeconds(2f);
        goodJobPanel.SetActive(false);
        task1.SetActive(false);

        GameManager.Instance.gameEndTime = System.DateTime.Now;


        // percentage = ((float)playerScore / (float)maxScore) * 100f;

        // percentage = Mathf.Clamp(percentage,0,100);
        double totalScore = CalculateFinalScore();
        double roundedValue = Math.Round(totalScore, 2);
        scoreText.text = totalScore.ToString("F2") + "%";
        timeText.text = "Avcbvi †gvU mgq †j‡M‡Qt " + CalculateFinalTime();
        levelEndPanel.SetActive(true);

        postScore.CallPostAPI(gameManager.userID, gameManager.userName, totalScore);
    }

    float CalculateFinalScore()
    {
        float allLevelsScore = gameManager.Level1Score + gameManager.Level2Score + gameManager.Level3Score + gameManager.Level4Score + gameManager.Level5Score;
        float allLevelsTotalScore = gameManager.Level1TotalScore + gameManager.Level2TotalScore + gameManager.Level3TotalScore + gameManager.Level4TotalScore + gameManager.Level5TotalScore;
        return (allLevelsScore / allLevelsTotalScore) * 100;
    }

    string CalculateFinalTime()
    {
        string totalTime = (gameManager.gameEndTime - gameManager.gameStartTime).ToString();
        totalTime = totalTime.Substring(3, 2) + " wgwbU " + totalTime.Substring(6, 2) + " †m‡KÛ";
        return totalTime;
    }



    public void OnNextClicked()
    {
        SceneManager.LoadScene(0);
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

        level3ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level3Score / GameManager.Instance.Level3TotalScore) * 100).ToString("0.00") + "%";
        level3ScoreObj.gameObject.SetActive(true);

        level4ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level4Score / GameManager.Instance.Level4TotalScore) * 100).ToString("0.00") + "%";
        level4ScoreObj.gameObject.SetActive(true);

        // level5ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level5Score / GameManager.Instance.Level5TotalScore) * 100).ToString("0.00") + "%";
        // level5ScoreObj.gameObject.SetActive(true);

    }

    public void ResetAll()
    {
        GameManager.Instance.ResetScores();
        SceneManager.LoadScene(0);
    }
}
