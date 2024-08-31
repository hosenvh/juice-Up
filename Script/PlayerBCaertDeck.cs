using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBCaertDeck : SingleTon<PlayerBCaertDeck>
{
    public List<Image> cards;
    private void Start()
    {
        switch (GameInfo.Instance.OpponentLevel)
        {
            case 0:
                cards[1].gameObject.SetActive(false);
                cards[2].gameObject.SetActive(false);
                return;
            case 1:
                cards[0].gameObject.SetActive(false);
                cards[2].gameObject.SetActive(false);
                return;
            case 2:
                cards[0].gameObject.SetActive(false);
                cards[2].gameObject.SetActive(false);
                return;
            case 3:
                cards[0].gameObject.SetActive(false);
                cards[1].gameObject.SetActive(false);
                return;
            case 4:
                cards[0].gameObject.SetActive(false);
                cards[1].gameObject.SetActive(false);
                return;
        }
    }
    public void visualize(int val)
    {
       // print(val + " the value befreduct");
        foreach (var card in cards)
        {
            var _card = card.GetComponent<jellyCard>();

            val -= _card.filledFragments;

        }
       // print(val + " the value");
        foreach (var card in cards)
        {
            var _card = card.GetComponent<jellyCard>();

            while (val > 0 && _card.filledFragments < _card.TotalFragments)
            {
                --val;
                ++_card.filledFragments;
                _card.GetComponent<Image>().fillAmount = (float)_card.filledFragments / (float)_card.TotalFragments;
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
