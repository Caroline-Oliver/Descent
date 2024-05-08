using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Creature creature;
    LineRenderer lineRenderer;
    float width;
    float height;

    void Awake() {
        creature = GetComponent<Creature>();
        if (creature == null) {
            Debug.LogError("No creature component found!!!");
        }
        lineRenderer = GetComponentInChildren<LineRenderer>();
        if (lineRenderer == null) {
            Debug.LogError("No line renderer child component found!!!");
        }
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.5f;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (creature.getCurrentHealth() != creature.getMaxHealth())
        {
            DrawHealthBar(creature.getCurrentHealth(), creature.getMaxHealth());
        }
        else {
            EraseHealthBar();
        }
    }

    void DrawHealthBar(float currentHealth, float maxHealth) {
        Vector3 startPoint = new Vector3(transform.position.x - width/2, transform.position.y + height, -10);
        Vector3 endPoint = new Vector3(transform.position.x - width/2 + currentHealth/maxHealth, transform.position.y + height, -10);

        Vector3[] pos = {startPoint, endPoint};

        lineRenderer.startColor = new Color(0xFF, 0x00, 0x00, 0.4f);
        lineRenderer.endColor = Color.red;

        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .1f;
        lineRenderer.SetPositions(pos);
    }
    
    void EraseHealthBar() {
        Vector3[] empty = {};
        lineRenderer.startColor = Color.clear;
        lineRenderer.endColor = Color.clear;
        lineRenderer.SetPositions(empty);
    }
}
