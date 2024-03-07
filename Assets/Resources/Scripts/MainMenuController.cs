using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void StartGame() {
        // currently just always starting in limbo

        if (SceneManager.GetSceneByName("Limbo") != null) {
            SceneManager.LoadScene("Limbo");
        }
        else {
            Debug.Log("There was a problem while attempting to load Limbo in MenuController.cs/StartGame()");
        }
    }
}
