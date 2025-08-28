using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimientopj : MonoBehaviour
{
    public float speed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera;
    public float verticalRotation = 0f;
    public float jumpForce = 2f;
    public float gravity = -9.81f;
    public float pushForce = 3f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private Animator anim;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -45f, 55f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Gravedad
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(z != 0)
            {
                anim.SetFloat("trote", Math.Abs(x) + Math.Abs(z));
            }
            else
            {
                anim.SetFloat("trote", 0);
            }

        if (x < 0)
        {
            anim.SetFloat("izq", Math.Abs(x));
        }
        else
        {
            anim.SetFloat("izq", 0);
        }

        if (x > 0)
        {
            anim.SetFloat("der", Math.Abs(x));
        }
        else
        {
            anim.SetFloat("der", 0);
        }

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.5f))
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null && !rb.isKinematic)
            {
                rb.AddForce(playerCamera.forward * pushForce, ForceMode.Impulse);
            }
        }
    }
}
