using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : SingleTon<Board>
{
    public List<jelly_cell> row1 = new List<jelly_cell>();
    public List<jelly_cell> row2 = new List<jelly_cell>();
    public List<jelly_cell> row3 = new List<jelly_cell>();
    public List<jelly_cell> row4 = new List<jelly_cell>();
    public List<jelly_cell> row5 = new List<jelly_cell>();
    public List<jelly_cell> row6 = new List<jelly_cell>();
    public List<List<jelly_cell>> allrows = new List<List<jelly_cell>>();
    public GameObject Glue,Fanar;
    public Jelly_Hole hole;     
    public GameObject highlight;
    public ParticleSystem touchdown;
    public List<GameObject> numbers;
    List<ParticleSystem> touchDownPool = new List<ParticleSystem>();
    public void PlayTouchDown(Vector3 pos,int team)
    {
        
        if (team==1)
        {
            StadiumSound.Instance.PlayHappy();
           // StadiumSound.Instance.PlaySwoosh();
            StadiumSound.Instance.PlayTouchDown();
            //StadiumSound.Instance.PlayTouchDown();
        }
        else
        {
            StadiumSound.Instance.PlaySwoosh();
            StadiumSound.Instance.PlayBoo();
        }
        print(pos);
        foreach(var touchDown in touchDownPool)
        {
            if(!touchDown.isPlaying)
            {
                
                touchDown.transform.position = pos;
                StartCoroutine(PlayParticleByDelay(touchDown)); 
                break;
            }
        }
    }

    IEnumerator PlayParticleByDelay(ParticleSystem particle)
    {
        yield return new WaitForSeconds(0);
        particle.gameObject.SetActive(true);
        particle.Play();
    }
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var temp = Instantiate(touchdown);
            touchDownPool.Add(temp);
        }
        allrows.Clear();
        allrows.Add(row1);
        allrows.Add(row2);
        allrows.Add(row3);
        allrows.Add(row4);
        allrows.Add(row5);
        allrows.Add(row6);

    }
}
