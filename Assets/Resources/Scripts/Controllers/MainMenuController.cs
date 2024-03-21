using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using JetBrains.Annotations;

public class MenuController : MonoBehaviour
{
    [SerializeField] string firstSceneName = "SafeZone";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void StartGame() {
        // currently just always starting in limbo

        if (SceneManager.GetSceneByName(firstSceneName) != null) {
            SceneManager.LoadScene(firstSceneName);
        }
        else {
            Debug.LogError("There was a problem while attempting to load first scene in MenuController.cs/StartGame()");
        }
    }
}
