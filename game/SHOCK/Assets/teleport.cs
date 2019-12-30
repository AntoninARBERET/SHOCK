using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform p;
    public bool first;
    private bool init;
    public Transform opie;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
      if(first && init){
        init=false;
      }
      else{
        if(first){
          opie.position=new Vector3(p.position.x+);
        }else{
        }
      }

    }
}
