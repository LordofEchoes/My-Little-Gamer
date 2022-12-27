using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTextScript : PopUpScript
{
   public GameObject NextGameObject;
   /*
   Script moves obj  into the user's screen and then down when closeDialog is clicked inheiriting from PopUpScript.
   Further, it sets the next game Object to be active through overriding the OnComplete() function
   */

   public override void OnComplete()
   {
      Object.SetActive(false);
      NextGameObject.SetActive(true);
   }

}
