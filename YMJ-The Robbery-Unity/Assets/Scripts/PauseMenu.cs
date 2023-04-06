using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
        // Reference to the PauseScreen game object
    public GameObject m_pauseScreen;
    // Variables
    [HideInInspector]
    public bool m_pauseScreenIsActive = false;

    void Start(){
        m_pauseScreen.SetActive(false);
    }

    public void SwitchToPauseScreen() {
        m_pauseScreen.SetActive(true);
        m_pauseScreenIsActive = true;
    }
}
