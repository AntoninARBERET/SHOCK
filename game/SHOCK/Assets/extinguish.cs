using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class extinguish : MonoBehaviour
{    private float timer  = 0.0f;
     public GameObject fire;
     public Transform chair;
     public Transform opie;
     public GameObject bird_bath;
     public GameObject carpet;
    void OnParticleCollision(GameObject other)
    {
      if(timer<10f){
        timer+= Time.deltaTime;
      }else{
        fire.SetActive(false);
        chair.gameObject.GetComponent<changeMaterial>().setMaterial();
        bird_bath.transform.gameObject.SetActive(true);
        carpet.transform.gameObject.SetActive(false);
        opie.gameObject.GetComponent<startGame>().setLevel(3);
      }
    }
}
