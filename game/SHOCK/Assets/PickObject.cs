using UnityEngine;
using System.Collections;

public class PickObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float throwForce = 10;
    public Transform water;
    private bool hasPlayer = false;
    float target_dist;
    float speed;
    RaycastHit shot;
    private bool touched = false;
    private  float allowed_dist=1f;
    void Update()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist<2f)
        {
            touched = true;
        }
        else
        {
            touched = false;
        }
        if (touched && Input.GetKey(KeyCode.P))
        {
          hasPlayer=true;
        }
        if(hasPlayer){
          transform.parent = playerCam;
          if (Input.GetKeyDown(KeyCode.E))
          { water.gameObject.SetActive(true);
          }
          else if (Input.GetKeyUp(KeyCode.E))
          { water.gameObject.SetActive(false);
          }
          if(Input.GetKey(KeyCode.U)){
            touched = false;
            transform.parent = null;
            hasPlayer=false;
          }
        }
    }
}
