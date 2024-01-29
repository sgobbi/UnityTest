using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;


public class InputManager : NetworkBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    private PlayerPickUpDrop pickupDrop;

    private bool movementBlocked;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        onFoot.Enable();
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        pickupDrop =  GetComponent<PlayerPickUpDrop>();

        onFoot.Grab.performed += ctx => Grab();
        onFoot.Focus.performed += ctx => Focus();
      
    }

    // Update is called once per frame
    void Update()
    {
        //dire au playermotor de bouger en fonction de la valeur du movement action
        /* if(Input.GetKeyDown(KeyCode.F) && pickUpDrop.grabbableObject!=null)
        {      
            movementBlocked = !movementBlocked;
            Debug.Log(movementBlocked);  
        } */
        if(movementBlocked == false)
        {
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }
        
    }

    private void LateUpdate()
    {
        if(movementBlocked==false)
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }
        
    }

    void Grab()
    {
        Debug.Log("grab event called");
        pickupDrop.GrabActionToggled();
    }

    void Focus()
    {
        Debug.Log("focus event called");
        if(pickupDrop.grabbableObject!=null)
        {      
            movementBlocked = !movementBlocked;
            Debug.Log(movementBlocked);  
        }
        pickupDrop.FocusActionToggled();       

    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

}
