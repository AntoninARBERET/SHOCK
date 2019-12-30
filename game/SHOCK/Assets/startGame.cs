using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public Transform nero;
    public Transform wall_in;
    public Transform wall_out;
    public Transform P3;
    public Transform P4;
    public Transform P5;
    public Transform P6;
    private bool calibration=false; //calibration is true when it has been calibrated
    private Vector3 A = new Vector3( -4f,0, 18f );
    //Vector3 B = new Vector3( 4f,0,  18f );
    //Vector3 C = new Vector3( 7f,0,  -6.25f );
    private Vector3 D = new Vector3( 4f,0,  -6f );
    public GameObject obj;
    public GameObject objD;
    public GameObject fire;
    private GameObject dest;
    private bool stressed=false;
    private float timer  = 0.0f;
    private int level= 0; // if calibration is in progress ==> level = 0
    void Start()
    {

    }

    void Update()
    {//when the calibration is done, the variable calibration is set to true and we start the game!!
      if(calibration){
        nero.gameObject.GetComponent<FollowOpie>().setCalibration(true);
        if(insideHouse()){
          switch(level){
            //Opie is in the house and the bird bath object is destruced when opie is near from it, opie is following nero, nero flees and disappears
            case 1:
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
            //the stove catches fire, opie should find a fire extinguisher and extinguishes the fire
            //the next level is available when the fire is extinguished
            case 2:
                fire.SetActive(true);
                Destroy(dest,1f);
                dest.transform.gameObject.SetActive(false);
                level =-1;
                break;

            case 3:
                //even if the user is stressed or not, this level serves only to push the user getting the first floor, by appearing nero going upstairs
                //the next level is available when opies gets to the next floor
                if(transform.position.z<11.33f && transform.position.y<1f && transform.position.x<-1.9f){
                  nero.gameObject.GetComponent<appear>().appearing(P4.position);
                  StartCoroutine(waitForAWhile());
                  nero.gameObject.GetComponent<disappear>().setNextLevel(4);
                  nero.gameObject.GetComponent<moveTo>().setTarget(P5);
                  nero.gameObject.GetComponent<moveTo>().setMove(true);
                  level =-1;
                }
                break;
              case 4:
                //if the user is not stressed, we ++ / -- some walls
                //the next level is availavle when the player is streesed or bored
                if(!stressed){
                  if(transform.position.y>1f && transform.position.x>3.02f && transform.position.z>-1.32f){
                    wall_in.transform.gameObject.SetActive(true);
                    timer+= Time.deltaTime;
                  }
                  if(transform.position.y>1f && transform.position.x>-0.12f && transform.position.x<1.19f && transform.position.z>-1.08f && transform.position.z<0.57f){
                      wall_in.transform.gameObject.SetActive(false);
                  }
                  //if the user is never stressed, it may mean he is bored, so we permit him accessing the level 5
                  if(timer>20f){
                    level=5;
                  }
                //if the user is too stressed we stop  ++ / -- walls and finally open the wall to get to the next room (level 5)
                }else{
                  level=5;
                }
                  break;

          //The end, nero follows Opie
          case 5:
            wall_in.transform.gameObject.SetActive(false);
            wall_out.transform.gameObject.SetActive(false);
            //nero appears and follows opie getting outside the house
            nero.gameObject.GetComponent<appear>().appearing(P6.position);
            nero.gameObject.GetComponent<GetOutHouse>().setOutside(true);
            level=6;
          break;

          //getting out of the house
          case 6:
            if(!insideHouse()){
              //Win
              Debug.Log("You win !!");
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
    public bool isStressed(){
      return streesed;
    }
}
