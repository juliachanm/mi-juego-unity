using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Captura del input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Cambiar animación
        bool isWalking = movement != Vector2.zero;
        animator.SetBool("isWalking", isWalking);

        // Movimiento
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }
}

