using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : MonoBehaviour
{
    Transform Player;
    public Transform FirePoint;
    public GameObject Bullets;
    public AudioSource source;
    public AudioClip fire;
    public AudioClip death;
    public float shootDelay = 0.0f;
    public float deathDelay = 0.0f;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Jenkins").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = (Player.position - transform.position)*1;
        if(!isDead){
            FireBullet();
        }
        if(isDead){
            deathDelay += Time.deltaTime;
        }
        if(deathDelay >= 2.5f){
                Destroy(gameObject);
        }
    }

    void FireBullet(){

        shootDelay += Time.deltaTime;

        if(shootDelay >= 2.0f){
            source.PlayOneShot(fire);
            Instantiate(Bullets, FirePoint.position, FirePoint.rotation);
            shootDelay = 0.0f;
        }   
    }

    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Bullet"){
            isDead = true;
            source.PlayOneShot(death);
            Debug.Log("You've killed them");
            Destroy(other.gameObject);
        }
    }
}
