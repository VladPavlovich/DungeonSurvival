using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import the TMP namespace

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;  // Player movement speed
    public float mouseSensitivity = 1.5f;  // Reduced mouse sensitivity for slower looking around
    public float jumpForce = 2f;  // Lower force applied when the player jumps
    public float boostForce = 1f;  // Force applied when boosting
    public float boostDuration = 0.5f;  // How long the boost lasts in seconds
    public float boostCooldown = 10f;  // Boost cooldown time in seconds
    public Transform cameraTransform;  // Reference to the player's camera
    public TextMeshProUGUI boostTimerText;  // Reference to the TMP Text for boost timer

    private CharacterController controller;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private float xRotation = 0f;
    private bool canBoost = true;  // Whether the player can use the boost
    private float boostTimer = 0f;  // Timer to track boost recharge
    private bool isBoosting = false;  // Track if the player is boosting
    private float boostTimeRemaining = 0f;  // Time left for boost duration

    void Start()
    {
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;

        // Get the CharacterController component attached to the player
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Handle mouse look
        MouseLook();

        // Handle player movement
        MovePlayer();

        // Handle boost mechanism
        BoostPlayer();

        // Recharge boost
        RechargeBoost();

        // Update the boost timer text in the UI
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
