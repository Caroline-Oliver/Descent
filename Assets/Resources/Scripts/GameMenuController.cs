using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] bool gamePaused;

    void Awake() {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        bool isEscapeHeld = Input.GetKeyDown(KeyCode.BackQuote);
        #else
        bool isEscapeHeld = Input.GetKeyDown(KeyCode.Escape);
        #endif

        if (isEscapeHeld) {
            if (gamePaused)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void PauseGame() {
        gamePaused = true;
        Time.timeScale = 0;
        menu.SetActive(gamePaused);
    }

    public void UnpauseGame() {
        gamePaused = false;
        Time.timeScale = 1;
        menu.SetActive(gamePaused);
    }

    public void ExitGame() {
        // save and stuff
        Application.Quit();
    }
}
