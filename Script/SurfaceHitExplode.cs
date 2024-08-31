using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceHitExplode : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem explode;
    public List<ParticleSystem> Particles;
    AudioSource audio;
    public AudioClip littleExplode;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        for(int i = 0; i < 10; i++)
        {
            var temp=Instantiate(explode);
            //temp.transform.parent = transform;
            temp.gameObject.SetActive(false);
            Particles.Add(temp);    
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("moving"))
        {
            print("explode");
            audio.PlayOneShot(littleExplode);
            Explode(other.transform);
        }
    }

    IEnumerator TurnOffParticle(GameObject obj,GameObject movingStreoid)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
        Destroy(movingStreoid);
    }

    public void Explode(Transform hitPoint)
    {
        foreach(ParticleSystem particle in Particles)
        {
            if(!particle.gameObject.activeInHierarchy)
            {
                particle.transform.position = hitPoint.position; 
                particle.gameObject.SetActive(true);
                particle.transform.rotation = Quaternion.LookRotation((hitPoint.position - transform.position).normalized);
                StartCoroutine(TurnOffParticle(particle.gameObject,hitPoint.gameObject));
                return;
            }
        }
    }


}
