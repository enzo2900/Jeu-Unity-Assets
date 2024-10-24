using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    string sceneName;

    [SerializeField]
    Vector2 newPositionOfPlayer;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<CharacterControllerNew>(out CharacterControllerNew c))
        {
            c.dreamManager.RemoveAll();
            OnEnter(collision.gameObject);
        }
    }
    void OnEnter(GameObject entered)
    {
        DontDestroyOnLoad(entered);
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetActiveScene());
        player.transform.position = newPositionOfPlayer;

       
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
