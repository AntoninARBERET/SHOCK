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
     public float limit_fire=15f;
    void OnParticleCollision(GameObject other)
    {
      if(timer<limit_fires){
        timer+= Time.deltaTime;
      }
      //If the player is too stressed or if the player extinguishes the fire
      if(opie.gameObject.GetComponent<startGame>().isStressed() || timer>=limit_fires){
        fire.SetActive(false);
        //change the color  of the chair
        chair.gameObject.GetComponent<changeMaterial>().setMaterial();
        //the bird bath destructed becomes intact again
        bird_bath.transform.gameObject.SetActive(true);
        //the carpet disappears
        carpet.transform.gameObject.SetActive(false);
        opie.gameObject.GetComponent<startGame>().setLevel(3);
      }
    }
}
