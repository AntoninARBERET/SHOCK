using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableFloor : MonoBehaviour
{
    // Start is called before the first frame update
    private bool playerOnFloor;
    private int nbContact;
    void Start()
    {
      playerOnFloor = false;
      nbContact = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
   {
     if(collision.gameObject.tag=="Player" && !playerOnFloor){
       playerOnFloor = true;
       nbContact++;
       UnityEngine.Debug.Log(nbContact);
     }
   }

   void OnCollisionExit(Collision collision)
  {
    if(collision.gameObject.tag=="Player"){
      playerOnFloor = false;
    }
  }

   public int getNbContact(){
     return nbContact;
   }
}
