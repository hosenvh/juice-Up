using System;
using System.Collections;
using System.Collections.Generic;
//using Cafebazaar;
//using Cafebazaar;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class JuiceUpLevelManager : SingleTon<JuiceUpLevelManager>
{
     public bool CoffeBazra;
    private void Start()
    {
        StartCoroutine(SetUp());
    }

    IEnumerator SetUp()
    {
        if(CoffeBazra)
      //  MiniGame.Initialize();
        yield return null;
    }

  public List<Color> colors;
 public TextMeshProUGUI play, level, part;
 public List<juiceLevelSCOBJT> levels;
 public Image sky;
 public (Color back, Color bubble) getcollor(int index)
 {
  Color back = colors[index];
  play.color = new Color(back.r, back.g, back.b, 1);
  level.color = new Color(back.r, back.g, back.b, 1);
  sky.color=new Color(back.r, back.g, back.b, 0.12f);
  back = new Color(back.r, back.g, back.b, (float)147/(float)256);
  Color buble = new Color(back.r, back.g, back.b, (float)187/(float)256);
  if (index >= levels.Count)
  {
      index = levels.Count - 1;
  }
  GameInfo.Instance.juiceLevelScobjt = GetLevelData(index);
  level.text = GameInfo.Instance.juiceLevelScobjt.levelName;
  GameInfo.Instance.juiceLevelScobjt.levelColor = back;
  return (back,buble);
 }



 public juiceLevelSCOBJT GetLevelData(int index)
 {
     return levels[index];
 }

 public void GoToGame()
 {
     SceneManager.LoadScene("jelly");
 }
 
}
