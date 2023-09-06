using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    //GameObjects variables
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject charecterPick;
    [SerializeField] private GameObject charecterPick_b;
    [SerializeField] private GameObject mapPick;
    [SerializeField] private GameObject Score;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject[] gameObjects;
    [SerializeField] private TextMeshProUGUI[] LeaderBoard;

    private void Awake()
    {
        volumeSlider.value = GameManager.volume;
        GameManager.Load();
    }

    //methods for buttons
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuActive()
    {
        mainMenuReset();
        menu.SetActive(true);
    }

    public void settingsActive()
    {
        mainMenuReset();
        settings.SetActive(true);
        volumeSlider.value = GameManager.volume;
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void charecterPickMenu()
    {
        mainMenuReset();
        charecterPick.SetActive(true);
        charecterPick_b.SetActive(true);
    }

    public void mapPickMenu()
    {
        mainMenuReset();
        mapPick.SetActive(true);
    }

    public void scoreMenu()
    {
        mainMenuReset();
        Score.SetActive(true);
        updateScoreText();
    }


    //helping methods 
    //close all menues
    private void mainMenuReset()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(false);
            GameManager.save();
        }
    }

    //change leaderbord values
    private void updateScoreText()
    {
        for(int i = 0; i < LeaderBoard.Length; i++)
        {
            LeaderBoard[i].text = GameManager.leaderBoardText[i];
        }
    }

    //change music volume
    public void volumeChange()
    {
        GameManager.volume = volumeSlider.value;
    }
}
