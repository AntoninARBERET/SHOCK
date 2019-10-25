

using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {
	
	public float moveSpeed = 1f;
	public float turnSpeed = 10f;

	// The start method is called when the script is initalized, before other stuff in the scripts start happening.
	void Start () {
		//We have a reference called myAnimator but we need to fill that reference with an Animator component.
		//We can do that by 'getting' the animator that is on the same game object this script is appleid to.
	
	}
	
	// Update is called once per frame so this is a great place to listen for input from the player to see if they have
	//interacted with the game since the LAST frame (such as a button press or mouse click).
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)){
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
		if(Input.GetKey(KeyCode.DownArrow)){
			transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.Rotate(Vector3.up * - turnSpeed * Time.deltaTime);
        }
		if(Input.GetKey(KeyCode.RightArrow)){
			transform.Rotate(Vector3.up *  turnSpeed * Time.deltaTime);
        }
       
		
		}
		
		
	void OnCollisionEnter(Collision collision){
		 if (Input.GetMouseButtonDown(0)){
		 	Debug.Log("wesh");
        	if(collision.transform.name == "Door_2"){
    			collision.gameObject.GetComponent<Open>().openDoor();
    	}
        }

    }

	void OnGUI(){
		GUI.Label (new Rect(0, 0, 200, 25), "Forward: W");
        GUI.Label(new Rect(0, 25, 200, 25), "Backward: S");
        GUI.Label (new Rect(0, 50, 200, 25), "Strafe Left: A");
		GUI.Label (new Rect(0, 75, 200, 25), "Strafe Right: D");
		GUI.Label (new Rect(0, 100, 200, 25), "Turn Left: Q");
		GUI.Label (new Rect(0, 125, 200, 25), "Turn Right: E");
		GUI.Label (new Rect(0, 150, 200, 25), "Toggle Dance: 1");
		GUI.Label (new Rect(0, 175, 200, 25), "Toggle Kneeling: 2");
		GUI.Label (new Rect(0, 200, 200, 25), "Wave (Layer): 3");
	}
}

