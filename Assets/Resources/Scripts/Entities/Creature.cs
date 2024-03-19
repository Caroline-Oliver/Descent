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
    [SerializeField] public float Speed = 5;
    [SerializeField] public float JumpPower = 5;
    [SerializeField] public Rigidbody2D body;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        if (body is null) {
            Debug.LogError("Rigidbody2D is NULL!");
        }
    }

    private void Jump(float y) {
        body.velocity = new Vector3(body.velocity.x, y * JumpPower);
    }

    public void MoveCreature(Vector2 inputMovement) {
        if (inputMovement.y > 0 && body.velocity.y == 0)
        {
            Jump(inputMovement.y);
        }

        body.velocity += new Vector2(inputMovement.x * Speed * Time.deltaTime, 0);
    }

    public void MoveCreature(Vector3 inputMovement) {
        inputMovement = Vector3.Normalize(inputMovement);

        MoveCreature(new Vector2(inputMovement.x, inputMovement.y));
    }

    public void MoveCreatureToward(Vector3 target){
        Vector3 direction = target - transform.position;
        MoveCreature(direction.normalized);
    }

    public void Stop() {
        MoveCreature(Vector3.zero);
    }
}
