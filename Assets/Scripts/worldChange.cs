using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;


public class worldChange : NetworkBehaviour
{
    private NetworkManager networkManager;
    public string worldName;
    private GameObject playerObject;

    [SerializeField] GameObject playerPrefab;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
        playerObject = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter(Collider other) 
    {
        /* if (other.CompareTag("Player"))
        {           
            if(playerObject!=null)
            {
                Debug.Log("on a trouve le playerobject");
                SceneManager.MoveGameObjectToScene(playerObject, SceneManager.GetSceneByName(worldName));
            }
            SceneManager.LoadScene(worldName);
        } */
        
       /*  if(other.CompareTag("Player"))
        {
            Debug.Log("je suis rentree dans la boucle pour load mon monde");
            StartCoroutine(LoadYourAsyncScene());
        } */

        /* if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene(worldName);
            GameObject playerInstance = Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity);
            NetworkServer.Spawn(playerInstance);
        } */

        if(other.CompareTag("Player"))
        {
            networkManager.ServerChangeScene(worldName);
        }
    }
        


    IEnumerator LoadYourAsyncScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(worldName, LoadSceneMode.Additive);
        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log(worldName);
        Debug.Log("loaded");
        SceneManager.MoveGameObjectToScene(playerObject, SceneManager.GetSceneByName(worldName));

        SceneManager.UnloadSceneAsync(currentScene);
        Debug.Log(currentScene);
        Debug.Log("unloaded");
    }

    private void SpawnPlayer()
    {
        GameObject playerPrefab = networkManager.spawnPrefabs[0];
        NetworkServer.Spawn(Instantiate(playerPrefab));
    }
}
