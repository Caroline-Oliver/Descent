using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class Player : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] float MaxHealth = 3;
    [SerializeField] float Health = 3;

    [Header("Movement")]
    [SerializeField] float MaxSpeed = 5;
    [SerializeField] float Speed = 5;
    [SerializeField] private PlayerActions _playerActions;
    [SerializeField] private Rigidbody2D _rbody;
    [SerializeField] private Vector2 _moveInput;

    void Awake() {
        _playerActions = new PlayerActions();
        _rbody = GetComponent<Rigidbody2D>();
        if (_rbody is null) {
            Debug.LogError("Rigidbody2D is NULL!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable() {
        _playerActions.Player_Map.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
    }

    void FixedUpdate() {
        
    }

    private void handleMovement() {
        _moveInput = _playerActions.Player_Map.Movement.ReadValue<Vector2>();
        _rbody.velocity += Speed * Time.deltaTime * _moveInput;
    }
}
