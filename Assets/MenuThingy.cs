using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuThingy : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    void Start() // Lägger till de två lyssnarna.
    {
        startButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(ExitGame);
    }
    void StartGame() // Här ändrar vi scen från meny till spel.
    {
        SceneManager.LoadScene("GameScene");
    }

    void ExitGame() // Här lämnar vi hela programmet.
    {
        Application.Quit();
    }
}
