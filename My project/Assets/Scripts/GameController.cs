using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  
    public float mouseSensitivity = 1.5f;  
    public float jumpForce = 2f; 
    public float boostForce = 1f;  
    public float boostDuration = 0.5f; 
    public float boostCooldown = 10f;  
    public Transform cameraTransform;  
    public TextMeshProUGUI boostTimerText;  

    private CharacterController controller;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private float xRotation = 0f;
    private bool canBoost = true;  
    private float boostTimer = 0f;  
    private bool isBoosting = false; 
    private float boostTimeRemaining = 0f;  

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MouseLook();

        MovePlayer();

        BoostPlayer();

        RechargeBoost();

        UpdateBoostTimerUI();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");  // A/D
        float z = Input.GetAxis("Vertical");  // W/S

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    void BoostPlayer()
    {
        if (Input.GetKeyDown(KeyCode.E) && canBoost)
        {
            isBoosting = true;
            boostTimeRemaining = boostDuration;
            canBoost = false;
            boostTimer = boostCooldown;
        }

        if (isBoosting)
        {
            Vector3 boostDirection = transform.forward;
            controller.Move(boostDirection * boostForce * Time.deltaTime);

            boostTimeRemaining -= Time.deltaTime;

            if (boostTimeRemaining <= 0)
            {
                isBoosting = false;
            }
        }
    }

    void RechargeBoost()
    {
        if (!canBoost)
        {
            boostTimer -= Time.deltaTime;

            if (boostTimer <= 0f)
            {
                canBoost = true;
                boostTimer = 0f;
            }
        }
    }

    void UpdateBoostTimerUI()
    {
        if (canBoost)
        {
            boostTimerText.text = "Boost Ready!";
        }
        else
        {
            boostTimerText.text = "Boost Recharge: " + Mathf.Ceil(boostTimer).ToString() + "s";
        }
    }
}
