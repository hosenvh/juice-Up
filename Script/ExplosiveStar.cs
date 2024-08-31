using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveStar : MonoBehaviour
{
    bool hit;
    private void OnTriggerEnter(Collider other)
    {
        if (hit) return;
        hit = true;
        if(other.CompareTag("Player"))
        {
            Explode(other.GetComponent<Rigidbody>());
        }
    }
    public int ExtraForce;
    public ParticleSystem shield, explosion;
    void Explode(Rigidbody rb)
    {
        shield.gameObject.SetActive(false); 
        explosion.gameObject.SetActive(true);
        rb.AddForce(((rb.transform.position-transform.position).normalized*transform.localScale.x*(200+ExtraForce)));
        LeanTween.scale(gameObject, Vector3.zero, 1.5F).setEaseOutQuad().setOnComplete(() =>
          {
              Destroy(gameObject);
          });
    }

    

}
