using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class jellyState : MonoBehaviour,IPointerClickHandler
{
    public int level, reward,time;
    public TextMeshProUGUI REWARD_Text, LevelText;
    public Image Lock,state;
    public GameObject highlight;
    public string StateName;
    public bool Online;
    private void Start()
    {
        SetVisuals();
       // GetComponent<Image>().sprite=transform.parent.GetComponent<Image>().sprite; 
    }

    private void Awake()
    {
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt(StateName) == 1 && !Online)
        {
            state.color = Color.red;
        }
    }
    public void SetVisuals()
    {
        SetTexts();
        setLock();
        
    }


    public bool locked;
    void setLock()
    {
        if (Online)
        {
            locked = false;
            Lock.gameObject.SetActive(false);   
            return;
        }
        if(PlayerProfile.Instance.getLevel()<level)
        {
            Lock.gameObject.SetActive(true);
            locked = true;
           // state.color = Color.black;
        }
        else
        {
            Lock.gameObject.SetActive(false);
            locked = false;
        }
    }
   
    
    public void SetLevel(int level)
    {
        LevelText.text = "Level " + (level + 1);
    }
    void SetTexts()
    {
       //  REWARD_Text.text ="Reward:"+reward.ToString();
       // LevelText.text ="Level"+level.ToString();
       SetLevel(level);
    }
    public void HightlightOn()
    {
        if(transform.parent.GetComponent<Image>().color != Color.red)
        transform.parent.GetComponent<Image>().color = Color.yellow;
    }
    public void HighlightOff()
    {
        if (transform.parent.GetComponent<Image>().color != Color.red)
            transform.parent.GetComponent<Image>().color = Color.white;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        print("Ok");
        if(Online)
        {
            MapSelecttion.Instance.StateSelectionConfirm.interactable = true;
            MapSelecttion.Instance.UnhighLightAllStates();
            HightlightOn();
            MapSelecttion.Instance.SelectedStateLevel.text = "State Level: " + (level+1).ToString();

        }
        else
        {
            if (!locked)
            {
                MapSelecttion.Instance.PlayTime.text = "Game Time:   " + time.ToString();
                MapSelecttion.Instance.StateSelectionConfirm.interactable = true;
                MapSelecttion.Instance.UnhighLightAllStates();
                HightlightOn();
                MapSelecttion.Instance.SelectedStateLevel.text = "State Level:    " + (level + 1).ToString();
            }
            else
            {
                MapSelecttion.Instance.PlayTime.text ="Game Time:"+ time.ToString();
                MapSelecttion.Instance.StateSelectionConfirm.interactable = false;
                MapSelecttion.Instance.UnhighLightAllStates();
                HightlightOn();
                MapSelecttion.Instance.SelectedStateLevel.text = "State Level:  " + (level + 1).ToString() + " (Level Mismatch)! ";
            }
        }
       
        MapSelecttion.Instance.selectedStateName.text = StateName;
        
        MapSelecttion.Instance.SelectedStateReward.text=reward.ToString();
        GameInfo.Instance.OpponentLevel=level;  
        GameInfo.Instance.reward=reward;
        GameInfo.Instance.stateName=StateName;  
        GameInfo.Instance.Online=Online;
        GameInfo.Instance.GameDuration = time;
    }
}
