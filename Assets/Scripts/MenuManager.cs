using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public string gameSceneName;
    public string mainMenuSceneName, creditSceneName, controlSceneName;
    public GameObject pauseObject;
    public GameObject creditBG, controlsBg, playBg;

    private void Start()
    {
    }
    void Update()
    {
        // If the player press "Escape" button, the game is paused until he press "Escape" again or click "Resume". During the pause, everything is stopped in the game.
        {
            if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name == gameSceneName)
            {
                print("echap");
                if (pauseObject.activeInHierarchy == false)
                {
                    pauseObject.SetActive(true);
                    print("pause menu closed");
                    Time.timeScale = 0;
                }
                else
                {
                    ResumeGame();
                }
            }
            
        } 
    }

    public void PlayGame() // Launch the game when the player press "Play" in the main menu.
    {
        SceneManager.LoadScene(gameSceneName);
        print("Loading game.");
        Time.timeScale = 1;
    } 
    public void QuitGame() // Close the game.
    {
        print("Quitting game.");
        Application.Quit();
    } 
    public void BackToMenu() // Close the running game and conduct the player to the main menu. 
    {
        SceneManager.LoadScene(mainMenuSceneName);
        print("Main menu loading.");
    } 

    public void ResumeGame() // The game restart when it stopped before the pause.
    {
        pauseObject.SetActive(false);
        print("pause menu closed");
        Time.timeScale = 1;
    } 

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void CreditMenu()
    {
        SceneManager.LoadScene(creditSceneName);
    }


    public void ControlMenu()
    {
        SceneManager.LoadScene(controlSceneName);
    }







    public void ActiveBgCredit()
    {
        creditBG.SetActive(true);
    }
    public void DisableBgCredit()
    {
        creditBG.SetActive(false);
    }
    public void ActiveBgControls()
    {
        controlsBg.SetActive(true);
    }
    public void DisableBgControls()
    {
        controlsBg.SetActive(false);
    }
    public void ActiveBgPlay()
    {
        playBg.SetActive(true);
    }
    public void DisableBgPlay()
    {
        playBg.SetActive(false);
    }
}
