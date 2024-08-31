using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public int team;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("dice"))
        {
            jelly_dice dice = other.GetComponent<jelly_dice>();
           // print("ok");
            if(dice.team==team)
            {
                if(!dice.counted)
                {
                    
                }
               
            }
        }
    }
}
