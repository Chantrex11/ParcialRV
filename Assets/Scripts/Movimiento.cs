using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoMaleta : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 10f;
    public float mouseSensitivity = 2f;
    public float verticalRotation = 0f;
    public float jumpForce = 2f;
    
    private float mouseX;
    private float mouseY;
    private float horizontalInput;
    private float verticalInput;
    private bool isGrounded;
    private Vector3 velocity;
    private float gravity = -9.81f;

    [Header("Referencias")]
    public Transform maletaCamara;
    private CharacterController controller;

    void Start()
    {
        // Bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        // Rotación con el mouse
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        maletaCamara.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Movimiento con teclas
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move * speed * Time.deltaTime);

        //Gravedad
        isGrounded = controller.isGrounded;
        if (!isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
