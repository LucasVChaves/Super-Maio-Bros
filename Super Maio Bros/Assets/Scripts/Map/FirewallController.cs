using UnityEngine;

public class FirewallController : MonoBehaviour {
    public float moveSpeed = 0.12f;
    public float speedIncrement = 0.02f;
    public Vector2 startPos;
    public Vector2 endPos;

    private bool isMovingRight = true;
    private int rightDirCount = 0;

    void FixedUpdate() {
        Vector3 targetPos = isMovingRight ? (Vector3)endPos : (Vector3)startPos;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
    
        if (Vector3.Distance(transform.position, targetPos) < 0.1f) {
            isMovingRight = !isMovingRight;
            if (isMovingRight) {
                rightDirCount++;
                // A cada 5x que a parede vai pra direita ela aumentar a velocidade
                if (rightDirCount % 5 == 0 && moveSpeed <= 1.2f) {
                    moveSpeed += speedIncrement;
                    Debug.Log("Firewall: Speed Increases");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                playerHealth.TakeDamage(1);
            }
        }
    }
}
