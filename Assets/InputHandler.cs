using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    
    [SerializeField] private PlayerActions _playerActions;
    [SerializeField] private Creature player;

    void Awake() {
        _playerActions = new PlayerActions();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Creature>();
        if (player is null) {
            Debug.LogError("Player is NULL!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handlePlayer();
    }

    private void OnEnable() {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable() {
        _playerActions.Player_Map.Disable();
    }

    private void handlePlayer() {
        Vector2 moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        player.move(moveInput);
    }
}
