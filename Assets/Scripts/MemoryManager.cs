using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class MemoryManager : MonoBehaviour
{
    public static MemoryManager instance;
    public int hiScore = 0 ;
    public string hiScoreName = "";

    public string userName;

    public InputField usernameText;
    public Text hiScoreText;

    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int hiScore;

    }

    [System.Serializable]
    public class AllHiScores
    {
        public List<PlayerData> hiScoreData = new List<PlayerData>();
    }



    public AllHiScores ahs;
    public string tmpJson;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        ahs = new AllHiScores();
        LoadNameAndHiScore();
        if(ahs.hiScoreData.Count > 0)
        {
            hiScoreName = ahs.hiScoreData[0].name;
            hiScore = ahs.hiScoreData[0].hiScore;
        }
    }
    public void StartGame()
    {
        userName = usernameText.text;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SaveNameAndHiScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();

#else
        Application.Quit();

#endif
    }

    public void ShowHiScores()
    {
        SceneManager.LoadScene(2);
    }
    public void SaveNameAndHiScore()
    {
        PlayerData singleInfo = new PlayerData();
        singleInfo.hiScore = hiScore;
        singleInfo.name = hiScoreName;
        ahs.hiScoreData.Add(singleInfo);

        ahs.hiScoreData.Sort((f, s) => f.hiScore.CompareTo(s.hiScore));
        ahs.hiScoreData.Reverse();
        string json = JsonUtility.ToJson(ahs);
        
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNameAndHiScore()
    {
        tmpJson = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
        ahs = JsonUtility.FromJson<AllHiScores>(tmpJson);

        Debug.Log(ahs);        
    }

    public string printHiScores()
    {
        string output = "";

        foreach(PlayerData item in ahs.hiScoreData)
        {
            output += item.name + " Has " + item.hiScore + " Points" + System.Environment.NewLine;
        }

        return output;
    }
}
