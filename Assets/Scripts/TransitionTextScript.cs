using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTextScript : MonoBehaviour
{
   public Transform box;
   public int newLocation;
   public float speed;
   public float delay;
   public GameObject nextGameObject;
   /*
   Script moves box up into the user's screen and then down when closeDialog is clicked.
   Further, it sets the next game Object to be active.
   */
   public void OnEnable()
   {
       box.localPosition = new Vector2(0, -Screen.height);
       box.LeanMoveLocalY(newLocation,speed).setEaseOutExpo().delay = 1f;
   }

   public void CloseDialog()
   {
      box.LeanMoveLocalY(-Screen.height,speed).setEaseInExpo().setOnComplete(OnComplete);
   }

   public void OnComplete()
   {
      gameObject.SetActive(false);
      nextGameObject.SetActive(true);
   }

}
