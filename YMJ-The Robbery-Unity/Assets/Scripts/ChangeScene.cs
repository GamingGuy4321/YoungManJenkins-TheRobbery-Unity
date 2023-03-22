using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void LoadScene(string Game)
    {
        SceneManager.LoadScene(Game);
    }

    public void ExitGame() {
        Debug.Log("Exit Button Clicked");
        Application.Quit();
    }
}
