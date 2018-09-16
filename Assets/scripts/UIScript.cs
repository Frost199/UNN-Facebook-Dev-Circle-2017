using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {

	public GameObject pausePanel;
	public GameObject pauseButton;
	public GameObject gameMenu;
	public Rigidbody2D rb;

	void Start () {
		pausePanel.SetActive(false);
	}

	public void Pause(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
			pausePanel.SetActive(true);
			pauseButton.SetActive(false);
			rb.velocity = Vector2.zero;
			Time.timeScale = 0;
		}
	}

	public void Resume() {
		if(Time.timeScale == 0){
			Time.timeScale = 1;
			pausePanel.SetActive(false);
			pauseButton.SetActive(true);
		}
	}
		
	public void Home(string SceneGivenName){
			SceneManager.LoadScene(SceneGivenName);
		facebookScript.instance.Awake();
			Debug.Log("Play button is pressed");
		Time.timeScale = 1;
	}
}
