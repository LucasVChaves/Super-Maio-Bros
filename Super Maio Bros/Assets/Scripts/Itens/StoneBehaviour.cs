using System.Collections;
using UnityEngine;

public class StoneBehaviour : MonoBehaviour {
    private Collider2D col;

    void Start() {
        col = GetComponent<Collider2D>();
    }

    public void DestroyStone() {
        Debug.Log("Pedra destruída: " + gameObject.name);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        col.enabled = false;
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Explosion")) {
            DestroyStone();
        }
    }
}
