using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public Transform nero;
    public Transform P3;
    public Transform P4;
    public Transform P5;
    public Transform P6;
    public Transform P7;
    private bool calibration=true; //calibration is true when it has been calibrated
    private Vector3 A = new Vector3( -4f,0, 18f );
    //Vector3 B = new Vector3( 4f,0,  18f );
    //Vector3 C = new Vector3( 7f,0,  -6.25f );
    private Vector3 D = new Vector3( 4f,0,  -6f );
    public GameObject obj;
    public GameObject objD;
    public GameObject fire;
    private GameObject dest;
    private int level= 4; // if calibration is in progress ==> level = 0
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
              //Debug.Log("Level 1");
             if(transform.position.z<12f && transform.position.y<1f && transform.position.x>-1.5f){
               //Destroy(obj,1f);
    	         obj.transform.gameObject.SetActive(false);
               dest=Instantiate(objD,new Vector3(-1.197861f,0.5023094f,7f), transform.rotation) as GameObject;
               nero.gameObject.GetComponent<appear>().appearing(new Vector3(-0.36f,0.104f,9.746f));
               StartCoroutine(waitForAWhile());
               nero.gameObject.GetComponent<disappear>().setNextLevel(2);
               nero.gameObject.GetComponent<moveTo>().setTarget(P3);
               nero.gameObject.GetComponent<moveTo>().setMove(true);
               level=-1;
             }
              break;
          case 2:
              fire.SetActive(true);
              Destroy(dest,1f);
              dest.transform.gameObject.SetActive(false);
              level =-1;
              break;
            case 3:
                if(transform.position.z<11.33f && transform.position.y<1f && transform.position.x<-1.9f){
                  Debug.Log("DETECTION");
                  nero.gameObject.GetComponent<appear>().appearing(P4.position);
                  StartCoroutine(waitForAWhile());
                  nero.gameObject.GetComponent<disappear>().setNextLevel(4);
                  nero.gameObject.GetComponent<moveTo>().setTarget(P5);
                  nero.gameObject.GetComponent<moveTo>().setMove(true);
                  level =-1;
                }
                break;
              case 4:
                if(transform.position.y>5f && transform.position.x>P6.position.x && transform.position.z==P7.position.z){
                  transform.position=P6.position;
                }
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
    IEnumerator waitForAWhile(){
          yield return new WaitForSeconds(1f);

      }
    public bool insideHouse(){

      if(transform.position.x<D.x && transform.position.x>A.x && transform.position.z>D.z && transform.position.z<A.z ){
        return true;
      }
      return false;
    }
    public void setLevel(int l){
      level=l;
    }
}
