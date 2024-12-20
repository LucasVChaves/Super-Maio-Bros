using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform target;
    [SerializeField]
    private float followTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private new Camera camera;

    void Start() {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate() {
        Vector3 point = camera.WorldToViewportPoint(target.position);
		Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
		Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, followTime);
    }
}
