using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerACardDeck : SingleTon<PlayerACardDeck>
{
    public List<Image> cards;
    public GameObject card;
    public Sprite glue, fanar, chale;

    public static event Action<bool> CartEnabled;

    public void UnhighLight()
    {
        CartEnabled(false);
    }

    private void Start()
    {
        if(true)
        {
            PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")-GameInfo.Instance.cartPurchase);
            var temp=Instantiate(card);
            temp.transform.SetParent(transform);
            
            cards.Add(temp.GetComponentInChildren<jellyCard>().GetComponent<Image>());
            temp.GetComponentInChildren<jellyCard>().cardType = GameInfo.Instance.juiceLevelScobjt.card;
            temp.transform.localScale=Vector3.one;
            
            switch(temp.GetComponentInChildren<jellyCard>().cardType)
            {
                case Cards.glu:
                    temp.GetComponentInChildren<jellyCard>().GetComponent<Image>().sprite = glue;
                    break;
                case Cards.fanar:
                    temp.GetComponentInChildren<jellyCard>().GetComponent<Image>().sprite = fanar;  
                    break;
                case Cards.hole:
                    temp.GetComponentInChildren<jellyCard>().GetComponent<Image>().sprite = chale;
                    break;
            
            
            }
        }
    }
    public void visualize(int val)
    {
       // print(val + " the value befreduct");
        foreach (var card in cards)
        {
            var _card = card.GetComponent<jellyCard>();
            
                val-=_card.filledFragments;          

        }
       // print(val+" the value");
            foreach (var card in cards)
        {
            var _card=card.GetComponent<jellyCard>();
            
            while (val > 0 && _card.filledFragments<_card.TotalFragments)
            {
                --val;
                ++_card.filledFragments;
                var fillAmount= (float)_card.filledFragments / (float)_card.TotalFragments;
                _card.GetComponent<Image>().fillAmount = fillAmount;
                if(fillAmount>=1)
                {
                    Color clr = _card.GetComponent<Image>().color;
                    clr.a = 1;
                    _card.GetComponent<Image>().color = clr;
                    CartEnabled(true);
                    StadiumSound.Instance.PlayBonus();
                }
            }
        }

        



        /*var numberOfFilledCards = (val / cards.Count);
        print(numberOfFilledCards+"vxz"+val+" "+cards.Count);
        for(int i = 0; i < numberOfFilledCards; i++)
        {
            cards[i].fillAmount = 1;
        }

        var fragmentParts=(val% cards.Count);   
        if(numberOfFilledCards<cards.Count)
        {
            cards[numberOfFilledCards].fillAmount = (float)fragmentParts / (float)cards.Count;
        }*/
        
    }

   

}
