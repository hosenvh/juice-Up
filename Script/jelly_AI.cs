using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jelly_AI : SingleTon<jelly_AI>
{
    public float DecisionTime,desicionMargin;
    public GameObject Reward;
    public Image Glue, Fanar, hole, super;
    public int AI_Level;
    public float MoveSpeed, MoveDelay,animSpeed;
    private void Start()
    {
       
        SetAiLevel(GameInfo.Instance.juiceLevelScobjt.levelInt);
        NumberOfInGredients = GameInfo.Instance.juiceLevelScobjt.ingredients.Count;
        rewardRow = -1;
        StartCoroutine(Decision_Loop());
        InvokeRepeating("rewardCheck", 5, 5);
    }

    public static event Func<List<jelly_cell>,int> OnCheckingRows;
    public static event Func<List<jelly_cell>, int> OnCheckingRowsForReward;

    public int Testlevel;
    void SetAiLevel(int level)
    {
        print("level" + level);
        level = Testlevel;
        
        switch (level)
        {
            case 1: case 2:
                DecisionTime = 1.8f;
                MoveDelay = 0.205f;
                animSpeed = 1;
                
                break;
            case 3: case 4:
                MoveDelay = 0.1f;
                animSpeed = 1.3f;
                DecisionTime = 1.5f;
                
                break;
            case 5: case 6: case 7:
                MoveDelay = 0.1f;
                animSpeed = 1.6f;
                DecisionTime = 1;
                break;
            case 8: case 9: case 10:
                MoveDelay = 0.1f;
                animSpeed = 1.9f;
                DecisionTime = 0.7f;
                break;
            case 11: case 12: case 13:
                MoveDelay = 0.1f;
                animSpeed = 2.3f;
                DecisionTime = 0.5f;
                break;
        }
        MoveSpeed = 0.7f/animSpeed;
    }

    IEnumerator Decision_Loop()
    {
        float start=Time.time;
        var delay=UnityEngine.Random.Range(DecisionTime, DecisionTime+desicionMargin);
       // print(delay);
        while(Time.time-start<delay)
        {
            yield return null;
        }
        try
        {
            MakeDesicion();
        }catch (Exception e)
        {
            Debug.LogException(e);
        }
       
        StartCoroutine(Decision_Loop());
    }

    public int currentrow,currentRowForREwardChk,currentrowForCart;

    public int rewardRow;
    public int NumberOfInGredients,currentIngredient;
    public bool bonusMulti, bonusAdd,ingred;
    void rewardCheck()
    {
        if (Reward.gameObject.activeInHierarchy) return;
       
        List<jelly_cell> cells = new List<jelly_cell>();
        rewardRow = -1;
        foreach (var row in Board.Instance.allrows)
        {
            rewardRow++;
            currentRowForREwardChk = 0;

            OnCheckingRowsForReward(row);
            if (currentRowForREwardChk ==0 )
            {
                if (currentIngredient >= NumberOfInGredients)
                {
                    currentIngredient = 0;
                }
                
                
                if (!bonusMulti && !bonusAdd && !ingred)
                {
                    ingred = true;
                    currentIngredient++;
                    print("current ingredient" + currentIngredient);
                    Reward.GetComponent<JellyReward>().renderer.sprite =
                        GameInfo.Instance.juiceLevelScobjt.ingredients[currentIngredient - 1];
                    Reward.transform.position=row[5].transform.position;
                    
                }
                else if(!bonusAdd && !bonusMulti && ingred)
                {
                    
                    Reward.GetComponent<JellyReward>().SetAdd();
                    bonusAdd = true;
                    //bring add
                    //bring multi
                }
                else if(bonusAdd && !bonusMulti && ingred)
                {
                    
                    Reward.GetComponent<JellyReward>().SetMulti();
                    
                    bonusMulti = true;
                    //do nothing
                }
                else
                {
                    Reward.GetComponent<JellyReward>().ResetReward();
                    bonusAdd = false;
                    bonusMulti = false;
                    ingred = false;
                }
                
               Reward.gameObject.SetActive(true);
                return;
            }
            // print("row" + c + "  " + currentrow);
            
        }
    }




    void MakeDesicion()
    {
        //  UseCart(UnityEngine.Random.Range(0, 6));
        int _rowIndex = 0;
        int c = 0;
        int cach = 0;
        List<jelly_cell> cells = new List<jelly_cell>();
        foreach(var row in Board.Instance.allrows)
        {
            currentrow = 0;
            try
            {
                OnCheckingRows(row);
            }
            catch (Exception e)
            {

            }
            
            if(currentrow<cach)
            {
                cells = row;
                cach = currentrow;
                _rowIndex = c;
            }
           // print("row" + c + "  " + currentrow);
            c++;
        }
        if(cach<0)
        {
            //getting attack from this row
            print("cache is"+cach);
            if (cach<-6 && UseCart(_rowIndex))
            {
                print("ccc");
                return;
            }
            int biggestDice = 0;
            jelly_dice defendingDice=new jelly_dice();
            foreach(var dice in jelly_dicemaker.Instance.PlayerBdices)
            {
                if(dice.Val>biggestDice&& dice.move)
                {
                    defendingDice = dice;
                    biggestDice=dice.Val;
                }

            }
           // print(biggestDice+" is biggest dice to defend"+cach);

            Vector3 land = cells[cells.Count-1].GetComponent<jelly_cell>().landingPos;
            if(cells==null || defendingDice==null)
            {
                return;
            }
            Vector3 curePoint = new Vector3(land.x, defendingDice.transform.position.y + 0.5f, land.z);

            Vector3[] pt = new Vector3[5];
            pt[0] = defendingDice.transform.position;
            pt[1] = defendingDice.transform.position;
            pt[2] = curePoint;
            pt[3] = land;
            pt[4] = land;

            LTSpline sp = new LTSpline(pt);

            var tomove = defendingDice;
            jelly_dicemaker.Instance.PlayerBdices.Remove(tomove);
            tomove.GetComponent<jelly_dice>().move = false; 
            LeanTween.moveSpline(tomove.gameObject, sp, 0.3f).setOnComplete(() =>
            {
                tomove.GetComponent<jelly_dice>().startRow(cells);
            });
        }else if(cach==0)
        {
            //make blind attack
            if(jelly_dicemaker.Instance.PlayerBdices.Count>0)
            {
               //dificulty 
                var randomRow = rewardRow != -1? Board.Instance.allrows[rewardRow]: Board.Instance.allrows[UnityEngine.Random.Range(0, 6)];
                // var diceIndex =UnityEngine.Random.Range(0, jelly_dicemaker.Instance.PlayerBdices.Count - 1);
                //  print("BLenght=" + jelly_dicemaker.Instance.PlayerBdices.Count + "  and diceIndex is" + diceIndex);
                jelly_dice randomHat = null;
                int diceCach = 6;
                if(rewardRow!=-1)
                {
                    foreach (var dice in jelly_dicemaker.Instance.PlayerBdices)
                    {
                        if (dice.move && dice.Val < diceCach)
                        {
                            randomHat = dice;
                            diceCach = dice.Val;
                            rewardRow = -1;
                        }

                    }

                }
                else
                {
                    foreach (var dice in jelly_dicemaker.Instance.PlayerBdices)
                    {
                        if (dice.move )
                        {
                            randomHat = dice;
                            break;
                            
                        }

                    }

                }
                if (randomHat == null)
                {
                    UseCart(UnityEngine.Random.Range(0, 6));


                    return;
                }
                Vector3 land = randomRow[randomRow.Count - 1].GetComponent<jelly_cell>().landingPos;
                Vector3 curePoint = new Vector3(land.x, randomHat.transform.position.y + 0.5f, land.z);

                Vector3[] pt = new Vector3[5];
                pt[0] = randomHat.transform.position;
                pt[1] = randomHat.transform.position;
                pt[2] = curePoint;
                pt[3] = land;
                pt[4] = land;

                LTSpline sp = new LTSpline(pt);

                var tomove = randomHat;
                tomove.GetComponent<jelly_dice>().move = false;
                jelly_dicemaker.Instance.PlayerBdices.Remove(randomHat); 
                //p
                LeanTween.moveSpline(tomove.gameObject, pt, 0.3f).setOnComplete(() =>
                {
                    tomove.GetComponent<jelly_dice>().startRow(randomRow);
                });

            }
        }

    }
    List<int> trappedRowsByAi=new List<int>(); 
    

    IEnumerator MarkRowForTrap(int row,float deactivatetime)
    {
        trappedRowsByAi.Add(row);
        yield return new WaitForSeconds(deactivatetime);    
        trappedRowsByAi.Remove(row);
    }

    bool UseCart(int row)
    {
        print("usingcard "+row);
        if(trappedRowsByAi.Contains(row))
        {
            return false;
        }
        if(AI_Level==5)
        {
            
            /*foreach(var cart in PlayerBCaertDeck.Instance.cards)
            {
                if(cart.fillAmount==1)
                {
                   for(int i=6;i<11;i++)
                    {
                        var cell=Board.Instance.allrows[row][i];
                        if(cell.cellState==Cell.open)
                        {
                            GameManager.Instance.changeStar(2, -cart.GetComponent<jellyCard>().TotalFragments);
                            cart.GetComponent<jellyCard>().filledFragments = 0;
                            cart.fillAmount = 0;
                            cell.AddEffect(cart.GetComponent<jellyCard>().cardType, 2);
                            MarkRowForTrap(row, 20);
                            Match_res.Instance.Magic_used_opp++;
                            return true;
                        }
                    }
                }
            }*/
        }
       
            return false;
        
    }


}
