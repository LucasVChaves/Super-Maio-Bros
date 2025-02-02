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

        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(DestroySelf());
    }

    void ExplodeInDirection(Vector2 dir) {
        for (int i = 1; i <= explosionRange; i++) {
            Vector2 targetPos = (Vector2)transform.position + (dir * i);

            Collider2D[] hits = Physics2D.OverlapCircleAll(targetPos, 0.4f);

            foreach (Collider2D hit in hits) {
                
                CrateBehaviour crate = hit.GetComponent<CrateBehaviour>();
                if (crate != null) {
                    crate.Destroy();
                    continue;
                }
                StoneBehaviour stone = hit.GetComponent<StoneBehaviour>();
                if (stone != null) {
                    stone.DestroyStone();
                    continue;
                }
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null) {
                    playerHealth.TakeDamage(1);
                }
            }

            Instantiate(explosionPrefab, targetPos, Quaternion.identity);
        }
    }

    IEnumerator DestroySelf() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        foreach (Vector2 dir in directions) {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + dir * explosionRange);
        }
    }
}
