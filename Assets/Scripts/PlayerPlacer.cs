using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerPlacer : MonoBehaviour
{
    [SerializeField] private Transform startPos1;
    [SerializeField] private Transform startPos2;
    [SerializeField] private string previousWorld1;
    [SerializeField] private string previousWorld2;

    private Rigidbody playerRigidBody;
    public static string PreviousLevel{get;private set;}
    private GameObject player;


    private void Awake()
    {
        
        if(PreviousLevel != null)
        {
            Debug.Log(PreviousLevel);
            player = GameObject.FindWithTag("Player");
            if(player == null)
            {
                Debug.Log("on a pas trouve de player");
            }
            if(PreviousLevel == previousWorld1)
            {
                player.transform.position = startPos1.position;
            }
            if(PreviousLevel == previousWorld2)
            {
                player.transform.position = startPos2.position;
            }       
        }
        
    }

    private void OnDestroy()
    {
        PreviousLevel = gameObject.scene.name;
    }

}
