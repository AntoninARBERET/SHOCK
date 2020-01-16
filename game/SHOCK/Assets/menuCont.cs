using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuCont : MonoBehaviour
{
	public void ButtonStart(){
		Debug.Log("rrrrrrrr");
		SceneManager.LoadScene(0);

	}
	public void ButtonStats(){
		SceneManager.LoadScene(2);
	}
    public void ButtonExit(){
		Application.Quit();
	}

}
