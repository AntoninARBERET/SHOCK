using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutHouse : MonoBehaviour
{
  public GameObject player;
  private bool outside=false;
  private float target_dist;
  private float allowed_dist=1f;
  private float speed;
  private RaycastHit shot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(outside){
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
    }
    public void setOutside(bool b){
      outside=true;
    }
}
