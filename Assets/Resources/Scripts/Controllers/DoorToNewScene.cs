using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNewScene : MonoBehaviour
{
    [SerializeField] string nextScene;
    [SerializeField] bool startOpen = true;
    [SerializeField] float delay = 0.25f;
    private bool open;

    void Start() {
        open = startOpen;
    }

    public void Open() {
        open = true;
    }

    public void Close() {
        open = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!open) return;

        if (other.gameObject.CompareTag("Player")) {
            StartCoroutine(Transition(nextScene));            
        }
    }

    private IEnumerator Transition(string nextScene) {
        yield return new WaitForSeconds(delay);
        if (SceneManager.GetSceneByName(nextScene) != null) {
                SceneManager.LoadScene(nextScene);
            }
            else {
                Debug.LogError("There was a problem while attempting the next scene in DoorToNewScene.cs/OnTriggerEnter()");
            }
    }
}
