using UnityEngine;
using System.Collections;

public class DiamondBehaviour : MonoBehaviour {
    private AudioSource audioSource;
    private Collider2D col;
    private bool alreadyTriggered = false;
    void Start() {
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void AddScoreToPlayer() {
        ScoreManager.Instance.AddScore(50);
        audioSource.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        col.enabled = false;
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Gambiarra pra arrumar o trigger sendo ativado 2x
        if (alreadyTriggered) return;
        alreadyTriggered = true;

        if (other.CompareTag("Player")) AddScoreToPlayer();
    }
}
