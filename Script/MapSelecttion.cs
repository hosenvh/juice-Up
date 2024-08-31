using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapSelecttion : SingleTon<MapSelecttion>
{
    // Start is called before the first frame update
    public Button StateSelectionConfirm;
    public List<jellyState> jellyStates;
    public GameObject map;
    public TextMeshProUGUI selectedStateName, SelectedStateReward, SelectedStateLevel,PlayerLevel,PlayerMoney,PlayTime;
    public int totalGames, remainingGames,GamesPlayed;
    public MapMessage mapmessage;
    public GameObject StateMap, DeckDashBoard;
    public Slider LifeBar;
    //;
    private void Start()
    {
        GameInfo.Instance.ResetStats();
        int captured = 0;
        foreach(var jellyState in map.GetComponentsInChildren<jellyState>())
        {
            jellyStates.Add(jellyState);
            if (jellyState.state.color == Color.red )
            {
                captured++;
            }
        }
        GamesPlayed = PlayerPrefs.GetInt("played");
        remainingGames = totalGames - GamesPlayed;
        

        float totalLos = totalGames - jellyStates.Count;
        float lost = GamesPlayed - captured;
        float LifereamainigPercentage=(float)(1-((float)lost / (float)totalLos));

        LifeBar.value = LifereamainigPercentage;

        if (jellyStates.Count-captured>remainingGames)
        {
            //Game over
            mapmessage.showFinal($"You Failed to Conquer The U.S ");
        }
        PlayerLevel.text= (PlayerProfile.Instance.getLevel()+1).ToString();
       // LevelVisualizer.Instance.SetLevel(PlayerProfile.Instance.getLevel());
       //    PlayerLevel.text=PlayerProfile.Instance.getLevel().ToString();
        PlayerMoney.text=PlayerProfile.Instance.getMoney().ToString();
    }
    public void UnhighLightAllStates()
    {
        foreach (var jellyState in jellyStates)
        {
            jellyState.HighlightOff();
        }
    }
    public GameObject backBtn;

    public void BackToMapState()
    {
        DeckDashBoard.SetActive(false);
        StateMap.SetActive(true);
        backBtn.SetActive(false);
    }
    public void OpenGameScene()
    {
        if(StateMap.activeInHierarchy)
        {
            DeckDashBoard.SetActive(true);
            StateMap.SetActive(false);
            backBtn.SetActive(true);
        }else if(DeckDashBoard.activeInHierarchy){
            StartCoroutine(LoadGameScene());
        }
       //
    }

    public Image LoaderImage;
    public Sprite l1, l2, l3,l4,l5;


    IEnumerator LoadGameScene()
    {
        Time.timeScale = 1;
        switch(PlayerProfile.Instance.getLevel())
        {
            case 0:
                LoaderImage.sprite = l1;    
                break;
            case 1:
                LoaderImage.sprite=l2;
                break;
            case 2:
                LoaderImage.sprite = l3;
                break;
            case 3:
                LoaderImage.sprite = l4;
                break;
            case 4:
                LoaderImage.sprite = l5;    
                break;
        }
        LoaderImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        print("loadingScene");
        SceneManager.LoadScene(1);

    }
    public void Restart()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
