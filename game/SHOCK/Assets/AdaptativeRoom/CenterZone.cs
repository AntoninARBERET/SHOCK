using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterZone : MonoBehaviour
{
    // Start is called before the first frame update
    private bool playerInRoom;
    void Start()
    {
      playerInRoom = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
   {
     if(collision.gameObject.tag=="Player"){
       playerInRoom = true;
     }
   }

   public bool getPlayerInRoom(){
     return playerInRoom;
   }


}
