using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance = null;
    
    [SerializeField]
    private GameObject m_pauseMenu;
    [SerializeField]
    private GameObject m_LoseMenu;

    private bool m_isLost;
    private bool m_isPaused;

    public bool GetIsLost(){return m_isLost;}
    public void SetIsLost(bool value){m_isLost = value;}

    public bool GetIsPaused(){return m_isPaused;}
    public void SetIsPaused(bool value){m_isPaused = value;}

    void Awake() {
        // On Awake, create the instance of the GameManager that will be used for the duration of the program
        SetInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        m_pauseMenu = GameObject.Find("PauseMenuCanvas");
        m_LoseMenu = GameObject.Find("LoseCanvas");
        m_pauseMenu.SetActive(false);
        m_LoseMenu.SetActive(false);
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
