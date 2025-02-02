using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnerScript : MonoBehaviour {
    public GameObject[] objectsToSpawn;
    public float spawnInterval = 10f;
    public int maxObjectsOnMap = 30;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public float objectRadius = 0.5f;

    void Start() {
        Tilemap tilemap = FindObjectOfType<Tilemap>();
        if (tilemap != null) {
            BoundsInt tilemapBounds = tilemap.cellBounds;
            spawnAreaMin = new Vector2(tilemapBounds.xMin, tilemapBounds.yMin);
            spawnAreaMax = new Vector2(tilemapBounds.xMax, tilemapBounds.yMax);
        } else {
            Debug.LogWarning("Tilemap não encontrado! Defina a área manualmente.");
            spawnAreaMin = new Vector2(-5, -3);
            spawnAreaMax = new Vector2(5, 3);
        }

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects() {
        while (true) {
            yield return new WaitForSeconds(spawnInterval);

            int currentObjects = GameObject.FindGameObjectsWithTag("Explodable").Length;

            if (currentObjects < maxObjectsOnMap) {
                int amountToSpawn = Random.Range(3, 6);

                for (int i = 0; i < amountToSpawn; i++) {
                    Vector2 spawnPosition = GetValidSpawnPosition();

                    if (spawnPosition != Vector2.zero) {
                        int randomIndex = Random.Range(0, objectsToSpawn.Length);
                        Instantiate(objectsToSpawn[randomIndex], spawnPosition, Quaternion.identity);
                    }
                }
            }
        }
    }

    Vector2 GetValidSpawnPosition() {
        for (int attempts = 0; attempts < 10; attempts++) {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 potentialPosition = new Vector2(randomX, randomY);

            Collider2D hit = Physics2D.OverlapCircle(potentialPosition, objectRadius);

            if (hit == null) {
                return potentialPosition;
            }
        }

        return Vector2.zero;
    }
}
