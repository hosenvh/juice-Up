using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jelly_dicemaker : SingleTon<jelly_dicemaker>
{
    public GameObject dice;
    public static event Action SystemTick;
    public Transform PlayerStartPos, EnemyStartPos;

    public Material Player, Opp;
    //called by invoke repeat
    public List<jelly_dice> PlayerADices, PlayerBdices;
    float diceMakeSpeed, SlideSpeed;
    private int maxDice;
    private void Start()
    {
        maxDice = GameInfo.Instance.juiceLevelScobjt.levelInt * 6;
        switch(GameInfo.Instance.OpponentLevel)
        {
            case 0:
                diceMakeSpeed = 1.8f;
                SlideSpeed = 1;
                break;
            case 1:
                diceMakeSpeed = 1.7f;
                SlideSpeed = 1;
                break;
            case 2:
                diceMakeSpeed = 1.4f;
                SlideSpeed = 1.4f;
                break;
            case 3:
                diceMakeSpeed = 1.2f;
                SlideSpeed = 1.8f;
                break;
            case 4:
                diceMakeSpeed = 1f;
                SlideSpeed = 2;
                break;
        }
        PlayerADices = new List<jelly_dice>();  
        PlayerBdices = new List<jelly_dice>();
        InvokeRepeating("makedice", 1, diceMakeSpeed);
        
    }
    void makedice()
    {
        var diceVal = UnityEngine.Random.Range(1, maxDice);
        
        for (int i = 0; i < 2; i++)
        {
            var dc = Instantiate(dice);
            var jellydice=dc.GetComponent<jelly_dice>();
            jellydice.Speed = SlideSpeed;
            dc.GetComponent<jelly_dice>().team = i + 1;
            if(i+1==1)
            {
                foreach (var VARIABLE in jellydice.numbers)
                {
                    VARIABLE.material = Player;
                }
                PlayerADices.Add(jellydice);
                if(P1TrapJump)
                {
                    jellydice.TrapWatch = true;
                }
                jellydice.Couter.Rotate(new Vector3(0, 180, 0));
            }
            else
            {
                foreach (var VARIABLE in jellydice.numbers)
                {
                    VARIABLE.material = Opp;
                }
                PlayerBdices.Add(jellydice);
                if (P2TrapJump)
                {
                    jellydice.TrapWatch = true;
                }
                

            }
            jellydice.SetValue(diceVal);
            jumpdown(dc);
           
            
        }
        
        SystemTick();
    }

    public Transform jumpLanding,t2,enemyJumpland,enemyt2;
    public bool P1TrapJump, P2TrapJump;

    Coroutine PlayerA_Jump_co, PlayerB_Jump_co;

    public void TrapJumpActivator(int val)
    {
        if(val==1)
        {
            if(PlayerA_Jump_co!=null)
            {
                StopCoroutine(PlayerA_Jump_co);
            }
            PlayerA_Jump_co = StartCoroutine(Player_A_Trap_Jump_Enabler());
        }else if(val==2)
        {
            if(PlayerB_Jump_co != null)
            {
                StopCoroutine (PlayerB_Jump_co);
            }
            PlayerB_Jump_co=StartCoroutine(Player_B_Trap_Jump_Enabler());
        }
    }




    IEnumerator Player_A_Trap_Jump_Enabler()
    {
        P1TrapJump = true;
        yield return new WaitForSeconds(25);
        P1TrapJump = false;
    }
    IEnumerator Player_B_Trap_Jump_Enabler()
    {
        P2TrapJump = true;
        yield return new WaitForSeconds(25);
        P2TrapJump = false;
    }


    void jumpdown(GameObject dc)
    {
        if(dc.GetComponent<jelly_dice>().team==1)
        {
            
            dc.transform.position = PlayerStartPos.position;
            dc.transform.LookAt(EnemyStartPos.position);    
            Vector3 jumpCurvePoint = new Vector3(jumpLanding.position.x, dc.transform.position.y + 0.3f, jumpLanding.position.z);
            Vector3[] rt = new Vector3[5];
            rt[0] = dc.transform.position;
            rt[1] = dc.transform.position;
            rt[2] = jumpCurvePoint;
            rt[3] = jumpLanding.position;
            rt[4] = jumpLanding.position;
            LTSpline sp = new LTSpline(rt);
            LeanTween.moveSpline(dc, sp, 0).setEaseOutBounce().setOnComplete(() =>
            {
                dc.GetComponent<jelly_dice>().slide(1);
               // Color clr = dc.GetComponent<jelly_dice>().hat.GetComponent<SpriteRenderer>().color;
               // clr.a = 1;
              //  dc.GetComponent<jelly_dice>().hat.GetComponent<SpriteRenderer>().color = clr;
                /*  Vector3 jumpCurvePoint = new Vector3(dc.transform.position.x, dc.transform.position.y + 0.5f, t2.position.z);
                 rt[0] = dc.transform.position;
                 rt[1] = dc.transform.position;
                 rt[2] = jumpCurvePoint;
                 rt[3] = t2.position;
                 rt[4] = t2.position;
                 LTSpline sp2 = new LTSpline(rt);
                 LeanTween.moveSpline(dc, sp2, 1).setEaseOutBounce();*/
            });
        }
        else
        {
            
            dc.transform.position = EnemyStartPos.position;
            Vector3 jumpCurvePoint = new Vector3(enemyJumpland.position.x, dc.transform.position.y + 0.3f, enemyJumpland.position.z);
            Vector3[] rt = new Vector3[5];
            rt[0] = dc.transform.position;
            rt[1] = dc.transform.position;
            rt[2] = jumpCurvePoint;
            rt[3] = enemyJumpland.position;
            rt[4] = enemyJumpland.position;
            LTSpline sp = new LTSpline(rt);
            LeanTween.moveSpline(dc, sp, 0f).setEaseOutBounce().setOnComplete(() =>
            {
                dc.GetComponent<jelly_dice>().slide(1);
               // Color clr = dc.GetComponent<jelly_dice>().hat.GetComponent<SpriteRenderer>().color;
              //  clr.a = 1;
              //  dc.GetComponent<jelly_dice>().hat.GetComponent<SpriteRenderer>().color = clr;
                /* Vector3 jumpCurvePoint = new Vector3(dc.transform.position.x, dc.transform.position.y + 0.5f, t2.position.z);
                 rt[0] = dc.transform.position;
                 rt[1] = dc.transform.position;
                 rt[2] = jumpCurvePoint;
                 rt[3] = t2.position;
                 rt[4] = t2.position;
                 LTSpline sp2 = new LTSpline(rt);
                 LeanTween.moveSpline(dc, sp2, 1).setEaseOutBounce();*/
            });
        }

        
    }


    
}
