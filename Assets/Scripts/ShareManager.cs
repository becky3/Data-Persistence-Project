using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShareManager : MonoBehaviour
{
    [System.Serializable]
    public class ScoreInfo
    {
        public string userName;
        public int bestScore;

        public ScoreInfo(string userName, int bestScore)
        {
            this.userName = userName;
            this.bestScore = bestScore;
        }
    }

    public static ShareManager Instance;

    public ScoreInfo bestScoreInfo { get;  private set; }
    
    public string currentUserName = "";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadBestScore();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadBestScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isBestScore(int score)
    {
        if (score <= 0)
        {
            return false;
        }

        if (bestScoreInfo != null)
        {
            return bestScoreInfo.bestScore < score;
        }

        return true;
    }

    public void SaveBestScore(int score)
    {
        var saveData = new ScoreInfo(
            currentUserName,
            score
        );

        

        string json = JsonUtility.ToJson(saveData);

        Debug.Log($"{json}");

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            Debug.Log($"exists : '{path}'");

            string json = File.ReadAllText(path);

            bestScoreInfo = JsonUtility.FromJson<ScoreInfo>(json);
        }
    }
}
