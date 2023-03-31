using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D m_rigidbody;
    public Animator m_animator;

    float m_horizontalMovement;
    float m_verticalMovement;
    public bool m_isMoving = false;
    public float m_moveSpeed;
    static int m_DefaultRunSpeed = 5;

    public int Health = 3;
    
    public GameObject Bullets;
    public int BulletRounds = 6;
    public int Clips = 0;
    public Transform Firepoint;

    public bool isCrouching;
    public Collider2D jenkinsCollider;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        isCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
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

            if(Input.GetKeyDown(KeyCode.Mouse0)){
                if(BulletRounds >= 1){
                    m_animator.SetBool("Shoot", true);
                    Instantiate(Bullets, Firepoint.position, Firepoint.rotation);
                    BulletRounds -= 1;
                }
            }

            if(Input.GetKey(KeyCode.R)){
                if(Clips >= 1){
                    BulletRounds = 6;
                    Clips -=1;
                }
            }

            if(Input.GetKeyDown(KeyCode.LeftControl)){
                
                isCrouching = !isCrouching;
            }
            if(isCrouching){
                m_animator.SetBool("Crouching", true);
                m_moveSpeed = 0;   
                m_isMoving = false;
                jenkinsCollider.enabled = false; 
            }
            if(!isCrouching){
                m_animator.SetBool("Crouching", false);
                m_moveSpeed = m_DefaultRunSpeed;
                jenkinsCollider.enabled = true; 
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

    void OnCollisionEnter2D(Collision2D other) {

        if(other.gameObject.tag == "AmmoBox"){
              Debug.Log("Picked Up Ammo");
            Clips +=1;
            Destroy(other.gameObject);
        }
    }
}
