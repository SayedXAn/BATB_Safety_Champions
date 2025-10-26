using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PostScore : MonoBehaviour
{
    private string apiUrl = "https://leaderboard-backend.mern.singularitybd.net/api/v1/score";

    private string id;
    private string userName;
    private string userScore;
    private int gameID;

    public TMP_InputField idIF;
    public TMP_InputField nameIF;

    [System.Serializable]
    public class ScoreData
    {
        public string uuid;
        public string name;
        public string score;
        public int game;
    }

    public void LoginSubmitButton()
    {
        GameManager.Instance.userName = nameIF.text;
        GameManager.Instance.userID = idIF.text;
        GameManager.Instance.gameStartTime = System.DateTime.Now;
        //Debug.Log(System.DateTime.Now);
    }



    public void CallPostAPI()
    {
        StartCoroutine(PostScoreData("21312", "abu 31", "4565", 4));
    }

    IEnumerator PostScoreData(string uuid, string name, string score, int game)
    {
        ScoreData data = new ScoreData
        {
            uuid = uuid,
            name = name,
            score = score,
            game = game
        };

        string jsonData = JsonUtility.ToJson(data);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("x-token", "9b1de5f407f1463e7b2a921bbce364");

            Debug.Log("Sending: " + jsonData);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("✅ POST Success! Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("❌ POST Failed: " + request.error + "\nResponse: " + request.downloadHandler.text);
            }
        }
    }
}
