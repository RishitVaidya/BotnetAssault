using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameView : MonoBehaviour
{
    public static GameView Instance;

    public Text text_VirusesTerminated;
    public Text text_GoodPacketsCollected;

    public Image image_AntivirusPower;
    public Image image_ServerHealth;

    public GameObject gameOverView;

    private void Awake()
    {
        Time.timeScale = 0;
        Instance = this;
    }

    
    public void OnClick_TapToStart()
    {
        Time.timeScale = 1;

    }

    public void OnClick_PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
