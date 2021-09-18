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
    public int hiScore;
    public string userName;

    public InputField usernameText;
    public Text hiScoreText;

    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int hiScore;
    }

    public PlayerData hiScoreData;
    
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
        hiScoreData = new PlayerData();
        LoadNameAndHiScore();
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

    public void SaveNameAndHiScore()
    {
        string json = JsonUtility.ToJson(hiScoreData);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadNameAndHiScore()
    {
        PlayerData data = new PlayerData();

        string json = File.ReadAllText(Application.persistentDataPath + "/savefile.json");
        data = JsonUtility.FromJson<PlayerData>(json);


        hiScoreData.hiScore = data.hiScore;
        hiScoreData.name = data.name;
        //Debug.Log(hiScore);
        hiScoreText.text = "Best Score : " + hiScoreData.hiScore;
    }
}
