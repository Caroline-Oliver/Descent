using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    [SerializeField] public float spawnWidth;
    [SerializeField] public float spawnHeight;
    [SerializeField] public float spawnVerticalOffset;
    [SerializeField] public float spawnHorizontalOffset;
    [SerializeField] GameState gameState;
    [SerializeField] GameObject enemyPrefab;
    private int[] progression = {2, 3, 5, 7, 10};

    // Start is called before the first frame update
    void Start()
    {
        if (gameState == null) {
            SpawnEnemies(progression[0]);
        }
        else {
            gameState.NextLevel();
            SpawnEnemies(progression[gameState.GetLevel()]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemies(int countToSpawn) {
        int countSpawned = 0;
        int attempts = 0;
        int max_attempts = 100;
        //float randomX = Random.Range(-spawnRange,spawnRange);
        while (attempts++ < max_attempts && countSpawned < countToSpawn) {
            float randomX = Random.Range(-(spawnWidth/2), spawnWidth/2) + spawnHorizontalOffset;
            float randomY = Random.Range(-(spawnHeight/2), spawnHeight/2) + spawnVerticalOffset;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(randomX, randomY), new Vector2(1, 1), 0);

            if (colliders.Length == 0) {
                Instantiate(enemyPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
                countSpawned++;
            }
        }

        Debug.LogFormat("spawned={0}, attempts={1}", countSpawned, attempts);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(
            new Vector3(transform.position.x + spawnHorizontalOffset, transform.position.y + spawnVerticalOffset, transform.position.z),
            new Vector3(spawnWidth, spawnHeight, 0)
        );
    }
}
