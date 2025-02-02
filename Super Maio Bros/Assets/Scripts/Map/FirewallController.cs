using System.Collections;
using UnityEngine;

public class FirewallController : MonoBehaviour {
    public float moveSpeed = 0.16f;
    public float maxMoveSpeed = 1.4f;
    public float speedIncrement = 0.02f;
    public Vector2 startPos;
    public Vector2 endPos;

    private bool isMovingRight = true;
    private int rightDirCount = 0;
    private AudioSource audioSource;
    private bool alreadyTriggered = false;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    void FixedUpdate() {
        Vector3 targetPos = isMovingRight ? (Vector3)endPos : (Vector3)startPos;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed);
    
        if (Vector3.Distance(transform.position, targetPos) < 0.1f) {
            isMovingRight = !isMovingRight;
            if (isMovingRight) {
                rightDirCount++;
                // A cada 5x que a parede vai pra direita ela aumentar a velocidade
                if (rightDirCount % 5 == 0 && moveSpeed <= maxMoveSpeed) {
                    moveSpeed += speedIncrement;
                    Debug.Log("Firewall: Speed Increases");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (alreadyTriggered) return;
        alreadyTriggered = true;
        
        if (other.CompareTag("Player")) {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                playerHealth.TakeDamage(1);
                StartCoroutine(RestartTrigger());
            }
        }
    }

    IEnumerator RestartTrigger() {
        yield return new WaitForSeconds(0.75f);
        alreadyTriggered = false;
    }
}
