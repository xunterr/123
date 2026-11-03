using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    public int speed;
    public int rotSpeed;
    public int sens;
    public GameObject movables;

    CharacterController controller;
    Animator animator;

    private float xRotation;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerMovement = transform.right*rightJoystick.Horizontal + transform.forward*rightJoystick.Vertical;
        Vector3 movablesMovement = new Vector3(0,0,leftJoystick.Vertical);
        controller.Move(playerMovement * speed * Time.deltaTime);

        float moveX = leftJoystick.Horizontal * rotSpeed *Time.deltaTime;
        float moveY = leftJoystick.Vertical * sens *Time.deltaTime;
        transform.Rotate(Vector3.up*moveX);

        xRotation -= moveY;
        xRotation = Math.Clamp(xRotation, -90f, 90f);
        movables.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        Vector3 velocity = new Vector3();
        velocity.y += -9.8f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        animator.SetBool("IsRunning", playerMovement != Vector3.zero);
    }
}
