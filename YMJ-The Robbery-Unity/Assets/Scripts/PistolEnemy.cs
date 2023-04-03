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
    float shootDelay = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Jenkins").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = (Player.position - transform.position)*1;
        FireBullet();
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
            Debug.Log("You've killed them");
            Destroy(this);
        }
    }
}
