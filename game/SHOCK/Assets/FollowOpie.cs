﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOpie : MonoBehaviour
{
  public GameObject player;
  public Transform p1;
  public Transform p2;
  float target_dist;
  public float allowed_dist=0.75f;
  float speed;
  RaycastHit shot;
  private bool calibration=true;
  private bool gotoP1=true;
  private bool gotoP2=false;
  private bool waiting=false;
  private Vector3 targetPosition1,targetPosition2;
    // Start is called before the first frame update
    void Start()
    {
       targetPosition1=new Vector3(4.80f,transform.position.y, 17.0f);
       targetPosition2=new Vector3(3.0f,transform.position.y, 17.0f);
    }

    // Update is called once per frame
    void Update()
    {
      if(!calibration){
        transform.LookAt(player.transform);
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out shot)){
          target_dist=shot.distance;
          if(target_dist>=allowed_dist){
            speed=0.1f;
            transform.position =Vector3.MoveTowards(transform.position,player.transform.position,speed);
          }

          else{
            speed=0.0f;
          }
        }
      }
      else{
        if(gotoP1){
          inFrontOfHouse();
          if(transform.position.Equals(targetPosition1)){
            gotoP1=false;
            waiting=true;
          }
        }else{
          if(waiting){
            StartCoroutine(waitForAWhile());
          }else{
            if(gotoP2){
              EnterHouse();
              if(transform.position.Equals(targetPosition2)){
                gotoP2=false;
              }
            }
            else{
              StartCoroutine(disappear());
            }
          }
        }
      }
  }

  private void inFrontOfHouse(){
    transform.LookAt(p1);
     GetComponent<Animator>().Play("Walk");
     transform.position = Vector3.MoveTowards(transform.position, targetPosition1, 0.1f);
     GetComponent<Animator>().Play("Idle");

  }
  IEnumerator waitForAWhile(){
        yield return new WaitForSeconds(3f);
        waiting=false;
        gotoP2=true;
    }
    IEnumerator disappear(){
          yield return new WaitForSeconds(3f);
          transform.gameObject.SetActive(false);
      }
    public void EnterHouse(){
          transform.LookAt(p2);
          GetComponent<Animator>().Play("Walk");
            transform.position = Vector3.MoveTowards(transform.position, targetPosition2, 0.1f);
           GetComponent<Animator>().Play("Idle");

  }
}
