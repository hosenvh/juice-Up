using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly_end : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       // print("enter");
        if(other.gameObject.name.Contains("dice"))
        {
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<jelly_dice>().move = false;
            other.GetComponent<jelly_dice>().stopSlide();
           // Color clr = other.GetComponent<jelly_dice>().hat.GetComponent<SpriteRenderer>().color;
           // clr.a =0.3f;
           // other.GetComponent<jelly_dice>().hat.GetComponent<SpriteRenderer>().color = clr;

        }
    }
}
