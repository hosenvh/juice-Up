using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtmosphereCore : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Time.timeScale = 1;
           // other.gameObject.SetActive(false);
            SceneManager.LoadScene("pandoli");
        }
    }

}
