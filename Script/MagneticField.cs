using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public Transform center;
    public float RotationSpeed;
    public Transform RotationalObject;
    public float Camsize, Force;

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
             other.GetComponent<Rigidbody>().AddForce(center.forward*Force,ForceMode.Force);
            if(Camsize>0)
            {
                StartCoroutine(changeCamsize(Camsize));
            }
        }
    }*/
    /*IEnumerator changeCamsize(float to)
    /*{
        float start = Time.time;
        //Cinemachine.LensSettings lensSettings = PandolGameManager.Instance.cam.m_Lens;
       // var from = lensSettings.OrthographicSize;
        while (Time.time - start < 2)
        {

           // var lerp = Mathf.Lerp(from, to, (Time.time - start) / 2);

          //  lensSettings.OrthographicSize = lerp;
           // PandolGameManager.Instance.cam.m_Lens = lensSettings;
          //  yield return null;
        }
    }#1#*/
}
