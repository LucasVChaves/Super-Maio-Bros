using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float moveSpeed = 7f;
    private Vector2 velocity;
    private Rigidbody2D rbody;
    private Animator animator;

    void Start() {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        velocity = velocity.normalized;

        animator.SetFloat("Horizontal", velocity.x);
        animator.SetFloat("Vertical", velocity.y);
        animator.SetFloat("Speed", velocity.sqrMagnitude);

        if (velocity.x < 0) {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        } else {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    void FixedUpdate() {
        rbody.MovePosition(rbody.position + velocity * moveSpeed * Time.fixedDeltaTime);
    }
}
