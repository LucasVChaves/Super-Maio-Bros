using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 5f;
    private Vector2 velocity;
    private Rigidbody2D rbody;
    private SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        rbody.MovePosition(rbody.position + velocity * moveSpeed * Time.fixedDeltaTime);
        if (velocity.x > 0) {
            spriteRenderer.flipX = true;
        } else if (velocity.x < 0) {
            spriteRenderer.flipX = false;
        } else if (velocity.y > 0) {
            spriteRenderer.flipY = false;
        } else if (velocity.y < 0) {
            spriteRenderer.flipY = true;
        } 
    }
}
