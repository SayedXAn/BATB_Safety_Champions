using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float Level1Score = 0;
    public float Level2Score  = 0;
    public float Level3Score = 0;
    public float Level4Score = 0;
    public float Level5Score  = 0;

    public int Level1TotalScore  = 17;
    public int Level2TotalScore  = 15+7;
    public int Level3TotalScore  = 12+8;
    public int Level4TotalScore  = 17;
    public int Level5TotalScore  = 6;

    public bool GameStarted = false;

    public string userID;
    public string userName;
    public string userScore;
    //public string gameStartTime;
    public System.DateTime gameStartTime;
    public System.DateTime gameEndTime;
    


    public void ResetScores()
    {
        GameStarted = false;
        Level1Score = 0;
        Level2Score = 0;
        Level3Score = 0;
        Level4Score = 0;
        Level5Score = 0;
    }

    public void OnBackBtnClicked()
    {
        //get current scene index, then load the next scene, if it's the last scene, load the first scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 4)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex +1);
        }
    }
    //singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }
}