using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowOpie : MonoBehaviour
{
  public GameObject player;
  float target_dist;
  public float allowed_dist=0.75f;
  float speed;
  RaycastHit shot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
