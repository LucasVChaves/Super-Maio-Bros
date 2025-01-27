using UnityEngine;

public class PortalController : MonoBehaviour {
    public float tpOffset = 0f;
    public GameObject targetPortal;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Vector3 newPos = new Vector3(targetPortal.transform.position.x + tpOffset, 
                                        targetPortal.transform.position.y);
            other.transform.position = newPos;
        }
    }
}
