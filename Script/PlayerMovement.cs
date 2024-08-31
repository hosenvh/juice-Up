using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float Speed;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(transform.forward*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(-transform.forward*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(transform.right*Time.deltaTime*Speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-transform.right*Time.deltaTime*Speed);
        }
    }
}
