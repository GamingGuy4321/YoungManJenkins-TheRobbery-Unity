using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeImage : MonoBehaviour
{
    public Image imgbackground;
    public float fadeTimer = 20;
    public float fadeAmount = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fadeTimer -= Time.deltaTime;

        imgbackground = GetComponent<Image>();
        var tempColor = imgbackground.color;
        tempColor.a -= fadeAmount;
        imgbackground.color = tempColor;

        if(fadeTimer <= 0){
            SceneManager.LoadScene ("Menu");
        }
        
    }
}
