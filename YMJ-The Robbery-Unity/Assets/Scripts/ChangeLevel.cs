using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        string sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lvl-1")){
            if (other.gameObject.tag == "Player"){
                SceneManager.LoadScene ("Lvl-2");
            }
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lvl-2")){
            if (other.gameObject.tag == "Player"){
                SceneManager.LoadScene ("Lvl-3");
            }
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lvl-3")){
            if (other.gameObject.tag == "Player"){
                SceneManager.LoadScene ("Lvl-4");
            }
        }
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lvl-4")){
            if (other.gameObject.tag == "Player"){
                SceneManager.LoadScene ("WinScreen");
            }
        }
    }
}
