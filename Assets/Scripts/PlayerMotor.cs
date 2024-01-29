using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using Mirror;


public class PlayerMotor : NetworkBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool isGrounded;
    public float gravity = -9.8f;
    public float speed = 5f;

    private bool movementBlocked = false;
    private Rigidbody objectRigidBody;  
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        objectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if(Input.GetKeyDown(KeyCode.F))
        {      
            movementBlocked = !movementBlocked;
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;  
        controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded&&playerVelocity.y<0)
            {
                playerVelocity.y = -2f;
            }
                
        controller.Move(playerVelocity*Time.deltaTime);       
    }
    
}
    

