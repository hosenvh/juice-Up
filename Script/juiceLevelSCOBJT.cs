using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/JuicyLevels", order = 1)]
public class juiceLevelSCOBJT : ScriptableObject
{
   public string levelName;
   public int levelInt;
   public List<Sprite> ingredients;
   public Color levelColor;
   public int levelMax;
   
   public int PlusBonus;
   public int MultiBonus;
   public Cards card;

}
