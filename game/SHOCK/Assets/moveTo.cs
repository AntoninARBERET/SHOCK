using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTo : MonoBehaviour
{   private bool move=false;
    // Start is called before the first frame update
    private Transform target=null;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(target!=null && move==true){
        if(transform.position.Equals(target.position)){
          move=false;
          target=null;
          GetComponent<disappear>().disappearing();
        }
        else{
          transform.LookAt(target);
          GetComponent<Animator>().Play("Walk");
          transform.position = Vector3.MoveTowards(transform.position, target.position, 0.25f);
          GetComponent<Animator>().Play("Idle");
        }

      }
    }

    public void setTarget(Transform p){
      target=p;
    }
    public void setMove(bool v){
      move=v;
    }
}
