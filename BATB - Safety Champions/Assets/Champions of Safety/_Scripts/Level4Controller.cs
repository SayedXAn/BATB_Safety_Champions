using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Threading.Tasks;

public class Level4Controller : MonoBehaviour
{
    public GameObject mapPanel, levelEndPanel;

    [Header("Progress Meter")]
    public GameObject progressMeter;
    public GameObject step1, step2, step3;
    public Image step1Img, step2Img, step3Img;
    public Sprite red, green, yellow;

    [Header("Task UI")]
    public GameObject task1Promt;
    public GameObject task1, task2Promt, task2, task3, task3Promt;
    public VideoPlayer vp;
    public Button task1Next;
    public Text answerText;

    [Header("Hint Detectors")]
    public GameObject[] rights;
    public GameObject[] wrongs;
    public GameObject[] points;

    int taskNum;
    public int task1HintCount, task2HintCount, task3HintCount;
    public int task1TasksCount, task2TasksCount, task3TasksCount;
    public float task1NegativePoints, task2NegativePoints, task3NegativePoints;

    public TMP_Text timerText;
    public GameObject taskTimerObj;
    Coroutine _taskTimerCoroutineRef;

    public GameObject goodJobPanel, backBtn;

    public Button option1Btn, option2Btn, option3Btn;

    public GameObject next1, next2, next3;

    bool task2Loaded = false, deactivateCurrentTasks = false;
    public int task3DropCount = 0;
    public GameObject level1ScoreObj, level2ScoreObj, level3ScoreObj, level4ScoreObj, level5ScoreObj;

    void Start()
    {
        backBtn.SetActive(false);
        taskNum = 0;
        task1HintCount = 0;
        task2HintCount = 0;

        task2Loaded = false;

        vp.loopPointReached += Task1;

        ShowScore();
    }

    #region Task 1
    void Task1(VideoPlayer vp)
    {
        task1Promt.SetActive(false);
        task1.SetActive(true);

        progressMeter.SetActive(true);

        taskNum = 1;
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task1TasksCount * 5));
        deactivateCurrentTasks = false;
        TaskCountStarsManager.Instance.InitiateStars(task1TasksCount);
    }

    public void OnOptionASelected(GameObject btn)
    {
        // Color c = Color.red;
        // c.a = 0.5f;
        // btn.GetComponent<Image>().color = c;
        // btn.GetComponent<Animation>().Play();
        GameManager.Instance.Level4Score -= 0.25f;
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("wrong");
        //wrong answer
        // answerText.text = "Right Answer is B";
        task1Next.interactable = true;
        DisableAllOptions();
    }

    public void OnOptionBSelected(GameObject btn)
    {
        // Color c = Color.green;
        // c.a = 0.5f;

        // btn.GetComponent<Image>().color = c;
        // btn.GetComponent<Animation>().Play();
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("right");
        // answerText.text = "Right Answer is B";
        task1Next.interactable = true;
        // step1Img.sprite = green;
        // step1.SetActive(true);
        GameManager.Instance.Level4Score++;
        TaskCountStarsManager.Instance.FillStar();
        task1HintCount++;
        DisableAllOptions();
    }

    public void OnOptionCSelected(GameObject btn)
    {
        // Color c = Color.red;
        // c.a = 0.5f;
        // btn.GetComponent<Image>().color = c;
        // btn.GetComponent<Animation>().Play();
        GameManager.Instance.Level4Score -= 0.25f;
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("wrong");
        //wrong answer
        // answerText.text = "Right Answer is B";
        task1Next.interactable = true;
        DisableAllOptions();
    }

    void DisableAllOptions()
    {
        next1.SetActive(true);
        option1Btn.interactable = false;
        option2Btn.interactable = false;
        option3Btn.interactable = false;
    }

    public void OnTask1NextClicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        StartCoroutine(LoadTask2Promt());
    }
    IEnumerator LoadTask2Promt()
    {
        yield return new WaitForSeconds(0.5f);
        task2Promt.SetActive(true);
        task1.SetActive(false);
    }

    public void Task2StartButton()
    {
        StartCoroutine(LoadTask2());
    }


    public IEnumerator LoadTask2()
    {
        if (task2Loaded)
        {
            yield break;
        }
        task2Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        TaskCountStarsManager.Instance.ClearStars();

        if (task1HintCount < 1)
            step1Img.sprite = red;
        else
        {
            step1Img.sprite = green;
            // goodJobPanel.SetActive(true);
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Task 2 is loading");
        // goodJobPanel.SetActive(false);
        task1.SetActive(false);
        //progressMeter.SetActive(false);
        task2.SetActive(true);
        //task2Promt.SetActive(true);


        step1.SetActive(true);
        taskNum = 2;
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task2TasksCount * 5));
        deactivateCurrentTasks = false;
        TaskCountStarsManager.Instance.InitiateStars(task2TasksCount);
    }

    

    public void OnTask2NextClicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        //StartCoroutine(LoadTask3());
        StartCoroutine(LoadTask3Promt());
    }

    IEnumerator LoadTask3Promt()
    {
        yield return new WaitForSeconds(0.5f);
        task3Promt.SetActive(true);
        task2.SetActive(false);
    }

    public void Task3StartButton()
    {
        StartCoroutine(LoadTask3());
    }
    IEnumerator LoadTask3()
    {
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        TaskCountStarsManager.Instance.ClearStars();

        if (task2HintCount < task2TasksCount)
            step2Img.sprite = red;
        else
        {
            step2Img.sprite = green;
            // goodJobPanel.SetActive(true);
        }

        yield return new WaitForSeconds(1f);
        Debug.Log("Task 3 is loading");
        // goodJobPanel.SetActive(false);
        task2.SetActive(false);
        //progressMeter.SetActive(false);
        task2.SetActive(false);
        task3.SetActive(true);

        step2.SetActive(true);
        taskNum = 3;
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task3TasksCount * 5));
        deactivateCurrentTasks = false;
        TaskCountStarsManager.Instance.InitiateStars(task3TasksCount);
    }
    #endregion

    #region Hints Handling
    public void OnRightHintClicked(GameObject go)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("right");

        go.GetComponentInParent<Button>().interactable = false;
        TaskCountStarsManager.Instance.FillStar();
        GameObject right = go.transform.GetChild(0).gameObject;
        GameObject point = go.transform.GetChild(1).gameObject;

        right.GetComponentInParent<Button>().interactable = false;

        if (taskNum == 2)
            task2HintCount++;

        GameManager.Instance.Level4Score++;

        //Debug.Log(taskHintCount);

        right.SetActive(true);
        point.SetActive(true);

        StartCoroutine(WaitForRightWrong(go));
    }

    public void OnWrongHintClicked(GameObject go)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("wrong");

        go.GetComponentInParent<Button>().interactable = false;
        GameObject wrong = go.transform.GetChild(0).gameObject;
        GameObject point = go.transform.GetChild(1).gameObject;

        GameManager.Instance.Level4Score -= 0.25f;
        task2NegativePoints += 0.25f;

        wrong.SetActive(true);
        point.SetActive(true);
        StartCoroutine(WaitForRightWrong(go));
    }
    IEnumerator WaitForRightWrong(GameObject obj)
    {
        yield return new WaitForSeconds(2f);

        //obj.SetActive(false);

        if (taskNum == 2)
        {
            if (task2HintCount == task2TasksCount)
            {
                // next2.SetActive(true);
                //StartCoroutine(LoadTask3());
                StartCoroutine(LoadTask3Promt());
            }
        }

        if (taskNum == 3)
        {
            if (task3HintCount == task3TasksCount)
            {
                // next3.SetActive(true);
                LevelCompleted();
            }
        }
    }

    #endregion

    public void OnMapButtonClicked()
    {
        GameManager.Instance.Level4Score = 0;
        mapPanel.SetActive(false);
        task1Promt.SetActive(true);
        vp.Play();
        backBtn.SetActive(true);
    }

    public void OnMapSkipClicked()
    {
        SceneManager.LoadScene(4);
    }

    public void LevelCompleted()
    {
        Debug.Log("Level Completed");
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();
        // PlayerPrefs.SetInt("score", playerScore);

        StartCoroutine(WaitForLevelCompletion());
    }

    IEnumerator WaitForLevelCompletion()
    {
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
        yield return new WaitForSeconds(2f);
        // goodJobPanel.SetActive(false);
        task3.SetActive(false);
        progressMeter.SetActive(false);
        levelEndPanel.SetActive(true);
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

        deactivateCurrentTasks = true;

        if (taskNum == 1)
        {
            // next1.SetActive(true);
            //StartCoroutine(LoadTask2());
            StartCoroutine(LoadTask2Promt());
        }
        else if (taskNum == 2)
        {
            // LevelCompleted();
            // next2.SetActive(true);
            //StartCoroutine(LoadTask3());
            StartCoroutine(LoadTask3Promt());
        }
        else if (taskNum == 3)
        {
            // next3.SetActive(true);
            LevelCompleted();
        }

    }

    public void OnSkipClicked()
    {
        StartCoroutine(LoadTask2());
    }

    public void OnNextClicked()
    {
        SceneManager.LoadScene(4);
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
}
