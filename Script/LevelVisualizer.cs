using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelVisualizer : SingleTon<LevelVisualizer>
{
    public List<Image> levels;

    public void SetLevel(int level)
    {
        int c = 0;
        foreach (Image levelImage in levels)
        {
            if(level>=c)
            {
                levelImage.gameObject.SetActive(true);
            }
            else
            {
                Destroy (levelImage.gameObject); 
            }
            c++;
        }
    }
    
}
