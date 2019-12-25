using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public Transform nero;
    private bool calibration=true; //calibration is true when it has been calibrated
    private Vector3 A = new Vector3( -4f,0, 18f );
    //Vector3 B = new Vector3( 4f,0,  18f );
    //Vector3 C = new Vector3( 7f,0,  -6.25f );
    private Vector3 D = new Vector3( 4f,0,  -6f );
    public GameObject obj;
    private int level= 1; // if calibration is in progress ==> level = 0
    void Start()
    {

    }

    void Update()
    {//when the calibration is done, we start the game!!
      if(calibration){
        nero.gameObject.GetComponent<FollowOpie>().setCalibration(true);
        if(insideHouse()){
          switch(level){
          case 1:
              //Level 1 :  object destruction
              Console.WriteLine("Level 1");
              RaycastHit _hit = new RaycastHit();
              if(Physics.Raycast(transform.position, transform.forward, out _hit,1))
              {
                  if(_hit.transform.tag == "0bj1")
                 {
                      GameObject dest=Instantiate(obj,new Vector3(-1.3f,0.5f,7f), transform.rotation) as GameObject;
                      Console.WriteLine("HIT");
                 }
             }

              break;
          case 2:
              //Console.WriteLine("Level 2");
              break;
          default:
              //Console.WriteLine("Default case");
              break;
        }
        }
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
