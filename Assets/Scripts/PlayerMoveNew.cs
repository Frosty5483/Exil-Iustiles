using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoveNew : MonoBehaviour
{
    [Header("Third Person Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float rotationSpeed = 12f;

    [Header("First Person Movement")]
    public float fpWalkSpeed = 5f;
    public float fpRunSpeed = 8f;
    public float mouseSensitivity = 2.5f;
    public float mouseSmoothTime = 0.03f;

    [Header("First Person Smoothing")]
    public float fpAcceleration = 20f;
    public float fpDeceleration = 25f;

    Vector3 fpVelocity;

    [Header("Jump")]
    public float jumpHeight = 1.4f;
    public float gravity = -35f;
    public float jumpAnticipation = 0.08f;
    public float coyoteTime = 0.15f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.25f;
    public LayerMask groundLayer;

    [Header("Cameras")]
    public CinemachineCamera thirdPersonCam;
    public Camera firstPersonCam;
    public Transform playerHead;

    [Header("Animation")]
    public Animator animator;
    public float idleLookDelay = 5f;

    [Header("Rendering")]
    public SkinnedMeshRenderer bodyMesh;

    CharacterController controller;

    Vector3 velocity;
    Vector2 currentMouseDelta;
    Vector2 mouseDeltaVelocity;

    bool inDialogue;

    bool isGrounded;
    bool isJumping;
    bool jumpQueued;

    float coyoteTimer;
    float jumpTimer;
    float idleTimer;
    float xRotation;

    public bool IsFirstPerson => firstPersonCam.enabled;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ActivateThirdPerson();
    }

    void Update()
    {
        CheckGround();

        if (IsFirstPerson)
        {
            HandleFirstPersonLook();
            HandleFirstPersonMovement();
        }
        else
        {
            HandleThirdPersonMovement();
        }

        HandleJumpInput();
        HandleJumpPhysics();
        HandleCameraSwitch();

        Vector3 finalMove = Vector3.zero;

        if (IsFirstPerson)
            finalMove += fpVelocity;

        finalMove.y = velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }

    public void EnterDialogue()
    {
        inDialogue = true;

        // Always show body during dialogue
        if (bodyMesh)
            bodyMesh.enabled = true;

        // Animator should always be on for dialogue
        animator.enabled = true;
    }

    public void ExitDialogue()
    {
        inDialogue = false;

        // Restore correct state depending on camera
        if (IsFirstPerson)
        {
            if (bodyMesh)
                bodyMesh.enabled = false;

            animator.enabled = false;
        }
        else
        {
            if (bodyMesh)
                bodyMesh.enabled = true;

            animator.enabled = true;
        }
    }

    // ---------------- GROUND ----------------
    void CheckGround()
    {
        bool groundedNow = Physics.CheckSphere(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        if (groundedNow)
        {
            isGrounded = true;
            isJumping = false;
            coyoteTimer = coyoteTime;
            if (velocity.y < 0) velocity.y = -2f;
        }
        else
        {
            isGrounded = false;
            coyoteTimer -= Time.deltaTime;
        }

        animator.SetBool("isGrounded", isGrounded);
    }

    // ---------------- THIRD PERSON ----------------
    void HandleThirdPersonMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, 0, v);
        bool moving = input.sqrMagnitude > 0.01f;
        bool running = Input.GetKey(KeyCode.LeftShift);

        Transform cam = Camera.main.transform;
        Vector3 camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = cam.right;

        Vector3 moveDir = camForward * v + camRight * h;

        if (moving)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }

        float speed = running ? runSpeed : walkSpeed;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);

        UpdateAnimator(moving, running);
    }

    // ---------------- FIRST PERSON LOOK ----------------
    void HandleFirstPersonLook()
    {
        Vector2 rawMouseDelta = new Vector2(
            Input.GetAxisRaw("Mouse X"),
            Input.GetAxisRaw("Mouse Y")
        ) * mouseSensitivity;

        currentMouseDelta = Vector2.SmoothDamp(
            currentMouseDelta,
            rawMouseDelta,
            ref mouseDeltaVelocity,
            mouseSmoothTime
        );

        xRotation -= currentMouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * currentMouseDelta.x);
    }

    // ---------------- FIRST PERSON MOVE ----------------
    void HandleFirstPersonMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = (transform.forward * v + transform.right * h);
        inputDir = Vector3.ClampMagnitude(inputDir, 1f);
        bool hasInput = inputDir.sqrMagnitude > 0.01f;

        float targetSpeed = Input.GetKey(KeyCode.LeftShift) ? fpRunSpeed : fpWalkSpeed;

        if (hasInput)
        {
            
            fpVelocity = inputDir.normalized * targetSpeed;
        }
        else
        {
            
            fpVelocity = Vector3.zero;
        }

        controller.Move(fpVelocity * Time.deltaTime);

        UpdateAnimator(hasInput, Input.GetKey(KeyCode.LeftShift));
    }

    // ---------------- ANIMATION ----------------
    void UpdateAnimator(bool moving, bool running)
    {
        animator.SetBool("walking", moving && !running);
        animator.SetBool("running", moving && running);

        if (!moving)
        {
            idleTimer += Time.deltaTime;
            animator.SetBool("idleLook", idleTimer >= idleLookDelay);
        }
        else
        {
            idleTimer = 0;
            animator.SetBool("idleLook", false);
        }
    }

    // ---------------- JUMP ----------------
    void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && coyoteTimer > 0 && !isJumping)
        {
            jumpQueued = true;
            jumpTimer = jumpAnticipation;
            animator.SetTrigger("jump");
        }
    }

    void HandleJumpPhysics()
    {
        if (jumpQueued)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                jumpQueued = false;
                isJumping = true;
                coyoteTimer = 0f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // ---------------- CAMERA SWITCH ----------------
    void HandleCameraSwitch()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (IsFirstPerson)
                ActivateThirdPerson();
            else
                ActivateFirstPerson();
        }
    }

    public void ActivateFirstPerson()
    {
        thirdPersonCam.gameObject.SetActive(false);
        firstPersonCam.enabled = true;

        if (bodyMesh)
            bodyMesh.enabled = false;

        animator.enabled = false;
    }

    public void ActivateThirdPerson()
    {
        thirdPersonCam.gameObject.SetActive(true);
        firstPersonCam.enabled = false;

        if (bodyMesh)
            bodyMesh.enabled = true;

        animator.enabled = true;

        playerHead.localRotation = Quaternion.identity;
    }
}
