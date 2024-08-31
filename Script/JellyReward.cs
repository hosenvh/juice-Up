using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyReward : MonoBehaviour
{
    public SpriteRenderer renderer;
    public List<Transform>  _1th, _2th, _3th, _v2_1th, _v2_2th;
    public GameObject _3dNUmbers;
    public bool Add, multi;
    public GameObject Plus, Multi;
    public void SetMulti()
    {
        if(GameInfo.Instance.juiceLevelScobjt.MultiBonus==0) return;
        Plus.SetActive(false);
        Multi.SetActive(true);
        multi = true;
        Add = false;
        renderer.enabled = false;
        _3dNUmbers.SetActive(true);
        SetValue(GameInfo.Instance.juiceLevelScobjt.MultiBonus);
        print("vsetmulti");
    }

    public void SetAdd()
    {
        if(GameInfo.Instance.juiceLevelScobjt.PlusBonus==0) return;
        Plus.SetActive(true);
        Multi.SetActive(false);
        multi = false;
        Add = true;
        renderer.enabled = false;
        _3dNUmbers.SetActive(true);
        SetValue(GameInfo.Instance.juiceLevelScobjt.PlusBonus);
        print("vsetadd");
    }

    public void ResetReward()
    {
        Plus.SetActive(false);
        Multi.SetActive(false);
        multi = false;
        Add = false;
        _3dNUmbers.SetActive(false);
        renderer.enabled = true;
    }

    public int Val;
    
    public void SetValue(int val)
    {
        
        this.Val=val;
        _1th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
        _2th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
        _3th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
        _v2_1th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
        _v2_2th.ForEach((t) =>
        {
            t.gameObject.SetActive(false);
        }); 
      
        if (val <= 9 || val > 99)
        {
            if (val is <= 9 and > 0)
            {
                
                _2th[val].gameObject.SetActive(true);
            }else if (val > 9)
            {
                var _100t = val / 100;
                var _10t = (val%100) / 10;
                var _1t = (val%100) % 10;
                _1th[_100t].gameObject.SetActive(true);
                _2th[_10t].gameObject.SetActive(true);
                _3th[_1t].gameObject.SetActive(true);
            }
            
        }
        else 
        {
            var _10t = (val) / 10;
            var _1t = (val) % 10;
            _v2_1th[_10t].gameObject.SetActive(true);
            _v2_2th[_1t].gameObject.SetActive(true);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="dice")
        {
            //other.GetComponent<jelly_dice>().SetValue(6);
            if (!Add && !multi)
            {
                GameManager.Instance.IngredientCheck(other.GetComponent<jelly_dice>().team,renderer.sprite);
            }

            if (Add)
            {
                other.GetComponent<jelly_dice>().SetValue(other.GetComponent<jelly_dice>().Val+Val);
            }
            if (multi)
            {
                other.GetComponent<jelly_dice>().SetValue(other.GetComponent<jelly_dice>().Val*Val);
            }
            
            if(other.GetComponent<jelly_dice>().team==2)
            {
                Match_res.Instance.Bonus_opp++;
            }
            else
            {
                StadiumSound.Instance.PlayHitEnemy();
                Match_res.Instance.Bonus_you++;
            }
            gameObject.SetActive(false);
            jelly_AI.Instance.rewardRow = -1;
        }
    }
}
