using UnityEngine;
using UnityEngine.SceneManagement;

public class threeDMovement : MonoBehaviour
{

    public Rigidbody rb;
    public Animator anim;
    //public GoldCollector gc;
    public float speed;
    public Transform orientation;
    

    public float playerHeight;
    public LayerMask whatIsGround;
    public float groundDrag;
    public bool isAlive;

    private float horizontalInput;
    private float verticalInput;
    private bool grounded;
    private KeyCode restartKey;
    private KeyCode skipKey;

    private Vector3 moveDirection;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        restartKey = KeyCode.R;
        skipKey = KeyCode.N;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();

        //handle drag
        if (grounded)
        {
            rb.linearDamping = groundDrag;
        }else if (!grounded)
        {
            rb.linearDamping = 0;
        }

        // Restart button
        if (Input.GetKeyDown (restartKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Makes it so then it will load the current scene it is on. 
        }

        // Skip button
        if (Input.GetKeyDown (skipKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Makes it so then it will load the current scene it is on. 
        }

        // Sprinting code
        if (Input.GetKey(KeyCode.LeftShift))
        {
            groundDrag = 5;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            groundDrag = 10;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    //Handles the input of the player
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }

    // counts how many Gold collected
    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gold"))
        {
            gc.goldCount --;
            Destroy(other.gameObject);
            Debug.Log("Gold Collected!");
        }

        if (gc.goldCount == 1)
        {
            AudioManager.instance.Play("Spook");
        }
    }*/
}
