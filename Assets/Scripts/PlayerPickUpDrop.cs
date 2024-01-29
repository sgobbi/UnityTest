using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Mirror;


public class PlayerPickUpDrop : NetworkBehaviour
{
    
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private Transform grabbedObjectPointTransform;
    [SerializeField] private Transform focusedObjectPointTransform;
    private float pickupDistance = 3f;
    public GrabbableObject grabbableObject;
    public NetworkIdentity playerID;


    void Awake()
    {
        playerID = GetComponent<NetworkIdentity>();
    }
    // Update is called once per frame
    void Update()
    {
      /*  if(Input.GetKeyDown(KeyCode.E))
       {
        if(grabbableObject == null)
        {
            if(Physics.Raycast(playerCameraTransform.position,playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance))
            {
                Debug.Log(raycastHit.transform);
                if(raycastHit.transform.TryGetComponent(out grabbableObject))
                {
                    NetworkIdentity objectID = grabbableObject.objectID;  
                    CmdAssignAuthorityToPlayer(objectID);                  
                    Debug.Log(grabbableObject);
                    grabbableObject.Grab(grabbedObjectPointTransform, playerID);
                }
            }
        }
        else
        {
            grabbableObject.Drop();
            grabbableObject = null;
        } */
            

       //}
       /* if(Input.GetKeyDown(KeyCode.F))
       {
        if(grabbableObject != null)
        {           
            grabbableObject.focus_unfocus(focusedObjectPointTransform, grabbedObjectPointTransform);
            Debug.Log(grabbableObject);               
        }
        } */
    }

public void GrabActionToggled()
{
    if(grabbableObject == null)
        {
            if(Physics.Raycast(playerCameraTransform.position,playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance))
            {
                Debug.Log(raycastHit.transform);
                if(raycastHit.transform.TryGetComponent(out grabbableObject))
                {
                    NetworkIdentity objectID = grabbableObject.objectID;  
                    CmdAssignAuthorityToPlayer(objectID);                  
                    Debug.Log(grabbableObject);
                    grabbableObject.Grab(grabbedObjectPointTransform, playerID);
                }
            }
        }
        else
        {
            grabbableObject.Drop();
            grabbableObject = null;
        }
}

public void FocusActionToggled()
{
    if(grabbableObject != null)
        {           
            grabbableObject.focus_unfocus(focusedObjectPointTransform, grabbedObjectPointTransform);
            Debug.Log(grabbableObject);               
        }
}

[Command]
private void CmdAssignAuthorityToPlayer(NetworkIdentity objectId)
{
    var otherOwner = objectId.connectionToClient;

    if(otherOwner!= null)
    {
        objectId.RemoveClientAuthority();
    }
    objectId.AssignClientAuthority(playerID.connectionToClient);
}

}
