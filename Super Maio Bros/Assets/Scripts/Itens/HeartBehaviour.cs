using UnityEngine;
using System.Collections;

public class HeartBehaviour : MonoBehaviour {
    private AudioSource audioSource;
    private Collider2D col;
    private bool alreadyTriggered = false;
    void Start() {
        col = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator DestroySelf() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Mesma gambiarra do diamente
        if (alreadyTriggered) return;
        alreadyTriggered = true;

        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<PlayerHealth>().Heal(1);
            audioSource.Play();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            col.enabled = false;
            StartCoroutine(DestroySelf());
        }
    }
}
