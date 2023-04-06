using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    
    private GameObject m_pauseMenu;
    private GameObject m_LoseMenu;
    private GameObject m_WinMenu;

    private bool m_isLost;
    private bool m_isWon;
    private bool m_isPaused;

    public bool GetIsLost(){return m_isLost;}
    public void SetIsLost(bool value){m_isLost = value;}

    public bool GetIsWon(){return m_isWon;}
    public void SetIsWon(bool value){m_isWon = value;}

    public bool GetIsPaused(){return m_isPaused;}
    public void SetIsPaused(bool value){m_isPaused = value;}

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        m_pauseMenu = GameObject.Find("PauseMenuCanvas");
        m_LoseMenu = GameObject.Find("LoseCanvas");
        m_WinMenu = GameObject.Find("WinCanvas");
        m_pauseMenu.SetActive(false);
        m_LoseMenu.SetActive(false);
        m_WinMenu.SetActive(false);
    }

    void SetInstance(){
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    // Function to pause the game and activate the PauseMenu object
    public void PauseGame() {
        if (!m_isPaused) {
            Time.timeScale = 0;
            m_isPaused = true;
            m_pauseMenu.SetActive(true);
        }
    }

        // Function to unpause the game and deactivate the PauseMenu object
    public void UnpauseGame() {
        if(m_isPaused) {
            Time.timeScale = 1;
            m_isPaused = false;
            m_pauseMenu.SetActive(false);
        }
    }

    // Function to pause the game and activate the PauseMenu object
    public void LoseGame() {
        if (!m_isLost) {
            Time.timeScale = 0;
            m_isLost = true;   
            m_LoseMenu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
