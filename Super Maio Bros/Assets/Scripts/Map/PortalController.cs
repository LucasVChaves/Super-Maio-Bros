using UnityEngine;

public class PortalController : MonoBehaviour {
    public float tpOffset = 0f;
    public GameObject targetPortal;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            audioSource.Play();
            Vector3 newPos = new Vector3(targetPortal.transform.position.x + tpOffset, 
                                        targetPortal.transform.position.y);
            other.transform.position = newPos;
        }
    }
}
