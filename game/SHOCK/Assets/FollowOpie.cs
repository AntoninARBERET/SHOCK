using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOpie : MonoBehaviour
{
  public GameObject player;
  float target_dist;
  float allowed_dist=1f;
  float speed;
  RaycastHit shot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
      transform.LookAt(player.transform);
      if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out shot)){
        target_dist=shot.distance;
        if(target_dist>=allowed_dist){
          speed=0.25f;
          transform.position =Vector3.MoveTowards(transform.position,player.transform.position,speed);
        }
      }
  }
}
