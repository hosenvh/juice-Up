using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStreoid : MonoBehaviour
{
    public float speed;
    float start;
    private void Start()
    {
        GetComponent<AudioSource>().volume=transform.localScale.y/2;
    }
    // Update is called once per frame
    void Update()
    {
        if(start == 0)
        {
            start = Time.time;
        }
        if(Time.time - start > 12)
        {
            Destroy(gameObject);
        }
       transform.Translate(transform.forward*Time.deltaTime*speed); 
    }
}
