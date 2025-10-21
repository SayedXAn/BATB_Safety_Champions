using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Level1Controller : MonoBehaviour
{
    public GameObject mapPanel, levelEndPanel, bottomPanel;

    [Header("Progress Meter")]
    public GameObject progressMeter;
    public GameObject step1, step2, step3, step4, step5, step6;
    public Image step1Img, step2Img, step3Img, step4Img, step5Img, step6Img;
    public Sprite red, green, yellow;

    [Header("Task UI")]
    public GameObject task1Promt, task1, task2Promt, task2, task3Promt, task3, task4Promt, task4, task5Promt, task5, task6Promt, task6;

    [Header("Hint Detectors")]
    public GameObject[] rights;
    public GameObject[] wrongs;
    public GameObject[] points;

    [Header("Next Buttons")]
    public GameObject next1, next2, next3, next4, next5, next6;

    [Header("Score Objects")]
    public GameObject level1ScoreObj, level2ScoreObj, level3ScoreObj, level4ScoreObj, level5ScoreObj;

    int taskNum;

    [Header("Task Count")]

    [SerializeField] int task1TasksCount, task2TasksCount, task3TasksCount, task4TasksCount, task5TasksCount, task6TasksCount;
    int task1CompletedCount, task2CompletedCount, task3CompletedCount, task4CompletedCount, task5CompletedCount, task6CompletedCount;
    float task1NegativePoints, task2NegativePoints, task3NegativePoints, task4NegativePoints, task5NegativePoints, task6NegativePoints;

    public TMP_Text timerText;
    public GameObject taskTimerObj;
    Coroutine _taskTimerCoroutineRef;

    public GameObject goodJobPanel, backBtn;

    bool task2Loaded = false, task3Loaded = false, task4Loaded = false, task5Loaded = false, task6Loaded = false, deactivateCurrentTasks = false;

    void Start()
    {
        backBtn.SetActive(false);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // GameManager.Instance.Level1Score = 0;
        taskNum = 0;
        task1CompletedCount = 0;
        task2CompletedCount = 0;
        task3CompletedCount = 0;
        task4CompletedCount = 0;

        task1NegativePoints = 0;
        task2NegativePoints = 0;
        task3NegativePoints = 0;
        task4NegativePoints = 0;

        // task1Loaded = false;
        task2Loaded = false;
        task3Loaded = false;
        task4Loaded = false;

        // next1.SetActive(false);
        // next2.SetActive(false);
        // next3.SetActive(false);
        // next4.SetActive(false);


        if(GameManager.Instance.GameStarted)
        {
            ShowScore();
            bottomPanel.SetActive(true);
        }
    }

    #region Task 1

    public void OnTask1NextClicked()
    {
        Task1();
    }
    void Task1()
    {
        GameManager.Instance.Level1Score = 0;
        GameManager.Instance.GameStarted = true;
        task1Promt.SetActive(false);
        task1.SetActive(true);
        TaskCountStarsManager.Instance.InitiateStars(task1TasksCount);
        deactivateCurrentTasks = false;

        progressMeter.SetActive(true);

        taskNum = 1;
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task1TasksCount * 5));
    }

    public IEnumerator LoadTask2()
    {
        if(task2Loaded)
        {
            yield break;
        }
        task2Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        TaskCountStarsManager.Instance.ClearStars();
        
        taskTimerObj.SetActive(false);

        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        // Debug.Log("Task 2 is loading");
        task1.SetActive(false);
        //progressMeter.SetActive(false);
        task2Promt.SetActive(true);

        if (task1CompletedCount == 0)
            step1Img.sprite = red;
        else if (task1NegativePoints > 0)
            step1Img.sprite = yellow;
        else if (task1CompletedCount == task1TasksCount)
            step1Img.sprite = green;
        else
            step1Img.sprite = yellow;

        step1.SetActive(true);
    }

    #endregion

    #region Task 2

    public void OnTask2NextClicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        Task2();
    }
    void Task2()
    {
        task2Promt.SetActive(false);
        TaskCountStarsManager.Instance.InitiateStars(task2TasksCount);
        deactivateCurrentTasks = false;
        task2.SetActive(true);

        taskNum = 2;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task2TasksCount * 5));
    }

    public IEnumerator LoadTask3()
    {
        if(task3Loaded)
        {
            yield break;
        }
        task3Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        TaskCountStarsManager.Instance.ClearStars();
        taskTimerObj.SetActive(false);

        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        // Debug.Log("Task 3 is loading");
        task2.SetActive(false);
        //progressMeter.SetActive(false);
        task3Promt.SetActive(true);

        if (task2CompletedCount == 0)
            step2Img.sprite = red;
        else if (task2NegativePoints > 0)
            step2Img.sprite = yellow;
        else if (task2CompletedCount == task2TasksCount)
            step2Img.sprite = green;
        else
            step2Img.sprite = yellow;

        step2.SetActive(true);
    }

    #endregion

    #region Task 3

    public void OnTask3NextClicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        Task3();
    }
    void Task3()
    {
        task3Promt.SetActive(false);
        TaskCountStarsManager.Instance.InitiateStars(task3TasksCount);
        deactivateCurrentTasks = false;
        task3.SetActive(true);

        taskNum = 3;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task3TasksCount * 5));
    }

    public IEnumerator LoadTask4()
    {
        if(task4Loaded)
        {
            yield break;
        }
        task4Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();

        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        task3.SetActive(false);
        //progressMeter.SetActive(false);
        task4Promt.SetActive(true);

        if (task3CompletedCount == 0)
            step3Img.sprite = red;
        else if (task3NegativePoints > 0)
            step3Img.sprite = yellow;
        else if (task3CompletedCount == task3TasksCount)
            step3Img.sprite = green;
        else
            step3Img.sprite = yellow;

        step3.SetActive(true);
        progressMeter.SetActive(true);
    }

    #endregion

    #region Task 4

    public void OnTask4NextClicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        Task4();
    }
    void Task4()
    {
        task4Promt.SetActive(false);
        task4.SetActive(true);
        TaskCountStarsManager.Instance.InitiateStars(task4TasksCount);
        deactivateCurrentTasks = false;

        taskNum = 4;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task4TasksCount * 5));
    }
    public IEnumerator LoadTask5()
    {
        if (task5Loaded)
        {
            yield break;
        }
        task5Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();

        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        task4.SetActive(false);
        //progressMeter.SetActive(false);
        task5Promt.SetActive(true);

        if (task4CompletedCount == 0)
            step4Img.sprite = red;
        else if (task4NegativePoints > 0)
            step4Img.sprite = yellow;
        else if (task4CompletedCount == task4TasksCount)
            step4Img.sprite = green;
        else
            step4Img.sprite = yellow;

        step4.SetActive(true);
        progressMeter.SetActive(true);
    }

    #endregion

    #region Task 5

    public void OnTask5NextClicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        Task5();
    }
    void Task5()
    {
        task5Promt.SetActive(false);
        task5.SetActive(true);
        TaskCountStarsManager.Instance.InitiateStars(task5TasksCount);
        deactivateCurrentTasks = false;

        taskNum = 5;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(/*task5TasksCount * 5*/ 10)); //The feedback was to increase time
    }

    public IEnumerator LoadTask6()
    {
        if (task6Loaded)
        {
            yield break;
        }
        task6Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();

        // yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        task5.SetActive(false);
        //progressMeter.SetActive(false);
        task6Promt.SetActive(true);

        if (task5CompletedCount == 0)
            step5Img.sprite = red;
        else if (task5NegativePoints > 0)
            step5Img.sprite = yellow;
        else if (task5CompletedCount == task5TasksCount)
            step5Img.sprite = green;
        else
            step5Img.sprite = yellow;

        step5.SetActive(true);
        progressMeter.SetActive(true);
    }

    #endregion

    #region Task 6

    public void OnTask6NextClicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        Task6();
    }
    void Task6()
    {
        task6Promt.SetActive(false);
        task6.SetActive(true);
        TaskCountStarsManager.Instance.InitiateStars(task6TasksCount);
        deactivateCurrentTasks = false;

        taskNum = 6;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task6TasksCount * 5));
    }
    

    #endregion

    #region Hints Handling
    public void OnRightHintClicked(GameObject obj)
    {
        if(deactivateCurrentTasks)
        {
            return;
        }
        AudioManager.instance.PlaySound("right");
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
        if (taskNum == 5)
            task5CompletedCount++;
        if (taskNum == 6)
            task6CompletedCount++;

        GameManager.Instance.Level1Score++;

        //Debug.Log(taskHintCount);

        right.SetActive(true);
        points.SetActive(true);

        StartCoroutine(WaitForRightWrong(right));
    }

    public void OnWrongHintClicked(GameObject obj)
    {
        if(deactivateCurrentTasks)
        {
            return;
        }
        AudioManager.instance.PlaySound("wrong");
        GameObject wrong = obj.transform.GetChild(0).gameObject;
        GameObject point = obj.transform.GetChild(1).gameObject;
        obj.GetComponent<Button>().interactable = false;

        GameManager.Instance.Level1Score -= 0.25f;

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

        wrong.SetActive(true);
        point.SetActive(true);
        StartCoroutine(WaitForRightWrong(obj));
    }

    public void OnQuestionAnswerSelect(bool correct)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        if(correct)
        {
            AudioManager.instance.PlaySound("right");
            TaskCountStarsManager.Instance.FillStar();
            if (taskNum == 5)
                task5CompletedCount++;

            GameManager.Instance.Level1Score++;            
        }
        else
        {
            AudioManager.instance.PlaySound("wrong");
            GameManager.Instance.Level1Score -= 0.25f;

            if (taskNum == 5)
                task5NegativePoints += 0.25f;
        }
        StartCoroutine(LoadNextTask());
    }
    IEnumerator LoadNextTask()
    {
        yield return new WaitForSeconds(2f);
        //Debug.Log("Task 5 Completed");
        StartCoroutine(LoadTask6());
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
            StartCoroutine(LoadTask3());
            // next2.SetActive(true);
        }
        else if (taskNum == 3 && task3CompletedCount == task3TasksCount)
        {
            // goodJobPanel.SetActive(true);
            StartCoroutine(LoadTask4());
            // next3.SetActive(true);
        }
        else if (taskNum == 4 && task4CompletedCount == task4TasksCount)
        {
            // goodJobPanel.SetActive(true);
            StartCoroutine(LoadTask5());
            // next4.SetActive(true);
        }
        else if (taskNum == 5 && task5CompletedCount == task5TasksCount)
        {
            // goodJobPanel.SetActive(true);
            StartCoroutine(LoadTask6());
            // next4.SetActive(true);
        }
        else if (taskNum == 6 && task6CompletedCount == task6TasksCount)
        {
            if (task6CompletedCount == 0)
                step6Img.sprite = red;
            else if (task6NegativePoints > 0)
                step6Img.sprite = yellow;
            else if (task6CompletedCount == task6TasksCount)
                step6Img.sprite = green;
            else
                step6Img.sprite = yellow;

            step6.SetActive(true);
            progressMeter.SetActive(true);
            LevelCompleted();
        }
    }

    #endregion

    public void OnMapButtonClicked()
    {
        GameManager.Instance.Level1Score = 0; 
        mapPanel.SetActive(false);
        task1Promt.SetActive(true);
        backBtn.SetActive(true);
    }

    public void OnMapSkipClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSkipClicked()
    {
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
    }

    public void LevelCompleted()
    {
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        TaskCountStarsManager.Instance.ClearStars();
        taskTimerObj.SetActive(false);
   

        StartCoroutine(WaitForLevelCompletion());
    }

    IEnumerator WaitForLevelCompletion()
    {
        TaskCountStarsManager.Instance.ClearStars();
        if (task6CompletedCount == 0)
            step6Img.sprite = red;
        else if (task6NegativePoints > 0)
            step6Img.sprite = yellow;
        else if (task6CompletedCount == task6TasksCount)
            step6Img.sprite = green;
        else
            step6Img.sprite = yellow;

        step6.SetActive(true);

        yield return new WaitForSeconds(2f);
        goodJobPanel.SetActive(false);
        task6.SetActive(false);
        progressMeter.SetActive(false);
        levelEndPanel.SetActive(true);
    }

    IEnumerator TaskTimerCoroutine(int time=25)
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
            deactivateCurrentTasks = true;
        }
        else if (taskNum == 2)
        {
            StartCoroutine(LoadTask3());
            // next2.SetActive(true);
            deactivateCurrentTasks = true;
        }
        else if (taskNum == 3)
        {
            StartCoroutine(LoadTask4());
            // next3.SetActive(true);
            deactivateCurrentTasks = true;
        }
        else if (taskNum == 4)
        {
            StartCoroutine(LoadTask5());
            // next3.SetActive(true);
            deactivateCurrentTasks = true;
        }
        else if (taskNum == 5)
        {
            StartCoroutine(LoadTask6());
            // next3.SetActive(true);
            deactivateCurrentTasks = true;
        }
        else if (taskNum == 6)
        {
            LevelCompleted();
            next6.SetActive(true);
            deactivateCurrentTasks = true;
        }
    }

    public void OnNextClicked()
    {
        SceneManager.LoadScene(1);
        //Application.Quit();
    }

    public void OnBackBtnClicked()
    {
        GameManager.Instance.OnBackBtnClicked();
    }

    public void ShowScore()
    {
            level1ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level1Score/GameManager.Instance.Level1TotalScore) * 100).ToString("0.00") + "%";
            level1ScoreObj.gameObject.SetActive(true);

            level2ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level2Score/GameManager.Instance.Level2TotalScore) * 100).ToString("0.00") + "%";
            level2ScoreObj.gameObject.SetActive(true);

            level3ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level3Score/GameManager.Instance.Level3TotalScore) * 100).ToString("0.00") + "%";
            level3ScoreObj.gameObject.SetActive(true);

            level4ScoreObj.GetComponentInChildren<TMP_Text>().text =   ((GameManager.Instance.Level4Score/GameManager.Instance.Level4TotalScore) * 100).ToString("0.00") + "%";
            level4ScoreObj.gameObject.SetActive(true);

            level5ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level5Score/GameManager.Instance.Level5TotalScore) * 100).ToString("0.00") + "%";
            level5ScoreObj.gameObject.SetActive(true);
    }

    public void ResetAll()
    {
        GameManager.Instance.ResetScores();
        SceneManager.LoadScene(0);
    }
}
