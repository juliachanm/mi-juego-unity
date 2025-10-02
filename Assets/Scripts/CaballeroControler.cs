using UnityEngine;

public class CaballeroControler : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 3f;
    private float moveInput;

    [Header("Salto")]
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Disparo")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 1f;

    private Animator anim;
    private Rigidbody2D rb;
    private bool isFacingRight = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Cargar posición si existe
        LoadPosition();
    }

    void Update()
    {
        // Movimiento horizontal
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);

        // Animación de correr
        bool isRunning = Mathf.Abs(moveInput) > 0.01f;
        anim.SetBool("isRunning", isRunning);

        // Flip
        if (moveInput > 0 && !isFacingRight)
            Flip();
        else if (moveInput < 0 && isFacingRight)
            Flip();

        // Verificar si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Disparar (Z)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("isShooting", true);
            ShootFireball();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("isShooting", false);
        }

        // Guardar / Cargar
        if (Input.GetKeyDown(KeyCode.S)) SavePosition();
        if (Input.GetKeyDown(KeyCode.L)) LoadPosition();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        firePoint.localPosition = new Vector3(-firePoint.localPosition.x, firePoint.localPosition.y, firePoint.localPosition.z);
    }

    void ShootFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rbFireball = fireball.GetComponent<Rigidbody2D>();
        float direction = isFacingRight ? 1f : -1f;

        Physics2D.IgnoreCollision(fireball.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        rbFireball.velocity = new Vector2(direction * fireballSpeed, 0);
    }

    void SavePosition()
    {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        Debug.Log("Posición guardada");
    }


    void LoadPosition()
    {
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            transform.position = new Vector2(x, y);
            Debug.Log("Posición cargada");
        }
        else
        {
            Debug.Log("No hay posición guardada");
        }
    }
}
