using System.Collections;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {
    public GameObject explosionPrefab;
    public float explosionDelay = 1.5f;
    public int explosionRange = 2;
    [SerializeField] private LayerMask obstacleLayer;
    private AudioSource audioSource;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        obstacleLayer = LayerMask.GetMask("Obstacle");
        StartCoroutine(Explode());
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(explosionDelay);

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        audioSource.Play();
        ExplodeInDirection(Vector2.up);
        ExplodeInDirection(Vector2.down);
        ExplodeInDirection(Vector2.left);
        ExplodeInDirection(Vector2.right);

        // Desliga a parte visual da bomba e deixa o objeto em si ativo
        // Se eu destruir o objeto da bomba sem isso o audio buga
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(DestroySelf());
    }

    void ExplodeInDirection(Vector2 dir) {
        for (int i = 1; i <= explosionRange; i++) {
            Vector2 targetPos = (Vector2)transform.position + (dir * i);
            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero, 0f);

            if (hit.collider != null) {
                Debug.Log("Obstáculo atingido: " + hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Explodable")) {
                    hit.collider.GetComponent<CrateBehaviour>().Destroy();
                }
                if (hit.collider.CompareTag("Player")) {
                    hit.collider.GetComponent<PlayerHealth>().TakeDamage(1);
                }
                break;
            }

            Instantiate(explosionPrefab, targetPos, Quaternion.identity);
        }
    }

    IEnumerator DestroySelf() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Gizmos para debug
    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.3f);

        Gizmos.color = Color.red;

        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (Vector2 dir in directions) {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + dir * explosionRange);
        }
    }
}
