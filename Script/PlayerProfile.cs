using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile : SingleTon<PlayerProfile>
{
    // Start is called before the first frame update
    public int level, money;
    
    public int getLevel()
    {
        level = PlayerPrefs.GetInt("level");
        return level;
    }

    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt("level",level);
    }

    public int getMoney()
    {
        
        money = PlayerPrefs.GetInt("money");
        return money;
    }

    public void SetMoney(int val)
    {
        PlayerPrefs.SetInt("money", val);
    }


}
