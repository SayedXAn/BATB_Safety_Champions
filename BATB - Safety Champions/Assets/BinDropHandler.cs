using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BinDropHandler : MonoBehaviour, IDropHandler
{
    string transformName;
    public GameObject right, wrong, point;

    void Start()
    {
        transformName = transform.name;
        right = transform.gameObject.transform.GetChild(0).gameObject;
        wrong = transform.gameObject.transform.GetChild(1).gameObject;
        point = transform.gameObject.transform.GetChild(2).gameObject;
    }
    public Level4Controller controller4;
    public void OnDrop(PointerEventData eventData)
    {
        controller4.task3DropCount++;
        string tagName = eventData.pointerDrag.GetComponent<GarbageDragger>().targetBinName;
        Destroy(eventData.pointerDrag);
        if (tagName == transformName)
        {
            TaskCountStarsManager.Instance.FillStar();
            controller4.task3HintCount++;
            GameManager.Instance.Level4Score++;
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound("right");
            }
            point.GetComponent<Text>().text = "+1\nmwVK web";
            right.SetActive(true);
            point.SetActive(true);
        }
        else
        {
            TaskCountStarsManager.Instance.NegativeFillStar();
            controller4.task2NegativePoints += 0.25f;
            GameManager.Instance.Level4Score -= 0.25f;
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound("wrong");
            }
            point.GetComponent<Text>().text = "-0.25\nfzj web";
            wrong.SetActive(true);
            point.SetActive(true);
        }

        StartCoroutine(DisableRightWrong());
    }

    IEnumerator DisableRightWrong()
    {
        yield return new WaitForSeconds(2f);
        right.SetActive(false);
        wrong.SetActive(false);
        point.SetActive(false);
        if (controller4.task3DropCount == controller4.task3TasksCount)
        {
            // controller4.next3.SetActive(true);
            controller4.LevelCompleted();
        }
    }
}
