using System;
using System.Collections;
using System.Collections.Generic;
//using Cafebazaar;
//using Cafebazaar;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingleTon<GameManager>
{
    public bool coffebazar;
    public TextMeshProUGUI player, Opp;
    public DrinkBox drinkBox;
    public Sprite player1,player2;
    public Sprite _1, _2, _3, _4, _5, _6;
    int playerAScore, playerBscore;
    public int playerAstar, playerBstar;   
    public Canvas _canvas;
    public LayerMask cellMask;
    int maxCart;
    public  Slider slider;
    public GameObject QuickMessage;
    public TextMeshProUGUI QuickMessage_text;
    public Image PlayerGlass, OppGlass;
    public List<Image> playerIngredients;
    public List<Image> oppingredient;
    private int GlassMax;
    private void Update()
    {
        /*var elapsed = (float)(Time.time - GameStartTime) / GameDuration;
       // slider.value =1- elapsed; 
        if(elapsed>=1)
        {
            if (finalgoal) return;
            finalgoal = true;
            if (playerAScore==playerBscore)
            {
                //DRAW
                ShowQuickMess("Final Touch Down!!");
                
                
            }else if(playerAScore>playerBscore)
            {
                //A won
                GameFinish(true);
            }
            else
            {
                //B won
                GameFinish(false);
            }
        }*/
    }
    bool finalgoal;

    void CheckWin()
    {
        var oppGlassFull = false;
        var playerGlassFull = false;
        if (PlayerGlass.fillAmount >= 1)
        {
            playerGlassFull = true;
        }

        if (OppGlass.fillAmount >= 1)
        {
            oppGlassFull = true;
        }

        var oppIngredientsFull = true;
        var playerIngredientsFull = true;

        foreach (var VARIABLE in playerIngredients)
        {
            if (VARIABLE.GetComponent<CanvasGroup>().alpha != 1)
            {
                playerIngredientsFull = false;
            }
        }
        
        foreach (var VARIABLE in oppingredient)
        {
            if (VARIABLE.GetComponent<CanvasGroup>().alpha != 1)
            {
                oppIngredientsFull = false;
            }
        }

        if (playerGlassFull && playerIngredientsFull)
        {
            GameFinish(true);
        }

        if (oppGlassFull && oppIngredientsFull)
        {
            GameFinish(false);
        }
    }
        
    public void IngredientCheck(int team, Sprite sprite)
    {
        if (team == 1)
        {
            foreach (var VARIABLE in playerIngredients)
            {
                if (VARIABLE.sprite == sprite)
                {
                    VARIABLE.GetComponent<CanvasGroup>().alpha = 1;
                }
            }
        }
        else
        {
            foreach (var VARIABLE in oppingredient)
            {
                if (VARIABLE.sprite == sprite)
                {
                    VARIABLE.GetComponent<CanvasGroup>().alpha = 1;
                }
            }
        }
        CheckWin();
    }

    private bool finished;
    private Action _succ,_fail;

    public GameObject finalBTN;
    void success()
    {
        finalBTN.SetActive(true);
    }

    void fail()
    {
      //  MiniGame.SendScore(GameInfo.Instance.juiceLevelScobjt.levelMax, _succ, _fail);
    }
    
    public void GameFinish(bool won)
    {
        if(finished) return;
        finished = true;
       // Time.timeScale = 0;
        Match_res.Instance.showRes();
        FinalMessageBox.SetActive(true);
        if (won)
        {
            if (coffebazar)
            {
              //  MiniGame.SendScore(GameInfo.Instance.juiceLevelScobjt.levelMax, _succ, _fail);
            }
            else
            {
                finalBTN.SetActive(true);
            }
           
            drinkBox.gameObject.SetActive(true);
            drinkBox.SetIngredients(GameInfo.Instance.juiceLevelScobjt.ingredients,GameInfo.Instance.juiceLevelScobjt.levelColor);
            FinalMessageText.text = "YOU WON";
            if(!GameInfo.Instance.Online)
            {
                if (PlayerPrefs.GetInt("jlevel") < GameInfo.Instance.juiceLevelScobjt.levelInt )
                {
                    PlayerPrefs.SetInt("jlevel", GameInfo.Instance.juiceLevelScobjt.levelInt );
                    
                }
                PlayerPrefs.SetInt(GameInfo.Instance.stateName, 1);
            }
            
            PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")+GameInfo.Instance.reward);
           
        }
        else
        {
          // MiniGame.SendScore(0, _succ, _fail);
            FinalMessageText.text = "TRY AGAIN";
        }
    }

    void ShowQuickMess(string message)
    {
        QuickMessage.SetActive(true);
        QuickMessage_text.text = message;
        
        
        LeanTween.scale(QuickMessage, Vector2.one, 1f).setEaseInBounce().setOnComplete(() =>
          {
             StartCoroutine( PauseGameForSeconds(3));
              LeanTween.scale(QuickMessage, Vector2.zero, 0.2f).setEaseInCirc().setOnComplete(() =>
                {
                    Time.timeScale = 1;
                    QuickMessage.SetActive(false);
                });

          });


    }

    IEnumerator PauseGameForSeconds(float val)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(val);
        Time.timeScale = 1; 
    }

    public void Finish()
    {
         
        SceneManager.LoadScene(0);
    }


    public GameObject FinalMessageBox;
    public TextMeshProUGUI FinalMessageText;


    public void changeScore(int player,int val)
    {
      //  print("changeScore" + player + " " + val);
        if(player == 1)
        {
            playerAScore+=val;
            LeanTween.scale(playerScore.gameObject, 3 * Vector3.one, 0.3f).setOnComplete(() =>
            {
                LeanTween.scale(playerScore.gameObject, Vector3.one, 0.3f);
            });
            if (finalgoal)
            {
                GameFinish(true);
            }
        }else if(player == 2)
        {
            LeanTween.scale(enemyscore.gameObject, 3 * Vector3.one, 0.3f).setOnComplete(() =>
            {
                LeanTween.scale(enemyscore.gameObject, Vector3.one, 0.3f);
            });
            playerBscore +=val;
            if (finalgoal)
            {
                GameFinish(false);
            }
        }
        visualizeScors();
        CheckWin();
    }
    
    public int playerAMaxCard, playerBMaxcard;

    public void changeStar(int player,int val)
    {
        if (player == 1)
        {
            playerAstar=Mathf.Clamp( playerAstar + val,0, maxCart);
           
        }
        else if (player == 2)
        {
            
            playerBstar = Mathf.Clamp( playerBstar + val,0, maxCart);
        }
        visualizeScors();
    }
    public float GameDuration;
    float GameStartTime;
    private void Start()
    {
        _succ = success;
        _fail = fail;
        GameDuration = GameInfo.Instance.GameDuration;
        GlassMax = GameInfo.Instance.juiceLevelScobjt.levelMax;
        PlayerGlass.color = GameInfo.Instance.juiceLevelScobjt.levelColor;
        OppGlass.color = GameInfo.Instance.juiceLevelScobjt.levelColor;
        foreach (var VARIABLE in playerIngredients)
        {
            VARIABLE.gameObject.SetActive(false);
        }
        
        foreach (var VARIABLE in oppingredient)
        {
            VARIABLE.gameObject.SetActive(false);
        }

        int c = 0;
        foreach (var VARIABLE in GameInfo.Instance.juiceLevelScobjt.ingredients)
        {
            playerIngredients[c].gameObject.SetActive(true);
            oppingredient[c].gameObject.SetActive(true);

            playerIngredients[c].sprite = VARIABLE;
            oppingredient[c].sprite = VARIABLE;
            
            playerIngredients[c].GetComponent<CanvasGroup>().alpha = 0.3f;
            oppingredient[c].GetComponent<CanvasGroup>().alpha = 0.3f;

            
            
            c++;
        }
        
        if (!GameInfo.Instance.Online)
        {
            PlayerPrefs.SetInt("played", PlayerPrefs.GetInt("played") + 1);
        }
        
        GameStartTime = Time.time;
        Time.timeScale = 1;
        visualizeScors();
        cellMask = LayerMask.GetMask("cell");
        foreach(var k in PlayerACardDeck.Instance.cards)
        {
            maxCart += k.GetComponent<jellyCard>().TotalFragments;

        }
    }

    public TextMeshProUGUI playerScore, enemyscore;



    void visualizeScors()
    {
       // playerScore.text =playerAScore.ToString();
      //  enemyscore.text=playerBscore.ToString();
      player.text = (Math.Round(PlayerGlass.fillAmount,2)  * 100).ToString();
      Opp.text = (Math.Round(OppGlass.fillAmount,2)  * 100).ToString();
        PlayerGlass.fillAmount = (float)playerAScore / (float)GlassMax;
        OppGlass.fillAmount = (float)playerBscore / (float)GlassMax;
       PlayerACardDeck.Instance.visualize(playerAstar);
//       PlayerBCaertDeck.Instance.visualize(playerBstar);
    }
}

public enum Cell
{
    glu,
    fanar,
    hole,
    open,
    closed
}
public enum Cards
{
    glu,
    fanar,
    hole,
    jumpTrap

}
