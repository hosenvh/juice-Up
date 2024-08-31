using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreoidGenerator : MonoBehaviour
{
    public GameObject Streoid;
    public Transform Up, Down;
    private void Start()
    {
       // transform.LookAt(Target);
    }
    public Transform Target;


    public float nextDuration,lastDuration;

    private void Update()
    {
        if(Time.time-lastDuration > nextDuration)
        {
            GenerateStreoid();
            lastDuration=Time.time;
            nextDuration = Random.Range(0.1f, 1.5f);
        }
    }
    public void GenerateStreoid()
    {
        var rand = Random.Range(0.1f, 0.99f);
        var midWay = (Up.position - Down.position) * rand;
        var generationPoint = Down.position + midWay;
        var temp=Instantiate(Streoid);
        var StreoidScalerand = Random.Range(1.1f, 2.2f);
        temp.transform.localScale = StreoidScalerand * Vector3.one;
        temp.transform.position = generationPoint;
        temp.transform.rotation = transform.rotation;
    }
}
