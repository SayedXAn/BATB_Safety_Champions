using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2Controller : MonoBehaviour
{
    public GameObject mapPanel, levelEndPanel;

    //[Header("Progress Meter")]
    public GameObject progressMeter;
    public GameObject step1, step2, step3, step4, step5, step6, step7, step8, step9;
    public Image step1Img, step2Img, step3Img, step4Img, step5Img, step6Img, step7Img, step8Img, step9Img;
    public Sprite red, green, yellow;

    [Header("Task UI")]
    public GameObject task1Start;
    public GameObject task1Promt, task1, task1EndInstruction, task1End, task1EndPromt,
        task2Start,task2FadedPromt, task2Promt, task2, task2End,
        task3Start, task3Promt, task3,
        task4Promt1, task4Promt2, task4Promt3, task4,
        task5Promt, task5, task5EndPanel, task6Promt, task6,
        task7Promt, task7, task8Promt, task8, task9Promt, task9;

    public Sprite closeDoorImg;
    public Button task2Next;
    public Image task4Button;
    public Sprite task4Sign;

    //[Header("Hint Detectors")]
    public GameObject[] rights;
    public GameObject[] wrongs;
    public GameObject[] points;

    public GameObject task4Point;

    [HideInInspector]
    public int taskNum;
    [HideInInspector]
    public int task1HintCount, task2HintCount, task3HintCount, task4HintCount, task5HintCount, task6HintCount, task7HintCount, task8HintCount, task9HintCount;
    public int task1TasksCount, task2TasksCount, task3TasksCount, task4TasksCount, task5TasksCount, task6TasksCount, task7TasksCount, task8TasksCount, task9TasksCount;
    public float task1NegativePoints, task2NegativePoints, task3NegativePoints, task4NegativePoints, task5NegativePoints, task6NegativePoints, task7NegativePoints, task8NegativePoints, task9NegativePoints;

    public int task3DropCount = 0;

    [Header("Task 4 Stickers")]
    public GameObject electrify, stopHand, exclem;

    [Header("Next Buttons")]
    public GameObject next1, next2, next3, next4, next5;

    public TMP_Text timerText;
    public GameObject taskTimerObj;
    Coroutine _taskTimerCoroutineRef;

    public GameObject goodJobPanel, backBtn;

    bool task2Loaded = false, task3Loaded = false, task4Loaded = false, task5Loaded = false, task6Loaded = false,
        task7Loaded = false, task8Loaded = false, task9Loaded = false, deactivateCurrentTasks = false;
    public GameObject level1ScoreObj, level2ScoreObj, level3ScoreObj, level4ScoreObj, level5ScoreObj;

    [Header("DropDowns")]
    public TMP_Dropdown task6DropDown, task7DropDown;
    private int dragDroppedItemCount = 0;

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
        task7HintCount = 0;
        task8HintCount = 0;
        task9HintCount = 0;

        // task1Loaded = false;
        task2Loaded = false;
        task3Loaded = false;
        task4Loaded = false;
        task5Loaded = false;
        task6Loaded = false;
        task7Loaded = false;
        task8Loaded = false;
        task9Loaded = false;

        // next1.SetActive(false);
        // next2.SetActive(false);
        // next3.SetActive(false);
        // next4.SetActive(false);
        // next5.SetActive(false);

        ShowScore();
    }

    #region Task 1

    IEnumerator LoadTask1Prompt()
    {
        yield return new WaitForSeconds(2f);
        task1Start.SetActive(false);
        task1Promt.SetActive(true);
    }
    public void OnTask1NextClicked()
    {
        Task1();
    }
    void Task1()
    {
        // task1Promt.SetActive(false);
        // task1.SetActive(true);

        TaskCountStarsManager.Instance.InitiateStars(task1TasksCount);
        deactivateCurrentTasks = false;
        task1.SetActive(true);
        //task1EndInstruction.SetActive(true);
        StartCoroutine(WaitForTask1Instruction());

        progressMeter.SetActive(true);

        taskNum = 1;

    }

    //public void OnTask1SubmitClicked()
    //{
    //    task1.SetActive(false);
    //    task1EndInstruction.SetActive(true);

    //    StartCoroutine(WaitForTask1Instruction());
    //}

    IEnumerator WaitForTask1Instruction()
    {
        yield return new WaitForSeconds(1f);
        if (_taskTimerCoroutineRef != null)
        {
            StopCoroutine(_taskTimerCoroutineRef);
        }

        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task1TasksCount * 5));
        //task1End.SetActive(true);
        next1.SetActive(true);
    }

    public void OnTask1RightHintClicked(int hintNum)
    {
        return;
        AudioManager.instance.PlaySound("right");

        task1HintCount++;

        rights[hintNum].GetComponentInParent<Button>().interactable = false;

        GameManager.Instance.Level2Score++;

        //Debug.Log(taskHintCount);

        rights[hintNum].SetActive(true);
        points[hintNum].SetActive(true);

        //step1Img.sprite = green;
        //step1.SetActive(true);

        goodJobPanel.SetActive(true);

        StartCoroutine(WaitForTask1EndPrompt());
    }

    IEnumerator WaitForTask1EndPrompt()
    {
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        TaskCountStarsManager.Instance.ClearStars();

        yield return null;
        goodJobPanel.SetActive(false);
        task1EndPromt.SetActive(true);
        task1End.SetActive(true);
        StartCoroutine(LoadTask2(1f));
    }

    public IEnumerator LoadTask2(float waitTime)
    {
        TaskCountStarsManager.Instance.ClearStars();
        if (task2Loaded)
        {
            yield break;
        }
        
        task2Loaded = true;

        if (task1HintCount > 0 && task1NegativePoints > 0)
            step1Img.sprite = yellow;
        else if (task1HintCount == task1TasksCount)
            step1Img.sprite = green;
        else if (task1HintCount == 0)
            step1Img.sprite = red;

        step1.SetActive(true);
        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);

        yield return new WaitForSeconds(waitTime);
        rights[0].SetActive(false);
        
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Task 2 is loading");
        task1.SetActive(false);
        task1End.SetActive(false);
        task1EndPromt.SetActive(false);
        //progressMeter.SetActive(false);
        task2Start.SetActive(true);
        yield return new WaitForSeconds(1f);
        task2FadedPromt.SetActive(false);
    }

    #endregion

    #region Task 2

    public void OnKuddusClicked()
    {
        task2Start.SetActive(false);
        task2Promt.SetActive(true);
    }
    public void OnTask2NextClicked()
    {
        Task2();
    }
    void Task2()
    {
        task2Promt.SetActive(false);
        task2.SetActive(true);

        progressMeter.SetActive(true);

        taskNum = 2;
        deactivateCurrentTasks = false;
        TaskCountStarsManager.Instance.InitiateStars(task2TasksCount);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task2TasksCount * 5));
    }

    public void OnTask2SubmitClicked()
    {
        Debug.Log("Task 2 submit clicked");
        task2.SetActive(false);
        task2End.SetActive(true);

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        if (task2HintCount > 0 && task2NegativePoints > 0)
            step2Img.sprite = yellow;
        else if (task2HintCount == task2TasksCount)
            step2Img.sprite = green;
        else if (task2HintCount == 0)
            step2Img.sprite = red;
        else
            step2Img.sprite = yellow;
        step2.SetActive(true);

        TaskCountStarsManager.Instance.ClearStars();
    }

    public void OnLoadTask3Clicked()
    {
        TaskCountStarsManager.Instance.ClearStars();
        StartCoroutine(LoadTask3(1f));
    }

    public IEnumerator LoadTask3(float waitTime)
    {
        if (task3Loaded)
        {
            yield break;
        }

        task3Loaded = true;
        //task2End.SetActive(false);
        task2.SetActive(false);

        if (_taskTimerCoroutineRef != null)
            StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();

        yield return new WaitForSeconds(waitTime);
        goodJobPanel.SetActive(false);
        task1End.SetActive(false);
        //progressMeter.SetActive(false);
        task3Promt.SetActive(true);

        if (task2HintCount > 0 && task2NegativePoints > 0)
            step2Img.sprite = yellow;
        else if (task2HintCount == task2TasksCount)
            step2Img.sprite = green;
        else if (task2HintCount == 0)
            step2Img.sprite = red;
        else
            step2Img.sprite = yellow;

        step2.SetActive(true);
    }

    #endregion

    #region Task 3

    public void OnTask3NextClicked()
    {
        // taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();
        Task3();
    }
    void Task3()
    {
        next3.SetActive(true);
        task3Promt.SetActive(false);
        task3.SetActive(true);

        taskNum = 3;
        taskTimerObj.SetActive(true);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task3TasksCount * 5));
        TaskCountStarsManager.Instance.InitiateStars(task3TasksCount);
    }

    public void LoadTask4(float waitTime)
    {
        // taskTimerObj.SetActive(false);
        StartCoroutine(LoadingTask4(0.2f));
    }

    public IEnumerator LoadingTask4(float waitTime)
    {
        if (task4Loaded)
        {
            yield break;
        }

        task4Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();

        deactivateCurrentTasks = false;

        yield return new WaitForSeconds(1f);
        if (task3HintCount > 0 && task3NegativePoints > 0)
            step3Img.sprite = yellow;
        else if (task3HintCount == task3TasksCount)
            step3Img.sprite = green;
        else if (task3HintCount == 0)
        {
            step3Img.sprite = red;
            // goodJobPanel.SetActive(true);
        }
        else
            step3Img.sprite = yellow;
        step3.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        // goodJobPanel.SetActive(false);
        taskNum = 4;
        task3.SetActive(false);
        //progressMeter.SetActive(false);
        // task4Promt1.SetActive(true);
        // yield return new WaitForSeconds(3f);

        // task4Promt1.SetActive(false);
        // task4Promt2.SetActive(true);
        // yield return new WaitForSeconds(3f);

        // task4Promt2.SetActive(false);
        TaskCountStarsManager.Instance.InitiateStars(task4TasksCount);

        task4Promt3.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        task4Promt3.SetActive(false);
        task4.SetActive(true);
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task4TasksCount * 5));
    }

    #endregion

    #region Task 4

    public void OnTask4RightButtonClicked(GameObject btn)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("right");

        btn.GetComponent<Button>().interactable = false;
        btn.transform.GetChild(1).gameObject.SetActive(true);
        btn.transform.GetChild(2).gameObject.SetActive(true);
        GameManager.Instance.Level2Score++;
        task4HintCount++;
        StartCoroutine(DisableRightWrongBtn(btn.transform.GetChild(1).gameObject));

        if (btn.transform.name == "Electrify")
        {
            electrify.SetActive(true);
        }
        else if (btn.transform.name == "StopHand")
        {
            stopHand.SetActive(true);
        }
        else if (btn.transform.name == "Exclem")
        {
            exclem.SetActive(true);
        }

        TaskCountStarsManager.Instance.FillStar();

        if (task4HintCount == task4TasksCount)
        {
            Debug.Log("Task 4 completed");
            // step4Img.sprite = green;
            step4.SetActive(true);
            // next4.SetActive(true);
            deactivateCurrentTasks = true;
            StartCoroutine(LoadTask5(1f));
        }
    }

    public void OnTask4WrongButtonClicked(GameObject btn)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("wrong");
        GameManager.Instance.Level2Score -= 0.25f;
        task4NegativePoints += 0.25f;
        btn.GetComponent<Button>().interactable = false;
        btn.transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine(DisableRightWrongBtn(btn.transform.GetChild(1).gameObject));
    }

    IEnumerator DisableRightWrongBtn(GameObject btn)
    {
        yield return new WaitForSeconds(2f);
        btn.SetActive(false);
    }

    // {
    //     AudioManager.instance.PlaySound("right");

    //     // task4Button.sprite = task4Sign;
    //     // task4Button.gameObject.SetActive(true);
    //     // //rights[hintNum].SetActive(true);
    //     // task4Point.SetActive(true);

    //     // playerScore++;

    //     // step4Img.sprite = green;
    //     // step4.SetActive(true);

    //     // StartCoroutine(LoadTask5());
    // }

    IEnumerator LoadTask5(float waitTime)
    {
        // taskTimerObj.SetActive(false);
        if (task5Loaded)
        {
            yield break;
        }

        task5Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();

        if (task4HintCount > 0 && task4NegativePoints > 0)
            step4Img.sprite = yellow;
        else if (task4HintCount == task4TasksCount)
            step4Img.sprite = green;
        else if (task4HintCount == 0)
            step4Img.sprite = red;
        else
            step4Img.sprite = yellow;

        step4Img.gameObject.SetActive(true);

        Debug.Log("Task 5 is loading");
        yield return new WaitForSeconds(waitTime);

        // goodJobPanel.SetActive(false);
        task4.SetActive(false);
        task5Promt.SetActive(true);
    }

    #endregion

    #region Task 5

    public void OnTask5NextClicked()
    {
        task5Promt.SetActive(false);
        task5.SetActive(true);

        taskNum = 5;
        deactivateCurrentTasks = false;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task5TasksCount * 5));
        TaskCountStarsManager.Instance.InitiateStars(task5TasksCount);
    }

    #endregion

    #region Task 6

    public void OnTask6NextClicked()
    {
        task6Promt.SetActive(false);
        task6.SetActive(true);

        taskNum = 6;
        deactivateCurrentTasks = false;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(10 /*task6TasksCount * 5*/)); //time increased
        TaskCountStarsManager.Instance.InitiateStars(task6TasksCount);
    }
    IEnumerator LoadTask6(float waitTime)
    {
        // taskTimerObj.SetActive(false);
        if (task6Loaded)
        {
            yield break;
        }

        task6Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();
        task5EndPanel.SetActive(true);
        if (task5HintCount > 0 && task5NegativePoints > 0)
            step5Img.sprite = yellow;
        else if (task5HintCount == task5TasksCount)
            step5Img.sprite = green;
        else if (task5HintCount == 0)
            step5Img.sprite = red;
        else
            step5Img.sprite = yellow;

        step5Img.gameObject.SetActive(true);

        Debug.Log("Task 6 is loading");
        yield return new WaitForSeconds(waitTime);

        // goodJobPanel.SetActive(false);
        task5.SetActive(false);
        task6Promt.SetActive(true);
    }
    #endregion


    #region Task 7

    public void OnTask7NextClicked()
    {
        task7Promt.SetActive(false);
        task7.SetActive(true);

        taskNum = 7;
        deactivateCurrentTasks = false;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(10 /*task7TasksCount * 5*/)); //time increased
        TaskCountStarsManager.Instance.InitiateStars(task7TasksCount);
    }
    IEnumerator LoadTask7(float waitTime)
    {
        // taskTimerObj.SetActive(false);
        if (task7Loaded)
        {
            yield break;
        }

        task7Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();
        //task7EndPanel.SetActive(true);
        if (task6HintCount > 0 && task6NegativePoints > 0)
            step6Img.sprite = yellow;
        else if (task6HintCount == task6TasksCount)
            step6Img.sprite = green;
        else if (task6HintCount == 0)
            step6Img.sprite = red;
        else
            step6Img.sprite = yellow;

        step6Img.gameObject.SetActive(true);

        Debug.Log("Task 7 is loading");
        yield return new WaitForSeconds(waitTime);

        // goodJobPanel.SetActive(false);
        task6.SetActive(false);
        task7Promt.SetActive(true);
    }
    #endregion

    #region Task 8

    public void OnTask8NextClicked()
    {
        task8Promt.SetActive(false);
        task8.SetActive(true);

        taskNum = 8;
        deactivateCurrentTasks = false;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task8TasksCount * 5));
        TaskCountStarsManager.Instance.InitiateStars(task8TasksCount);
    }
    IEnumerator LoadTask8(float waitTime)
    {
        // taskTimerObj.SetActive(false);
        if (task8Loaded)
        {
            yield break;
        }

        task8Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();
        //task7EndPanel.SetActive(true);
        if (task7HintCount > 0 && task7NegativePoints > 0)
            step7Img.sprite = yellow;
        else if (task7HintCount == task7TasksCount)
            step7Img.sprite = green;
        else if (task7HintCount == 0)
            step7Img.sprite = red;
        else
            step7Img.sprite = yellow;

        step7Img.gameObject.SetActive(true);

        Debug.Log("Task 8 is loading");
        yield return new WaitForSeconds(waitTime);

        // goodJobPanel.SetActive(false);
        task7.SetActive(false);
        task8Promt.SetActive(true);
    }
    #endregion

    #region Task 9

    public void OnTask9NextClicked()
    {
        task9Promt.SetActive(false);
        task9.SetActive(true);

        taskNum = 9;
        deactivateCurrentTasks = false;
        _taskTimerCoroutineRef = StartCoroutine(TaskTimerCoroutine(task9TasksCount * 5));
        TaskCountStarsManager.Instance.InitiateStars(task9TasksCount);
    }
    IEnumerator LoadTask9(float waitTime)
    {
        // taskTimerObj.SetActive(false);
        if (task9Loaded)
        {
            yield break;
        }

        task9Loaded = true;

        if (_taskTimerCoroutineRef != null) StopCoroutine(_taskTimerCoroutineRef);
        taskTimerObj.SetActive(false);
        TaskCountStarsManager.Instance.ClearStars();
        //task7EndPanel.SetActive(true);
        if (task8HintCount > 0 && task8NegativePoints > 0)
            step8Img.sprite = yellow;
        else if (task8HintCount == task8TasksCount)
            step8Img.sprite = green;
        else if (task8HintCount == 0)
            step8Img.sprite = red;
        else
            step8Img.sprite = yellow;

        step8Img.gameObject.SetActive(true);

        Debug.Log("Task 9 is loading");
        yield return new WaitForSeconds(waitTime);

        // goodJobPanel.SetActive(false);
        task8.SetActive(false);
        task9Promt.SetActive(true);
    }
    #endregion

    #region Hints Handling
    public void OnRightHintClicked(GameObject go)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        go.GetComponent<Button>().interactable = false;
        GameObject right = go.transform.GetChild(0).gameObject;
        GameObject point = go.transform.GetChild(1).gameObject;
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("right");

        TaskCountStarsManager.Instance.FillStar();

        if (taskNum == 1)
        {
            task1HintCount++;
            if (task1HintCount == task1TasksCount)
            {
                deactivateCurrentTasks = true;
                StartCoroutine(WaitForTask1EndPrompt());
                // next1.SetActive(true);
            }
        }

        if (taskNum == 2)
        {
            task2HintCount++;
            if (task2HintCount == task2TasksCount)
            {
                deactivateCurrentTasks = true;
                taskTimerObj.SetActive(false);
                // next2.SetActive(true);
            }
        }
        if (taskNum == 3)
        {
            task3HintCount++;
            if (task3HintCount == task3TasksCount)
            {
                deactivateCurrentTasks = true;
                // next3.SetActive(true);
            }
        }
        // if (taskNum == 4)
        //     task4HintCount++;
        if (taskNum == 5)
        {
            task5HintCount++;
            if (task5HintCount == task5TasksCount)
            {
                deactivateCurrentTasks = true;
                // next5.SetActive(true);
            }
        }

        GameManager.Instance.Level2Score++;

        //Debug.Log(taskHintCount);

        right.SetActive(true);
        point.SetActive(true);

        StartCoroutine(WaitForRightWrong(go));
    }

    public void DropDownAnswerCheck(int taskNum)
    {
        //Debug.Log("value" + task6DropDown.value); //value 0, 1, 2 Correct is 2
        if(taskNum == 6 )
        {
            if(task6DropDown.value == 1 /*2*/) //Not high, correct is medium
            {
                //right
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySound("right");
                task6HintCount++;
                GameManager.Instance.Level2Score++;
                TaskCountStarsManager.Instance.FillStar();
            }
            else
            {
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySound("wrong");
                task6NegativePoints += 0.25f;
                GameManager.Instance.Level2Score -= 0.25f;
            }
        }
        else if(taskNum == 7 )
        {
            if (task7DropDown.value == 2)
            {
                //right
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySound("right");
                task7HintCount++;
                GameManager.Instance.Level2Score++;
                TaskCountStarsManager.Instance.FillStar();
            }
            else
            {
                if (AudioManager.instance != null)
                    AudioManager.instance.PlaySound("wrong");
                task7NegativePoints += 0.25f;
                GameManager.Instance.Level2Score -= 0.25f;
            }
        }

    }

    public void OnWrongHintClicked(GameObject go)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        go.GetComponent<Button>().interactable = false;
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySound("wrong");
        GameObject wrong = go.transform.GetChild(0).gameObject;
        GameObject point = go.transform.GetChild(1).gameObject;

        wrong.SetActive(true);
        point.SetActive(true);

        GameManager.Instance.Level2Score -= 0.25f;
        if (taskNum == 1)
        {
            task1NegativePoints += 0.25f;
        }
        if (taskNum == 2)
        {
            task2NegativePoints += 0.25f;
        }
        if (taskNum == 3)
        {
            task3NegativePoints += 0.25f;
        }
        if (taskNum == 4)
        {
            task4NegativePoints += 0.25f;
        }
        if (taskNum == 5)
        {
            task5NegativePoints += 0.25f;
        }

        StartCoroutine(WaitForRightWrong(go));
    }
    IEnumerator WaitForRightWrong(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        obj.SetActive(false);

        // if (task2HintCount == 2)
        // {
        //     task2Next.interactable = true;
        // }

        // if (taskNum == 5 && task5HintCount == 2)
        // {
        //     LevelCompleted();
        // }
        Debug.Log($"Task {taskNum}: {taskNum}HintCount: {taskNum}HintCount");
        if (taskNum == 1 && task1HintCount == task1TasksCount)
           StartCoroutine(WaitForTask1EndPrompt());
        else if (taskNum == 2 && task2HintCount == task2TasksCount)
        {
            //StartCoroutine(LoadTask3(1));
            task2End.SetActive(true);
        }
           
        else if (taskNum == 3 && task3HintCount == task3TasksCount)
            LoadTask4(2);
        else if (taskNum == 4 && task4HintCount == task4TasksCount)
            LoadTask5(1);
        else if (taskNum == 5 && task5HintCount == task5TasksCount)
            //LevelCompleted();
            StartCoroutine(LoadTask6(2));
    }

    public void OnQuestionAnswerSelect(bool correct)
    {
        if (deactivateCurrentTasks)
        {
            return;
        }
        if (correct)
        {
            AudioManager.instance.PlaySound("right");
            TaskCountStarsManager.Instance.FillStar();
            if (taskNum == 9)
                task9HintCount++;

            GameManager.Instance.Level2Score++;
        }
        else
        {
            AudioManager.instance.PlaySound("wrong");
            GameManager.Instance.Level1Score -= 0.25f;

            if (taskNum == 9)
                task9NegativePoints += 0.25f;
        }
        LevelCompleted();
    }

    #endregion

    public void OnMapButtonClicked()
    {
        GameManager.Instance.Level2Score = 0;
        mapPanel.SetActive(false);
        task1Promt.SetActive(true);    
        //task1Start.SetActive(true);
        // StartCoroutine(LoadTask1Prompt());
        //OnTask1NextClicked();
        backBtn.SetActive(true);
    }

    public void OnMapSkipClicked()
    {
        SceneManager.LoadScene(2);
    }

    public void OnSkipClicked()
    {
        if (taskNum == 1)
        {
            StartCoroutine(LoadTask2(0));
        }
        else if (taskNum == 2)
        {
            StartCoroutine(LoadTask3(0));
        }
        else if (taskNum == 3)
        {
            LoadTask4(0);
        }
        else if (taskNum == 4)
        {
            StartCoroutine(LoadTask5(0));
        }
        else if (taskNum == 5)
        {
            StartCoroutine(LoadTask6(2f));
        }
        else if (taskNum == 6)
        {
            StartCoroutine(LoadTask7(2f));
        }
        else if (taskNum == 7)
        {
            StartCoroutine(LoadTask8(2f));
        }
        else if (taskNum == 8)
        {
            StartCoroutine(LoadTask9(2f));
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

    IEnumerator WaitForLevelCompletion()
    {
        yield return new WaitForSeconds(1f);
        //task9EndPanel.SetActive(true);
        if (task9HintCount > 0 && task9NegativePoints > 0)
            step9Img.sprite = yellow;
        else if (task9HintCount == task9TasksCount)
            step9Img.sprite = green;
        else if (task9HintCount == 0)
            step9Img.sprite = red;
        else
            step9Img.sprite = yellow;

        step9.SetActive(true);

        yield return new WaitForSeconds(2f);
        goodJobPanel.SetActive(false);
        task9.SetActive(false);
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
            // StartCoroutine(LoadTask2());
            deactivateCurrentTasks = true;
            StartCoroutine(WaitForTask1EndPrompt());
            // next1.SetActive(true);
        }
        else if (taskNum == 2)
        {
            deactivateCurrentTasks = true;
            //StartCoroutine(LoadTask3(1));
            task2End.SetActive(true);
            
            // next2.SetActive(true);
            
        }
        else if (taskNum == 3)
        {
            deactivateCurrentTasks = true;
           LoadTask4(0);
            // next3.SetActive(true);
        }
        else if (taskNum == 4)
        {
            deactivateCurrentTasks = true;
            StartCoroutine(LoadTask5(0));
            // LevelCompleted();
            // next4.SetActive(true);
        }
        else if (taskNum == 5)
        {
            deactivateCurrentTasks = true;
            // next5.SetActive(true);
            //LevelCompleted();]
            StartCoroutine(LoadTask6(2f));
        }

        else if (taskNum == 6)
        {
            deactivateCurrentTasks = true;
            // next5.SetActive(true);
            //LevelCompleted();]
            StartCoroutine(LoadTask7(2f));
        }
        else if (taskNum == 7)
        {
            deactivateCurrentTasks = true;
            // next5.SetActive(true);
            //LevelCompleted();]
            StartCoroutine(LoadTask8(2f));
        }
        else if (taskNum == 8)
        {
            deactivateCurrentTasks = true;
            // next5.SetActive(true);
            //LevelCompleted();]
            StartCoroutine(LoadTask9(2f));
        }

    }
    public void OnNextClicked()
    {
        SceneManager.LoadScene(2);
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

        // level2ScoreObj.GetComponentInChildren<TMP_Text>().text = ((GameManager.Instance.Level2Score / GameManager.Instance.Level2TotalScore) * 100).ToString("0.00") + "%";
        // level2ScoreObj.gameObject.SetActive(true);

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

    
    public void DragDropOutput(int itemID, int slotID)
    {
        dragDroppedItemCount++;
        if(itemID == slotID)
        {
            Debug.Log("Milseeeee");
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySound("right");
            task8HintCount++;
            GameManager.Instance.Level2Score++;
            TaskCountStarsManager.Instance.FillStar();
        }
        else
        {
            Debug.Log("Mile nai");
            if (AudioManager.instance != null)
                AudioManager.instance.PlaySound("wrong");
            task8NegativePoints += 0.25f;
            GameManager.Instance.Level2Score -= 0.25f;
        }
        if(dragDroppedItemCount == 4)
        {
            StartCoroutine(LoadTask9(2));
        }
    }

}