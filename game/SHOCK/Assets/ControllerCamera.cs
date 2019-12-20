using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamer : MonoBehaviour
{   public Transform target;
    public float distanceFromObject = 6f;
    public bool lookPl=false;

    [SerializeField]
     private Vector3 offsetPosition;
 
     [SerializeField]
     private Space offsetPositionSpace = Space.Self;
 
     [SerializeField]
     private bool lookAt = true;

    void Start()
    {

    }
    void Update(){
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
     }
}