using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : MonoBehaviour
{
    Transform Player;
    public Transform FirePoint;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Jenkins").transform;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = (Player.position - transform.position)*-1;
    }
}
