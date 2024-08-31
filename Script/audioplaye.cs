using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audioplaye : SingleTon<audioplaye>
{
    // Start is called before the first frame update
    public AudioClip Woosh;
    public AudioSource t1, t2,t3,t4, ex;

    public void StopAll()
    {
        t1.volume = 0;
        t2.volume = 0;
        t3.volume = 0;
        t4.volume = 0;
        ex.volume = 0;
    }
    public void PlayMusic(int val)
    {
        print("music");
        switch (val)
        {
            case  1:
                PlayT1();
                break;
            case 2:
                Playt2();
                break;
            case 3:
                Playt3();
                break;
            case 4:
                Playt4();
                break;
            case 5:
                Playex();
                break;


        }
    }



    private void PlayT1()
    {
        StartCoroutine(ChangeVolume(t1, 0, 1));
        StartCoroutine(ChangeVolume(t2, 1, 0));
        StartCoroutine(ChangeVolume(t3, 1, 0));
        StartCoroutine(ChangeVolume(t4, 1, 0));       
        StartCoroutine(ChangeVolume(ex, 1, 0));
    }

    private void Playt2()
    {
        StartCoroutine(ChangeVolume(t1, 1, 0));
        StartCoroutine(ChangeVolume(t2, 0, 1));
        StartCoroutine(ChangeVolume(t3, 1, 0));
        StartCoroutine(ChangeVolume(t4, 1, 0));
        StartCoroutine(ChangeVolume(ex, 1, 0));
    }
    private void Playt3()
    {
        StartCoroutine(ChangeVolume(t1, 1, 0));
        StartCoroutine(ChangeVolume(t2, 1, 0));
        StartCoroutine(ChangeVolume(t3, 0, 1));
        StartCoroutine(ChangeVolume(t4, 1, 0));
        StartCoroutine(ChangeVolume(ex, 1, 0));
    }

    private void Playt4()
    {
        StartCoroutine(ChangeVolume(t1, 1, 0));
        StartCoroutine(ChangeVolume(t2, 1, 0));
        StartCoroutine(ChangeVolume(t3, 1, 0));
        StartCoroutine(ChangeVolume(t4, 0, 1));
        StartCoroutine(ChangeVolume(ex, 1, 0));
    }


    private void Playex()
    {
        StartCoroutine(ChangeVolume(t1, 1, 0));
        StartCoroutine(ChangeVolume(t2, 1, 0));
        StartCoroutine(ChangeVolume(t3, 1, 0));
        StartCoroutine(ChangeVolume(t4, 1, 0));
        StartCoroutine(ChangeVolume(ex, 0, 1));
    }


    IEnumerator ChangeVolume(AudioSource src,float from,float to)
    {
        var currentScene=SceneManager.GetActiveScene();
       
        if(to==1)
        {
            if(src.isPlaying)
            {
                if(currentScene.name=="pandoli")
                {
                    src.volume = to;
                }
                yield break;
            }
            src.Play();
            src.volume = from;
        }
        float start=Time.time;
        while(Time.time-start<1.5f)
        {
            var lerp=Mathf.Lerp(from,to,(Time.time-start)/2);
            src.volume=lerp;
            yield return null;
        }
        if(to==0)
        {
            src.Stop();
        }
    }



    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

   
}
