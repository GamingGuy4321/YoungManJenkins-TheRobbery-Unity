using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    // Reference to the PauseScreen game object
    [SerializeField] private GameObject m_loseScreen;

    // Variables
    [HideInInspector]
    public bool m_loseScreenIsActive = false;


    void Start(){
        m_loseScreen.SetActive(false);
    }

    public void SwitchToLoseScreen() {
        m_loseScreen.SetActive(true);
        m_loseScreenIsActive = true;
    }

    public void StopLoseScreen(){
        m_loseScreen.SetActive(false);
        m_loseScreenIsActive = false;
    }
}
