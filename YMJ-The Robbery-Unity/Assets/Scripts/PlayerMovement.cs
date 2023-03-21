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


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
                    if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) {
                // if movement is detected, mark m_isMoving as true
                m_isMoving = true;
                // Call to the Animator and set the Walk layer weight to 1.
                //m_animator.SetLayerWeight(1,1);
                // If the input given is Left or Right...
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
                    // Check the value of the Horizontal Movement.  Negative = left, positive = right
                    if(m_horizontalMovement < 0) {
                        // Left movement detected, call to Animator and set the Left bool to true
                        //m_animator.SetBool("Left", true);
                    } else {
                        // Right movement detected, call to Animator and set Right bool to true
                        //m_animator.SetBool("Right", true);
                    }
                //  If the input given is Up or Down (Forward or Backward)...
                } else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) {
                    if (m_verticalMovement < 0) {
                        // Forward movement detected, call to Animator and set the Forward bool to true
                       // m_animator.SetBool("Forward", true);
                    } else {
                        // Backward movement detected, call to Animator and set the Backward bool to true
                        //m_animator.SetBool("Backward", true);
                    }
                }
            // No movement input detected
            } else {
                // Set m_isMoving to false
                m_isMoving = false;
                // Call to the Animator and set the Walk layer weight back to 0.
                //m_animator.SetLayerWeight(1,0);
                // Call to the Animator and set the Run layer weight back to 0.
                //m_animator.SetLayerWeight(2,0);
            }
            // If the m_isMoving bool is true, check for the Left Shift key input
            if (m_isMoving) {
                // Left Shift input detected
                if (Input.GetKey(KeyCode.LeftShift)) {
                    // Call to the Animator and set the Run layer weight to 1
                    //m_animator.SetLayerWeight(2,1);
                    // Double the m_moveSpeed to make the character run
                    //m_moveSpeed = m_DefaultRunSpeed * 2;
                // Left shift input NOT detected
                } else {
                    // Call to the Animator and set the run layer weight back to 0
                   // m_animator.SetLayerWeight(2,0);
                    // Reset the m_moveSpeed so the character walks
                    m_moveSpeed = m_DefaultRunSpeed;
                }
            }
    }

    void FixedUpdate() {
        // Every frame, check for input from the "Horizontal" and "Vertical" inputs and assign them to the values accordingly
        m_horizontalMovement = Input.GetAxisRaw("Horizontal");
        m_verticalMovement = Input.GetAxisRaw("Vertical");
        // Add velocity to the Rigidbody component using the input values, adding the relative move speed we've assigned earlier
        m_rigidbody.velocity = new Vector2(m_horizontalMovement * m_moveSpeed, m_verticalMovement * m_moveSpeed);
    }
}
