using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public Transform nero;
    private bool calibration=true;
    private Vector3 A = new Vector3( -4f,0, 18f );
    //Vector3 B = new Vector3( 4f,0,  18f );
    //Vector3 C = new Vector3( 7f,0,  -6.25f );
    private Vector3 D = new Vector3( 4f,0,  -6f );
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(calibration){
        Debug.Log("yes");
        Debug.Log(insideHouse());
        nero.gameObject.GetComponent<FollowOpie>().setCalibration(true);
      }else{
        nero.gameObject.GetComponent<FollowOpie>().setCalibration(false);
      }
    }
    public void setCalibration(bool t){
      calibration=t;
    }
    public bool insideHouse(){

      if(transform.position.x<D.x && transform.position.x>A.x && transform.position.z>D.z && transform.position.z<A.z ){
        return true;
      }
      return false;
    }
}
