using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public class Creature : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] float MaxHealth = 3;
    [SerializeField] float Health = 3;

    [Header("Movement")]
    [SerializeField] float MaxSpeed = 5;
    [SerializeField] float Speed = 5;
    [SerializeField] private Rigidbody2D _rbody;

    void Awake() {
        _rbody = GetComponent<Rigidbody2D>();
        if (_rbody is null) {
            Debug.LogError("Rigidbody2D is NULL!");
        }
    }

    public void move(Vector2 inputMovement) {
        _rbody.velocity += Speed * Time.deltaTime * inputMovement;
    }
}
