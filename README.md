// HackAThon
//Making a game in 24 hours

//Code for player movement

private float horizontalInput;
    private float verticalInput;
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    private Vector3 velocity;
    private bool isGrounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Create a new Vector3 called move, that will essentially point in the direction that the player will move
        //If we use transform.Translate, it will move the player on the global variables while this will move on the local var.
        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;

        //Use the controller component to get the player to move
        controller.Move(move * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }