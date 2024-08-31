using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> : MonoBehaviour where T : Component
{
    public static T Instance {get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance=this as T; 
        }else
        {
            Destroy(this);
        }
    }


}
