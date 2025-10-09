using System.Collections.Generic;
using UnityEngine;

public class TaskCountStarsManager : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    List<GameObject> stars = new List<GameObject>();
    public int currentFilledStars = 0;

    public void ClearStars()
    {
        currentFilledStars = 0;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        stars.Clear();
    }

    public void InitiateStars(int count)
    {
        ClearStars();
        for (int i = 0; i < count; i++)
        {
            GameObject star = Instantiate(starPrefab, transform);
            stars.Add(star);
        }
    }

    public void FillStar()
    {
        if (currentFilledStars < stars.Count)
        {
            stars[currentFilledStars].transform.GetChild(0).gameObject.SetActive(true);
            currentFilledStars++;
        }
    }

    public void NegativeFillStar()
    {
        if (currentFilledStars < stars.Count)
        {
            stars[currentFilledStars].transform.GetChild(1).gameObject.SetActive(true);
            currentFilledStars++;
        }
    }
    //singleton
    private static TaskCountStarsManager _instance;

    public static TaskCountStarsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TaskCountStarsManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
}
