using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{   public Transform target;
    public float distanceFromObject = 6f;
    public bool lookPl=false;
    Vector3 playerPrevPos, playerMoveDir;
    public float speed = 5;
    public float a;
    public float b;
    public float c;
        Vector3 offset;
 public GameObject player;
    [SerializeField]
     private Vector3 offsetPosition;

     [SerializeField]
     private Space offsetPositionSpace = Space.Self;

     [SerializeField]
     private bool lookAt = true;
    void Start()
    {
      playerPrevPos = player.transform.position;
    }


    void LateUpdate () {
      playerMoveDir = player.transform.position - playerPrevPos;
      if (playerMoveDir != Vector3.zero)
      {
          transform.position = player.transform.position - playerMoveDir * distanceFromObject;
          transform.position = new Vector3(transform.position.x+a, transform.position.y+b, transform.position.z+c);
          //transform.position.y += h; // required height

          transform.LookAt(player.transform.position);

          playerPrevPos = player.transform.position;
      }
  }

  /*  void Update(){
        if(lookPl){
            Refresh();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!lookPl){
            Vector3 lookOnObject = target.position - transform.position;
            lookOnObject = target.position - transform.position;
            transform.forward = lookOnObject.normalized;

            Vector3 targetLastPosition;
            targetLastPosition = target.position - lookOnObject.normalized * distanceFromObject;
            transform.position = targetLastPosition;

            targetLastPosition.y = target.position.y + distanceFromObject/2;
            transform.position =    targetLastPosition;
        }
    }
    public void setLookPl(){
        lookPl=true;
    }

    public void Refresh()
     {
         if(target == null)
         {
             Debug.LogWarning("Missing target ref !", this);

             return;
         }

         // compute position
         if(offsetPositionSpace == Space.Self)
         {
             transform.position = target.TransformPoint(offsetPosition);
         }
         else
         {
             transform.position = target.position + offsetPosition;
         }

         // compute rotation
         if(lookAt)
         {
             transform.LookAt(target);
         }
         else
         {
             transform.rotation = target.rotation;
         }
     }*/
}
