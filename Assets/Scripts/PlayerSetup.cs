using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField] Behaviour[] componentsToDisable;
    Camera sceneCamera;

    private void Start()
    {
        if(!isLocalPlayer)
        {
            // si le player n'est pas notre player local, on va venir desactiver la liste des components
            for(int i = 0; i < componentsToDisable.Length;i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            Debug.Log(sceneCamera);
            if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

        }
    }

    private void OnDisable()
    {
        if(sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(true);
            }
    }

    
}
