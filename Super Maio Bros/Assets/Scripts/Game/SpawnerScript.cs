using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps; // ✅ Importa para trabalhar com Tilemaps

public class SpawnerScript : MonoBehaviour {
    public GameObject[] objectsToSpawn; // Objetos a spawnar (caixas, pedras, etc.)
    public float spawnInterval = 10f; // Tempo entre spawns
    public int maxObjectsOnMap = 30; // Máximo de caixas no mapa
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    public float objectRadius = 0.5f; // Raio para verificar colisões

    void Start() {
        Tilemap tilemap = FindObjectOfType<Tilemap>(); // 🔹 Busca um Tilemap na cena
        if (tilemap != null) {
            BoundsInt tilemapBounds = tilemap.cellBounds;
            spawnAreaMin = new Vector2(tilemapBounds.xMin, tilemapBounds.yMin); // Ponto inferior esquerdo
            spawnAreaMax = new Vector2(tilemapBounds.xMax, tilemapBounds.yMax); // Ponto superior direito
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

            int currentObjects = GameObject.FindGameObjectsWithTag("Explodable").Length; // Conta caixas

            if (currentObjects < maxObjectsOnMap) {
                int amountToSpawn = Random.Range(3, 6); // Número aleatório entre 3 e 6

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
        for (int attempts = 0; attempts < 10; attempts++) { // Tenta spawnar até 10 vezes
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 potentialPosition = new Vector2(randomX, randomY);

            // Verifica se já tem algo na posição
            Collider2D hit = Physics2D.OverlapCircle(potentialPosition, objectRadius);

            if (hit == null) { // Se não colidiu com nada, retorna a posição válida
                return potentialPosition;
            }
        }

        return Vector2.zero; // Retorna zero se não encontrar posição válida
    }
}
