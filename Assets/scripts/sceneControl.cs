using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneControl : MonoBehaviour {

	public GameObject facebookPanel;
	public GameObject facebookLoginMenus;
	public GameObject mainMenu;
	public GameObject optionsPanel;
	public GameObject optionButton;
	public Text scoreMenuText;

	void Start(){
		facebookPanel.SetActive(false);
		mainMenu.SetActive(true);
		mainMenu.GetComponent<Animator>().enabled = true;
		scoreMenuText.text = GameManagerScript.Score.ToString();
		optionsPanel.SetActive(false);
		facebookLoginMenus.SetActive(false);

	}
		
				

	public  void FacebookLinkButton(/*bool isLoggedIn*/){
		if(FB.IsLoggedIn){
			facebookLoginMenus.SetActive(true);
			mainMenu.SetActive(false);
		}else{
			facebookPanel.SetActive(true);
			mainMenu.SetActive(false);
		}

		/*if(isLoggedIn){
		facebookScript.instance.MenuManagerLoginState(isLoggedIn);
		mainMenu.SetActive(false);
		facebookLoginMenus.SetActive(true);
		}else{
			facebookLoginMenus.SetActive(false);
			facebookPanel.SetActive(true);
			mainMenu.SetActive(false);
		}*/
	}

	public void BackToMenu(){
		mainMenu.SetActive(true);
		facebookPanel.SetActive(false);
		}

	public void LoadScene(string SceneGivenName){
		SceneManager.LoadScene(SceneGivenName);
		mainMenu.SetActive(false);
		facebookPanel.SetActive(false);
		facebookLoginMenus.SetActive(false);
		Debug.Log("Play button is pressed");
		Time.timeScale = 1;
		GameManagerScript.Score = 0;
	}

	public void BackToMenuFacebook(){
		mainMenu.SetActive(true);
		facebookLoginMenus.SetActive(false);
	}

	public void CancelOption(){
		Time.timeScale  = 1;
		optionButton.SetActive(true);
		optionsPanel.SetActive(false);
	}

	public void OptionsPanelPoP(){
		Time.timeScale = 0;
		optionButton.SetActive(false);
		optionsPanel.SetActive(true);
	}
}
