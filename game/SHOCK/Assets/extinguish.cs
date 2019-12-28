using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class extinguish : MonoBehaviour
{    private float timer  = 0.0f;
     public GameObject fire;

    void OnParticleCollision(GameObject other)
    {
      if(timer<10f){
        timer+= Time.deltaTime;
      }else{
        fire.SetActive(false);
      }
    }
}
