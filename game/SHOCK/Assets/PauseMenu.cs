using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused=false;
	public GameObject pauseMenuUI;
	public GameObject statsMenu;
	public GameObject camera;
	private double lastSwitch=-1;
	[SerializeField] private WindowGraph wg;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape) && Time.realtimeSinceStartup-lastSwitch>1){
        	lastSwitch=Time.realtimeSinceStartup;

           if(GameIsPaused){
           	Resume();
           }
           else{
           	Pause();
           }
       }

    }
    public void Resume(){
        pauseMenuUI.SetActive(false);
        statsMenu.SetActive(false);
        Time.timeScale=1f;
    	GameIsPaused=false;
    	camera.gameObject.GetComponent<ThirdPersonOrbitCamBasic>().enabled = true;
    }
    public void Pause(){
    	pauseMenuUI.SetActive(true);
    	Time.timeScale=0f;
    	GameIsPaused=true;
    	camera.gameObject.GetComponent<ThirdPersonOrbitCamBasic>().enabled = false;
    }
    public void LoadMenu(){
    	SceneManager.LoadScene(1);
    }
    public void Stats(){
			wg.refresh();
			pauseMenuUI.SetActive(false);
    	statsMenu.SetActive(true);
    	Time.timeScale=0f;
    	GameIsPaused=true;
    	camera.gameObject.GetComponent<ThirdPersonOrbitCamBasic>().enabled = false;
    }

		public void QuitStats(){
			pauseMenuUI.SetActive(true);
    	statsMenu.SetActive(false);
    	Time.timeScale=0f;
    	GameIsPaused=true;
    	camera.gameObject.GetComponent<ThirdPersonOrbitCamBasic>().enabled = false;
    }
    public void QuitGame(){
    	Application.Quit();
    }

}
