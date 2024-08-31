using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Match_res : SingleTon<Match_res>
{
   public TextMeshProUGUI Touch_d_you_txt, Touch_d_opp_txt, Bonus_you_txt, Bonus_opp_txt, Magic_used_you_txt, Magic_used_opp_txt, Magic_succ_you_txt, Magic_succ_opp_txt, Opp_kill_you_txt, Opp_kill_opp_txt, Midpass_you_txt, MidPass_opp_txt;
   public int Touch_d_you, Touch_d_opp, Bonus_you, Bonus_opp, Magic_used_you, Magic_used_opp, Magic_succ_you, Magic_succ_opp, Opp_kill_you, Opp_kill_opp, Midpass_you, MidPass_opp;
   public void showRes()
    {
        Touch_d_you_txt.text =Touch_d_you.ToString();
        Touch_d_opp_txt.text =Touch_d_opp.ToString();
        Bonus_you_txt.text=Bonus_you.ToString();
        Bonus_opp_txt.text = Bonus_opp.ToString();
        Magic_used_you_txt.text = Magic_used_you.ToString();
        Magic_used_opp_txt.text =Magic_used_opp.ToString();
        Magic_succ_you_txt.text =(Magic_succ_you.ToString());
        Magic_succ_opp_txt.text =(Magic_succ_opp.ToString());   
        Opp_kill_you_txt.text=(Opp_kill_you.ToString());    
        Opp_kill_opp_txt.text=Opp_kill_opp.ToString();
        Midpass_you_txt.text =Midpass_you.ToString();
        MidPass_opp_txt.text = MidPass_opp.ToString();
    }
}
