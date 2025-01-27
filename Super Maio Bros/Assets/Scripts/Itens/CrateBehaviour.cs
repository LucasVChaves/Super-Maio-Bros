using UnityEngine;

public class CrateBehaviour : MonoBehaviour {
    CrateLoot loot;

    void Start() {
        loot = GetComponent<CrateLoot>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DestroySelf();
        }
    }

    public void DestroySelf() {
        // Particula()
        // Som()
        loot.rollLoot();
        Destroy(gameObject);
    }
}
