using UnityEngine;
using UnityEngine.SceneManagement;

public class platformMovement: MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    //public EggCounter ec;
    public int jumpForce; 
    public float mSpeed;

    private bool isGrounded;
    private KeyCode leftMoveKey;
    private KeyCode restartKey;
    [SerializeField] private AudioClip jumpingSound;
    [SerializeField] private AudioClip pickUpSound;
    private AudioSource audioSource;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftMoveKey = KeyCode.A;
        restartKey = KeyCode.R;
        // References the Rigidbody2D from the object itself
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Checks if the player is on the ground. 
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // Sets the peramiter in the Animator so it knows to go back to idling. 
            animator.SetBool("IsJumping", false);
            animator.Play("Oscar_Idle");
        }
    }

     public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("IsJumping", true);
            animator.Play("Oscar_Jump");

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Allows the player to jump only when they are grounded
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            //Play sound FX
            audioSource.clip = jumpingSound;
            audioSource.Play();
        }

        // Allows the player to move Left
        if (Input.GetKey(leftMoveKey))
        {
            rb.AddForce(Vector2.left * mSpeed, ForceMode2D.Impulse);
            transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
            animator.SetBool("IsWalking", true);
        } else 
        {
            animator.SetBool("IsWalking", false);
        }

        // Allows the player to move right
         if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
            rb.AddForce(Vector2.right * mSpeed, ForceMode2D.Impulse);
            animator.SetBool("IsWalking", true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown (restartKey)) // Restart button
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Makes it so then it will load the current scene it is on. 
        }
    }

    // counts how many eggs collected and disables them from the scene.
    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Egg"))
        {
            ec.eggCount++;
            other.gameObject.SetActive(false);
            Debug.Log("Egg Collected!");

            audioSource.clip = pickUpSound;
            audioSource.Play();
        }
    }*/
}
