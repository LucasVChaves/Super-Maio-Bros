using System.Collections;
using UnityEngine;

public class BombBehaviour : MonoBehaviour {
    public GameObject explosionPrefab;
    public float explosionDelay = 1.5f;
    public int explosionRange = 5;
    public LayerMask obstacleLayer;
    private AudioSource audioSource;
    
    void Start() {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Explode());
    }

    IEnumerator Explode() {
        yield return new WaitForSeconds(explosionDelay);

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
        for (int i = 1; i < explosionRange; i++) {
            Vector2 targetPos = (Vector2)transform.position + dir * i;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, i, obstacleLayer);
            if (hit.collider != null) {
                //Debug.Log("Raycast atingiu: " + hit.collider.gameObject.name);
                break;
            }

            Instantiate(explosionPrefab, targetPos, Quaternion.identity);

            Collider2D obj = Physics2D.OverlapCircle(targetPos, 1.0f);
            if (obj != null) {
                if (obj.CompareTag("Explodable")) {
                    obj.gameObject.GetComponent<CrateBehaviour>().Destroy();
                }
                if (obj.CompareTag("Player")) {
                    PlayerHealth playerHealth = obj.gameObject.GetComponent<PlayerHealth>();
                    if (playerHealth != null) playerHealth.TakeDamage(1);
                }
            }
        }
    }

    IEnumerator DestroySelf() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    // Gizmos para debug
    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, 0.8f);

        Gizmos.color = Color.red;

        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (Vector2 dir in directions) {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + dir * explosionRange);
        }
    }
}
