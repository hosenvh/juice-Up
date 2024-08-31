using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class bubbleItem : MonoBehaviour
{
   private Animator animator;
   public Sprite init;
   private void Start()
   {
      animator = GetComponent<Animator>();
      init = GetComponent<Image>().sprite;
   }

   private void OnEnable()
   {
      if(init!=null)
      GetComponent<Image>().sprite = init;
      print("movert");
   }

   public  void  pop()
   {
      animator.SetTrigger("pop"); // 
   }

   public void Disable()
   {
      gameObject.SetActive(false);
     // StartCoroutine(co_disable());
   }

   IEnumerator co_disable()
   {
     // GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, -100);
    //  GetComponent<Image>().sprite = init;
      yield return null;
      
   }
   
  
}
