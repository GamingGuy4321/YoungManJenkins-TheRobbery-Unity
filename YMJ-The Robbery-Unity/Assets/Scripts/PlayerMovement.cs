using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameManager m_gameManager;
    
    Rigidbody2D m_rigidbody;
    public Animator m_animator;

    float m_horizontalMovement;
    float m_verticalMovement;
    public bool m_isMoving = false;
    public float m_moveSpeed;
    static int m_DefaultRunSpeed = 4;

    public int Health = 3;
    
    public GameObject Bullets;
    public int BulletRounds = 6;
    public int Clips = 0;
    public Transform Firepoint;

    public bool isCrouching;

    public Image imgHealth;
    public Image imgBullets;

    public AudioSource source;
    
    public AudioClip fire;
    
    public AudioClip reload;

    public AudioClip death;

    private float loseTimer = 0.0f;
    private float reloadDelay = 0.0f;
    private bool isReloading = false;
    private bool isDead = false;
    

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        isCrouching = false;
        loseTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gameManager.GetIsPaused()) {
            // Look for the Esc keypress and pause the game if Esc is pressed.
            if(Input.GetKeyDown(KeyCode.Escape)) {
                m_gameManager.PauseGame();
            }
         }else {
            // If the game is paused and the Esc key is pressed, unpause the game.
            if(Input.GetKeyDown(KeyCode.Escape)) {
                m_gameManager.UnpauseGame();
            }
        }

        if(Health <= 0){
            loseTimer += Time.deltaTime;

            if(loseTimer <= 0.01f){
               source.PlayOneShot(death); 
            }
            m_animator.SetBool("Dead", true);

            if(loseTimer >= 2.0f){
                m_gameManager.LoseGame();
            }
        }

        ResetAnimDirection();
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) {
                // if movement is detected, mark m_isMoving as true
                m_isMoving = true;
                // Call to the Animator and set the Walk layer weight to 1.
                m_animator.SetLayerWeight(1,1);
                // If the input given is Left or Right...
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                    // Check the value of the Horizontal Movement.  Negative = left, positive = right
                    if(m_horizontalMovement < 0) {
                        // Left movement detected, call to Animator and set the Left bool to true
                        m_animator.SetBool("Left", true);
                    } else {
                        // Right movement detected, call to Animator and set Right bool to true
                        m_animator.SetBool("Right", true);
                    }
                //  If the input given is Up or Down (Forward or Backward)...
                } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) {
                    if (m_verticalMovement < 0) {
                        // Forward movement detected, call to Animator and set the Forward bool to true
                        m_animator.SetBool("Forward", true);
                    } else {
                        // Backward movement detected, call to Animator and set the Backward bool to true
                        m_animator.SetBool("Backward", true);
                    }
                }
            // No movement input detected
            } else {
                // Set m_isMoving to false
                m_isMoving = false;
                // Call to the Animator and set the Walk layer weight back to 0.
                m_animator.SetLayerWeight(1,0);
            }
            // If the m_isMoving bool is true, check for the Left Shift key input
            if (m_isMoving) {
                    m_moveSpeed = m_DefaultRunSpeed;
                
            }

            if(Input.GetKeyDown(KeyCode.Mouse0) && !isCrouching){
                if(BulletRounds >= 1){
                    m_animator.SetBool("Shoot", true);
                    Debug.Log("Shoot");
                    source.PlayOneShot(fire);
                    Instantiate(Bullets, Firepoint.position, Firepoint.rotation);
                    BulletRounds -= 1;
                    imgBullets.fillAmount -= 1.0f/6;
                }
            }

            if(Input.GetKey(KeyCode.R)){
                if(Clips >= 1){
                    if(reloadDelay == 0){
                        isReloading = true;
                        source.PlayOneShot(reload);
                    }
                    Debug.Log("Reload");


                }
            }
            if(isReloading){
                reloadDelay += Time.deltaTime;
                Debug.Log("Waiting");
                    
                if(reloadDelay >= 3.0f ){
                    Debug.Log("Reloaded");
                    BulletRounds = 6;
                    Clips -=1;
                    imgBullets.fillAmount = 1.0f;
                    reloadDelay = 0.0f;
                    isReloading = false;
                }
            }
                    

            if(Input.GetKeyDown(KeyCode.LeftControl)){
                
                isCrouching = !isCrouching;
            }
            if(isCrouching){
                m_animator.SetBool("Crouching", true);
                //Debug.Log("Crouched");
                m_moveSpeed = 0;   
                m_isMoving = false;
            }
            if(!isCrouching){
                m_animator.SetBool("Crouching", false);
                //Debug.Log("Stopped Crouching");
                m_moveSpeed = m_DefaultRunSpeed;
            }
    }

    void FixedUpdate() {
        // Every frame, check for input from the "Horizontal" and "Vertical" inputs and assign them to the values accordingly
        m_horizontalMovement = Input.GetAxisRaw("Horizontal");
        m_verticalMovement = Input.GetAxisRaw("Vertical");
        // Add velocity to the Rigidbody component using the input values, adding the relative move speed we've assigned earlier
        m_rigidbody.velocity = new Vector2(m_horizontalMovement * m_moveSpeed, m_verticalMovement * m_moveSpeed);
    }

    void ResetAnimDirection() {
        m_animator.SetBool("Left", false);
        m_animator.SetBool("Right", false);
        m_animator.SetBool("Forward", false);
        m_animator.SetBool("Backward", false);
        m_animator.SetBool("Shoot", false);
        //m_animator.SetBool("Crouching", false);
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "AmmoBox"){
            Debug.Log("Picked Up Ammo");
            Clips +=1;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "ShotgunEnemy" && !isCrouching){
            Debug.Log("You've been shot");
            imgHealth.fillAmount -= 1.0f/3;
            Health -=1;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "PistolEnemy" && !isCrouching){
            Debug.Log("You've been shot");
            imgHealth.fillAmount -= 1.0f/3;
            Health -=1;
            Destroy(other.gameObject);
        }
    }
}
