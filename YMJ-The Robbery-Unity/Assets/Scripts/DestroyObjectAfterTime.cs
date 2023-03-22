using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectAfterTime : MonoBehaviour
{
    public double Timer;
    public double internalTimer;

    void OnEnable() {
        internalTimer = Timer;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        internalTimer -= Time.deltaTime;
        if (internalTimer <= 0){
            Destroy(gameObject);
        }
    }
}
