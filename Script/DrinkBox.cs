using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DrinkBox : MonoBehaviour
{
   public Animator ney;
   public Image fill;
   public List<Image> ingredients;
   public GameObject glsBtn;
   private List<GameObject> animamatioList;

   private void Start()
   {
      animamatioList = new List<GameObject>();
   }

   public void SetIngredients(List<Sprite> sprites,Color color)
   {
      fill.color = color;
      animamatioList = new List<GameObject>();
      var c = 0;
      foreach (var VARIABLE in ingredients)
      {
         c++;
         if (sprites.Count >= c)
         {
            VARIABLE.sprite = sprites[c - 1];
            animamatioList.Add(VARIABLE.gameObject);
         }
         else
         {
            VARIABLE.gameObject.SetActive(false);
         }

         foreach (var VARIABLE2 in animamatioList)
         {
            VARIABLE2.gameObject.SetActive(false);
         }

         StartCoroutine(ListAnimate());
      }


      IEnumerator ListAnimate()
      {
         foreach (var VARIABLE in animamatioList)
         {
            VARIABLE.gameObject.SetActive(true);
            VARIABLE.transform.DOMove(glsBtn.transform.position, 2);
            VARIABLE.GetComponent<CanvasGroup>().DOFade(0, 2);
            yield return new WaitForSeconds(2);
         }

         float start = Time.time;
         ney.SetTrigger("drink");
         while (Time.time - start<6)
         {
            var lerp = (Time.time - start) / 6;
            fill.fillAmount = 1 - lerp;
            yield return null;
         }
         ney.SetTrigger("stop");
      }
   }
}
