using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;

    public float speed = 5f;
    public float speedIncreasePerPoint = 0.1f;

    Rigidbody rb;
    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 2f;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;

        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    void Update()
    {
        if (!alive) return;

        horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("moveAmount", 1f);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        if (transform.position.y < -5f)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.name.Contains("Obstacle2"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (!alive) return;

        alive = false;

        Invoke("Restart", 0.5f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.2f, groundMask);

        rb.AddForce(Vector3.up * jumpForce);
    }
}