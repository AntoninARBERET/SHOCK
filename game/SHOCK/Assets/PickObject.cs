using UnityEngine;
using System.Collections;
public class PickObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public Transform water;
    private bool hasPlayer = false;
    float target_dist;
    float speed;
    RaycastHit shot;
    private bool touched = false;
    private  float allowed_dist=1f;
    void Start(){

    }
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
          transform.position=playerCam.position;
          if (Input.GetKey(KeyCode.E))
          {
            water.gameObject.SetActive(true);
          }else{
            water.gameObject.SetActive(false);
          }
          if(Input.GetKey(KeyCode.U)){
            touched = false;
            transform.parent = null;
            hasPlayer=false;
          }
        }
    }
}
