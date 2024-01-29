using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Mirror;

public class GrabbableObject : NetworkBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform grabbedObjectPointTransform;
    bool isFocused = false;
    Vector3 angleVelocity;
    public NetworkIdentity objectID;
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectID =  GetComponent<NetworkIdentity>();
        angleVelocity = new Vector3(0,100,0);
    }

    public void Grab(Transform input, NetworkIdentity playerID)
    {
        Debug.Log("J'arrive bien dans le grab"); 
        Debug.Log(input); 
        //CmdSetAuth(objectID, playerID);
        //CmdPickupItem(objectID);
        grabbedObjectPointTransform = input;
        objectRigidBody.useGravity = false;
        objectRigidBody.freezeRotation = true;
    }
    

    public void Drop()
    {
        grabbedObjectPointTransform = null;
        objectRigidBody.freezeRotation = false;
        objectRigidBody.useGravity = true;

    }

    public void focus_unfocus(Transform focusedPoint, Transform unfocusedPoint)
    {
        if(isFocused == false)
        {
            grabbedObjectPointTransform = focusedPoint;
            objectRigidBody.useGravity = false;
            isFocused = true;
        }
        else
        {
            grabbedObjectPointTransform = unfocusedPoint;
            objectRigidBody.useGravity = false;
            isFocused = false;
        }
        
    }

    private void FixedUpdate()
    {
        if (grabbedObjectPointTransform!=null)       
        {
            if(isFocused)
            {            
                Quaternion deltaRotation = Quaternion.Euler(angleVelocity*Time.deltaTime);
                objectRigidBody.MoveRotation(objectRigidBody.rotation*deltaRotation);
            }
            float lerpspeed = 20f;
            Vector3 newPos = Vector3.Lerp(transform.position, grabbedObjectPointTransform.position, Time.deltaTime*lerpspeed);
            objectRigidBody.MovePosition(newPos);
        }
        
    }

//[Command]
    //void CmdPickupItem(NetworkIdentity id)
    //{
        //id.AssignClientAuthority(connectionToClient);
    //}
//[Command]
 /*    public void CmdSetAuth(NetworkIdentity objectId, NetworkIdentity player)
    {
        var networkIdentity = iObject.GetComponent<NetworkIdentity>();
        var otherOwner = networkIdentity.clientAuthorityOwner;      
 
        if (otherOwner == player.connectionToClient)
        {
            return;
        }else
        {
            if (otherOwner != null)
            {
                networkIdentity.RemoveClientAuthority(otherOwner);
            }
            networkIdentity.AssignClientAuthority(player.connectionToClient);
        }      
    } */

}
