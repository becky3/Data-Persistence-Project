using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;


public class TitleManager : MonoBehaviour
{

    private ShareManager shareManager;

    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField inputFieldUserName;

    // Start is called before the first frame update
    void Start()
    {
        shareManager = ShareManager.Instance;
        shareManager.LoadBestScore();

        var info = shareManager.bestScoreInfo;
        string text = "Challenge Best Score!!";
        if (info != null)
        {
            text = $"Best Score : {info.userName} : {info.bestScore}";
        }

        var name = shareManager.currentUserName;
        if (name != "")
        {
            inputFieldUserName.text = name;
        }

        bestScoreText.SetText(text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNew()
    {
        var name = inputFieldUserName.text;
        if (name == "")
        {
            name = "unknwon";
        }
        shareManager.currentUserName = name;

        Debug.Log($"user name:{name}");

        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
