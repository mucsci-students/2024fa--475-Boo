using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Variable Declarations
    public GameObject player;
    public Button quitBtn;
    public Button menuBtn;
    public ScenesManager scenesManager;

    public GameObject pauseMenu;

    private int pauseKeyCount = 0;

    public bool isGamePaused = false;

    //*********************************************************************
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
        }
        // isGamePaused = Input.GetKeyDown(KeyCode.escape);
        // if (isGamePaused && pauseKeyCount == 1)
        // {
        //     Debug.Log("Pause button clicked!");
        // }
        // OnApplicationPause(isGamePaused);
    }

    void FixedUpdate()
    {

        if (quitBtn == null)
        {
            quitBtn = pauseMenu.transform.Find("Quitbtn").GetComponent<Button>();
            quitBtn.onClick.AddListener(OnQuitButtonClicked);
        }
        if (menuBtn == null)
        {
            menuBtn = pauseMenu.transform.Find("MainMenubtn").GetComponent<Button>();
            menuBtn.onClick.AddListener(OnMenuButtonClicked);
        }
    }

    void OnApplicationPause(bool pause)
    {
        if ((pause == true))
        {
            pauseKeyCount++;
        }
        //Pauses the game.
        if (pauseKeyCount == 1)
        {
            quitBtn.gameObject.SetActive(true);
            menuBtn.gameObject.SetActive(true);
        }
        //Unpauses the game.
        if (pauseKeyCount == 2)
        {
            quitBtn.gameObject.SetActive(false);
            menuBtn.gameObject.SetActive(false);
            pause = false;
            pauseKeyCount = 0;
        }
    }

    void endOfGame()
    {
        Debug.Log("The game has ended.");
        Invoke("OnMenuButtonClicked", 5f);
    }

    //called by listeners
    //**************************************************************************************************
    void OnQuitButtonClicked()
    {
        quitBtn.gameObject.SetActive(false);
        menuBtn.gameObject.SetActive(false);
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }

    void OnMenuButtonClicked()
    {
        Debug.Log("Menu button clicked!");
        scenesManager.LoadMainMenu();
    }
}