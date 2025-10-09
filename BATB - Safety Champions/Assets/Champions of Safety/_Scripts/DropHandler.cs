 using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandler : MonoBehaviour, IDropHandler
{
    public Level2Controller controller2;
    public Level3Controller controller3;
    public string tagName;
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (controller2 != null)
    //     {
    //         Controller2EnterLogic(other);
    //     }
    //     else if (controller3 != null)
    //     {
    //         Controller3EnterLogic(other);
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (controller2 != null)
    //     {
    //         Controller2ExitLogic(other);
    //     }
    //     else if (controller3 != null)
    //     {
    //         Controller3ExitLogic(other);
    //     }
    // }

    void Controller2EnterLogic(PointerEventData eventData)
    {
        controller2.task3DropCount++;
        eventData.pointerDrag.GetComponent<Drag>().enabled = false;
        GameObject right = transform.gameObject.transform.GetChild(0).gameObject;
        GameObject wrong = transform.gameObject.transform.GetChild(1).gameObject;
        GameObject point = transform.gameObject.transform.GetChild(2).gameObject;
        if (eventData.pointerDrag.CompareTag(tagName))
        {
            GameManager.Instance.Level2Score += 1;
            TaskCountStarsManager.Instance.FillStar();
            controller2.task3HintCount++;
            // Debug.Log(tagName);
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound("right");
            }
            point.GetComponent<Text>().text = "+1";
            //make right as root child
            right.transform.SetParent(transform.root);
            right.SetActive(true);
            point.SetActive(true);
            Destroy(right, 2f);
        }
        else
        {
            TaskCountStarsManager.Instance.NegativeFillStar();
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound("wrong");
            }
            point.GetComponent<Text>().text = "-0.25";
            //make wrong as root child
            wrong.transform.SetParent(transform.root);
            wrong.SetActive(true);
            point.SetActive(true);
            Destroy(wrong, 2f);
            GameManager.Instance.Level2Score -= 0.25f;
            controller2.task3NegativePoints -= 0.25f;
        }
        if (controller2.task3DropCount == controller2.task3TasksCount)
        {
            // controller2.next3.SetActive(true);
            controller2.LoadTask4(0);
        }
        this.enabled = false;
    }

    void Controller2ExitLogic(Collider2D other)
    {
        if (controller2.task3HintCount == 3) return;
        if (other.CompareTag(tagName))
        {
            controller2.task3HintCount--;
        }
    }

    void Controller3EnterLogic(Collider2D other)
    {
        // if (other.CompareTag(tagName))
        // {
        //     controller3.task4HintCount++;
        //     Debug.Log($"{tagName} Entered -> count: {controller3.task4HintCount}");

        //     if (controller3.task4HintCount == 4)
        //     {
        //         controller3.playerScore += 4;
        //         Debug.Log("Level Completed");
        //         controller3.LevelCompleted();
        //     }
        // }
    }

    void Controller3ExitLogic(Collider2D other)
    {
        if (controller3.task4HintCount == 4) return;
        if (other.CompareTag(tagName))
        {
            controller3.task4HintCount--;
            Debug.Log($"{tagName} Exit -> count: {controller3.task4HintCount}");
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (controller2 != null)
        {
            Controller2EnterLogic(eventData);
        }
        else if (controller3 != null)
        {
            // controller3.OnDrop(eventData);
        }
    }
}
