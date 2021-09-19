using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScores : MonoBehaviour
{
    public Text scoresText;


    // Start is called before the first frame update
    void Start()
    {

        scoresText.text = MemoryManager.instance.printHiScores();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
