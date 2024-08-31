using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumSound : SingleTon<StadiumSound>
{
    public AudioSource idle, Exo, TouchDown,Bonus;
    public List<AudioClip> idleClips, happyClips, touchDownClips, boo;
    public AudioClip swoosh,bonus,wow,fanar,magic,mechanical,hit;
    private void Start()
    {
        lastPlayIdle = -1;
        Exo.volume = 0;
        idle.volume = 0;
        TouchDown.volume = 0; 
       PlayIdle();
    }

    public void PlayMagic()
    {
        Bonus.PlayOneShot(magic);
        StartCoroutine(PlayWowDelay());
    }

    public void PlayMechanical()
    {
        Bonus.PlayOneShot(mechanical);
        StartCoroutine(PlayWowDelay());
    }

    public void PlayFanar()
    {
        Bonus.PlayOneShot(fanar);
        StartCoroutine(PlayWowDelay());
    }

    IEnumerator PlayWowDelay()
    {
        
        yield return new WaitForSeconds(0.6f);
        PlayWow();
    }


    public void PlayWow()
    {
        Bonus.PlayOneShot(wow);
    }

    public void PlaySwoosh()
    {
        TouchDown.volume = 1;
        TouchDown.PlayOneShot(swoosh);
    }

    public void PlayBonus()
    {
       Bonus.volume = 1;    
        Bonus.PlayOneShot(bonus);
    }

    public void PlayIdle()
    {
        transitionTime = 5;
        //StartCoroutine(co_changeVolume(Exo, Exo.volume, 0));
        StartCoroutine(co_changeVolume(idle, 0, MaxIdle));
       // StartCoroutine(co_PlayIdle(idle));  

    }
    public void PlayBoo()
    {
        if (Exo.isPlaying)
        {
            return;
        }

        var happyindex = Random.Range(0, boo.Count - 1);
        Exo.volume = 1;
        Exo.clip = boo[happyindex];
        Exo.Play();
    }

    
    public void PlayTouchDown()
    {
        var index=Random.Range(0,touchDownClips.Count-1);
        TouchDown.volume = 0.4f;
        TouchDown.PlayOneShot(touchDownClips[index]);
    }

    public void PlayHitEnemy()
    {
        Bonus.PlayOneShot(hit);
    }
    public void PlayHappy()
    {
        if(Exo.isPlaying)
        {
            return;
        }

        var happyindex = Random.Range(0, happyClips.Count - 1);
        Exo.volume = 0.5f;
        Exo.clip=happyClips[happyindex];
        Exo.Play();
        StartCoroutine(co_changeVolume(Exo, 0,1,0.5f));
        print("happy");

    }
    
    



    int lastPlayIdle;

    public float MaxIdle;
    float transitionTime;
    IEnumerator co_PlayIdle(AudioSource source)
    {
        while(source.isPlaying)
        {
            yield return null;  
        }

        int index=Random.Range(0, idleClips.Count-1);
        while(index==lastPlayIdle)
        {
            index = Random.Range(0, idleClips.Count - 1);
            yield return null;
        }
        var wait=Random.Range(2, 5);
        yield return new WaitForSeconds(2);
        StartCoroutine(co_changeVolume(idle, 0, MaxIdle));    
        lastPlayIdle=index;
        source.clip=(idleClips[index]);
        source.Play();
        StartCoroutine(co_PlayIdle(source));


    }
    IEnumerator co_changeVolume(AudioSource sorce,float from,float to,float time=2)
    {
        sorce.Play();
        sorce.volume = from;    
        float start=Time.time;
        while(Time.time-start<time)
        {
            var val=Mathf.Lerp(from,to,(Time.time-start)/time);
            sorce.volume = val; 
            yield return null;
        }
    }



}
