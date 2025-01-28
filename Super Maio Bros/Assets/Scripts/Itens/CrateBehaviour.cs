using System.Collections;
using UnityEngine;

public class CrateBehaviour : MonoBehaviour {
    CrateLoot loot;

    void Start() {
        loot = GetComponent<CrateLoot>();
    }

    public void Destroy() {
        // Particula()
        // Som()
        loot.rollLoot();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
