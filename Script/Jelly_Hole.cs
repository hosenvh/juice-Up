using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly_Hole : MonoBehaviour
{
    public Animator hole;

    public void OpenHole()
    {
        hole.SetTrigger("trap");
        
    }

}
