using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : SingleTon<GameInfo>
{
    public int OpponentLevel;
    public int reward,GameDuration;
    public string stateName;
    public bool Online;
    public List<Cards> cards;
    public int cartPurchase,Cartduration;

    public juiceLevelSCOBJT juiceLevelScobjt;
    public void ResetStats()
    {
        OpponentLevel = 0;
        reward = 0;
        GameDuration = 0;
        cards.Clear();
        cartPurchase = 0;
        cartPurchase = 0;
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
