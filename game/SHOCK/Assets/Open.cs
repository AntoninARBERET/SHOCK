using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{	public Transform player;
	public bool openDoor=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        open();
    }
    public void open(){
    	if( openDoor==false){	
        	getClosestPlayer(player);
        	openDoor=true;
        }
    }
    public void getClosestPlayer (Transform player){
        Vector3 currentPosition = transform.position;
        Vector3 directionToTarget = player.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < 3)
            {if (Input.GetMouseButtonDown(0)){
                GetComponent<Animator>().enabled = true;
    			Debug.Log("jr");
            }
        }
             
    }
}
