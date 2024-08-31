using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class DeckItem : MonoBehaviour,IPointerClickHandler
{
    public int price, duration;
    public Cards card;
    public TextMeshProUGUI priceText, duration_text;
    bool locked;
    public GameObject Lock;
    public GameObject highLight;
    private void Start()
    {
        priceText.text =price.ToString();
        duration_text.text =duration.ToString();    
        checkAvaiability();
    }

    void checkAvaiability()
    {
        if(PlayerProfile.Instance.getMoney()<price)
        {
            locked = true;
            Lock.SetActive(true);
        }
        else
        {
            locked = false;
            Lock.SetActive(false);  
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(locked)
        {

        }
        else
        {
            foreach(var item  in transform.parent.GetComponentsInChildren<DeckItem>())
            {
                item.highLight.SetActive(false);
            }
            highLight.SetActive(true);
            GameInfo.Instance.cartPurchase = price;
            GameInfo.Instance.cards.Clear();
            GameInfo.Instance.Cartduration=duration;
            GameInfo.Instance.cards.Add(card);
        }
    }
}
