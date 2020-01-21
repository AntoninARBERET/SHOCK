using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptativeDoor : MonoBehaviour
{
    // Start is called before the first frame update
    private bool openable, open, opening, closing, contactPlayer;
    private float speed = 100f, totalAngle;
    void Start()
    {
      openable = true;
      opening = false;
      open = false;
      totalAngle=0;
    }

    // Update is called once per frame
    void Update()
    {
      /*if(opening){
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
        UnityEngine.Debug.Log("Rotate " +  speed * Time.deltaTime + " " +Vector3.up);
        transform.rotation = Quaternion.Euler(0, -1, 0);
        if(totalAngle>=90){
          opening=false;
          open=true;
        }
      }*/
    }

    void OnCollisionEnter(Collision collision)
   {
     if(collision.gameObject.tag=="Player"){
       contactPlayer = true;
       if(openable && !open){
         //opening = true;
         openDoor();
       }
     }
   }

   void OnCollisionExit(Collision collision)
  {
    if(collision.gameObject.tag=="Player"){
      contactPlayer = false;
    }
  }

  public void openDoor(){
    gameObject.SetActive(false);
    open=true;
  }

  public void closeDoor(){
    gameObject.SetActive(true);
    open=false;
  }

  public bool getOpen(){
    return open;
  }
  public void setOpenable(bool b){
    openable = b;
  }

}
