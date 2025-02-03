using System.Collections;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour {
    private CrateLoot loot;
    private Collider2D col;

    void Start() {
        loot = GetComponent<CrateLoot>();
        col = GetComponent<Collider2D>();
    }

    public void Destroy() {
        Debug.Log("Caixa destruída: " + gameObject.name);

        loot.rollLoot(); // Dropa loot
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        col.enabled = false; // Desativa o colisor para evitar interações futuras
        
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Explosion")) {
            Destroy();
        }
    }
}
