using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishFloor : MonoBehaviour
{
    [SerializeField] GameObject statMenu;
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
     if(collision.gameObject.tag=="Player"){
       statMenu.SetActive(true);
       statMenu.transform.Find("Back").gameObject.SetActive(false);
       statMenu.transform.Find("WinDisplays").gameObject.SetActive(true);

     }
   }
}
