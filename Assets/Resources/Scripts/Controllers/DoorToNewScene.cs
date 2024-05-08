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
    private AnimationStateChanger animationStateChanger;

    private bool open;

    void Awake() {
        animationStateChanger = GetComponent<AnimationStateChanger>();
        if (animationStateChanger == null) {
            Debug.LogError("No animation state changer attatched!!!");
        }
    }

    void Start() {
        Open(startOpen);
    }

    public void Open(bool open) {
        this.open = open;
        if (open) {
            animationStateChanger.ChangeAnimationState("Active_Teleporter");
        } else {
            animationStateChanger.ChangeAnimationState("Inactive_Teleporter");
        }
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
