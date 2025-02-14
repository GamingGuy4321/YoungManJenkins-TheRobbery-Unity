using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : MonoBehaviour
{
    Transform Player;
    public Transform FirePoint;
    public GameObject Bullets;
    public float shootDelay = 0.0f;
    public float cockShotgunDelay = 0.0f;
    public float deathDelay = 0.0f;
    public AudioSource source;
    public AudioClip cockShotgun;
    public AudioClip fire;
    public AudioClip death;

    bool hasPlayedSoundEffect = false;
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
        cockShotgunDelay += Time.deltaTime;
        shootDelay += Time.deltaTime;

        if(cockShotgunDelay >= 2.0f && !hasPlayedSoundEffect){
            hasPlayedSoundEffect = true;
            source.PlayOneShot(cockShotgun);
        }
        if(shootDelay >= 4.0f ){
            Instantiate(Bullets, FirePoint.position, FirePoint.rotation);
            shootDelay = 0.0f;
            cockShotgunDelay = 0.0f;
            source.PlayOneShot(fire);
            hasPlayedSoundEffect = false;
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
