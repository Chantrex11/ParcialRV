using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public AudioSource rodar;
    public float speed = 6f;
    public float rotationSmooth = 0.1f;

    [Header("Salto")]
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    [Header("Cámara")]
    public Transform cam;               
    public float mouseSensitivity = 3f;
    public float visionMin = -30f;
    public float visionMax = 60f;
    public float camDistance = 5f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float turnSmoothVelocity;

    private float yaw = 0f;
    private float pitch = 10f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MovePlayer();
        ApplyGravityAndJump();
    }

    void LateUpdate()
    {
        MoveCamera();
    }

private void MovePlayer()
{
    float h = Input.GetAxisRaw("Horizontal");
    float v = Input.GetAxisRaw("Vertical");
    Vector3 dir = new Vector3(h, 0, v).normalized;

    if (dir.magnitude >= 0.1f)
    {
        // Solo reproducir si NO se está reproduciendo
        if (!rodar.isPlaying)
        {
            rodar.Play();
        }

        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmooth);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir * speed * Time.deltaTime);
    }
    else
    {
        // Si no hay movimiento, parar el audio
        if (rodar.isPlaying)
        {
            rodar.Stop();
        }
    }
}


    private void ApplyGravityAndJump()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void MoveCamera()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, visionMin, visionMax);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = transform.position + rotation * new Vector3(0, 0, -camDistance);

        cam.position = desiredPosition;
        cam.LookAt(transform.position); 
    }
}
