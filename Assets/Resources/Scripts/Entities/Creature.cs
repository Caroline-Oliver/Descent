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

    [Header("Physics")]
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpOffset = -.5f;
    [SerializeField] float jumpRadius = .25f;

    [Header("Combat")]
    [SerializeField] GameObject attackPoint;
    [SerializeField] float attackRadius = 0.5f;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] public float attackCoolDown = 0.5f;
    [SerializeField] float timeUntilNextAttack = 0;
    [SerializeField] float damage = 1f;
    [SerializeField] float resistance = 1f;
    float hurtColorStartTimestamp = -100f;
    [SerializeField] float hurtColorRiseDuration = 0.25f;
    [SerializeField] float hurtColorFallDuration = 0.5f;

    [Header("Timer")]
    [SerializeField] float timer = 0f;

    private SpriteRenderer spriteRenderer;

    void Awake() {
        Health = MaxHealth;
        
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (body == null) {
            Debug.LogError("Rigidbody2D is NULL!");
        }
        timer = 0f;
    }

    void Update() {
        if (Health <= 0) {
            Destroy(gameObject);
        }
        HandleHurtColor();
    }

    void FixedUpdate() {
        timer += Time.deltaTime;
    }

    private void Jump(float y) {
        if (body.velocity.y > JumpPower / 2) return;

        if(Physics2D.OverlapCircleAll(transform.position + new Vector3(0,jumpOffset,0),jumpRadius,groundMask).Length > 0){
            body.AddForce(Vector3.up * JumpPower, ForceMode2D.Impulse);
        }
    }

    public void MoveCreature(Vector2 inputMovement) {
        if (inputMovement.y > 0)
        {
            Jump(inputMovement.y);
        }

        body.velocity += new Vector2(inputMovement.x * Speed * Time.deltaTime, 0);

        if(body.velocity.x < -0.1){
            body.transform.localScale = new Vector3(-1,1,1);
        }else if(body.velocity.x > 0.1){
            body.transform.localScale = new Vector3(1,1,1);
        }
    }

    public void MoveCreature(Vector3 inputMovement) {
        inputMovement = Vector3.Normalize(inputMovement);

        MoveCreature(new Vector2(inputMovement.x, inputMovement.y));
    }

    public void MoveCreatureToward(Vector3 target){
        Vector3 direction = target - transform.position;
        MoveCreature(direction.normalized);
    }

    public void TurnCreatureToward(Vector3 target) {
        float dx = target.x - transform.position.x;
        if (dx >= 0) {
            body.transform.localScale = new Vector3(1,1,1);
        }
        else {
            body.transform.localScale = new Vector3(-1,1,1);
        }
    }

    public void Stop() {
        MoveCreature(Vector3.zero);
    }

    public void ChangeColor(Color color) {
        spriteRenderer.color = color;
    }
    
    private void HandleHurtColor() {
        // in rising phase
        if (timer < hurtColorStartTimestamp + hurtColorRiseDuration) {
            ChangeColor(Color.Lerp(Color.white, Color.red, (timer - hurtColorStartTimestamp) / hurtColorRiseDuration));
        }
        // in falling phase
        else if (timer > hurtColorStartTimestamp + hurtColorRiseDuration && timer < hurtColorStartTimestamp + hurtColorRiseDuration + hurtColorFallDuration) {
            ChangeColor(Color.Lerp(Color.red, Color.white, (timer - (hurtColorStartTimestamp+hurtColorRiseDuration)) / hurtColorFallDuration));
        }
    }

    public void Attack() {
        if (timer < timeUntilNextAttack) return;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRadius, enemyMask);
        foreach (Collider2D enemyCollider in enemies) {
            Creature enemy = enemyCollider.GetComponent<Creature>();
            enemy.Hurt(damage);
        }
        
        timeUntilNextAttack = timer + attackCoolDown;
    }

    public void Hurt(float damage) {
        Health -= 1 / resistance * damage;
        if (timer > hurtColorStartTimestamp+hurtColorRiseDuration+hurtColorFallDuration) {
            hurtColorStartTimestamp = timer;
        }

        // already being damaged and in rising phase, send straight to fall
        else if (timer < hurtColorStartTimestamp + hurtColorRiseDuration) {
            hurtColorStartTimestamp = timer - hurtColorRiseDuration;
        }
        // already in falling phase
        else if (timer > hurtColorStartTimestamp + hurtColorRiseDuration && timer < hurtColorStartTimestamp + hurtColorRiseDuration + hurtColorFallDuration) {
            var percentLeft = 1 - (timer - (hurtColorStartTimestamp+hurtColorRiseDuration)) / hurtColorFallDuration;
            hurtColorStartTimestamp = timer - hurtColorRiseDuration * percentLeft;
        }
    }

    public float getMaxHealth() {
        return MaxHealth;
    }

    public float getCurrentHealth() {
        return Health;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRadius);
    }
}
