using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using Facebook.Unity;
using Facebook.MiniJSON;
using UnityEngine.UI;
using System.Linq;

public class facebookScript : MonoBehaviour {

	public static facebookScript instance;

	public GameObject fbCanvas;
	public GameObject fbLoginCanvas;
	public GameObject Username;
	public GameObject mainMenuProfileName;
	public GameObject profilePic;
	public GameObject profilepicMain;
	public GameObject mainMenuProfilePic;
	public string appLinkURL;

	string url;

	public void Awake ()
	{
		if (!FB.IsInitialized && instance == null) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
			instance = this;
		} else if(instance != this) {
			// Already initialized, signal an app activation App Event
			Destroy(gameObject);
			FB.ActivateApp();
		}
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
		MenuManagerLoginState(FB.IsLoggedIn);
	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	void Start(){
		fbLoginCanvas.SetActive(false);
		DontDestroyOnLoad(gameObject);
	}


	public void FacebookLogin(){
		List<string> perms = new List<string>();
		perms.Add("public_profile");
		FB.LogInWithReadPermissions(perms, AuthCallback);
		GameManagerScript.Score = 100;
	}

	private void AuthCallback (ILoginResult result) 
	{
		if(result.Error != null){
			Debug.Log(result.Error);
		}else{
			if(FB.IsLoggedIn){
				Debug.Log("Fb Logging Successful");
				GameManagerScript.Score = 100;
			}else{
				Debug.Log("Login canceled");
			}
			MenuManagerLoginState(FB.IsLoggedIn);
		}
	}

	public void MenuManagerLoginState(bool isLoggedIn){
		if(isLoggedIn){
			fbLoginCanvas.SetActive(true);
			fbCanvas.SetActive(false);


			FB.API("/me?fields=first_name", HttpMethod.GET, UsernameDisplay);
			FB.API("/me/picture?g&width=130&height=130&redirect=false", HttpMethod.GET, /*StartCoroutine(startDownload())*/ ProfilePicDisplay);
			FB.GetAppLink(AppLinkDesignation);

		}else{
			fbLoginCanvas.SetActive(false);
			fbCanvas.SetActive(true);
		}
	}
	void UsernameDisplay(IResult result){
		Text username = Username.GetComponent<Text>();
		Text menuUsername = mainMenuProfileName.GetComponent<Text>();

		if( result.Error == ""){
			username.text = "Welcome, " + result.ResultDictionary["first_name"];
			menuUsername.text = "Welcome, " + result.ResultDictionary["first_name"];
			mainMenuProfileName.SetActive(true);
			Debug.Log(result.ResultDictionary["first_name"]);
		}else{
			Debug.Log(result.Error);
		}
	}

	void ProfilePicDisplay(IGraphResult result){

		var dic = result.ResultDictionary["data"] as Dictionary<string, object>;
		url = dic.Where(i => i.Key=="url").First().Value as string;
		StartCoroutine(startDownload());
		mainMenuProfilePic.SetActive(true);
	}

	IEnumerator startDownload(){
		bool isstart = false;
		WWW www = new WWW(url);
		yield return www;
		isstart = true;
		if(isstart){
		var texture = www.texture;
		Debug.Log(texture);
		Image profilePicture = profilePic.GetComponent<Image>();
		profilePicture.sprite = Sprite.Create(texture, new Rect(0, 0,128,128), new Vector2());
		Image menuProfilePicture = mainMenuProfilePic.GetComponent<Image>();
		menuProfilePicture.sprite = Sprite.Create(texture, new Rect(0, 0,128,128), new Vector2());}
		else{
			profilePic.SetActive(false);
			profilepicMain.SetActive(false);
		}
	}

	void AppLinkDesignation(IAppLinkResult result){
		if(!string.IsNullOrEmpty(result.Url)){
			appLinkURL = "" + result.Url + "";
			Debug.Log(appLinkURL);
		}else{
			appLinkURL = "https://drive.google.com/folderview?id=0B8BLk3cYwCG2S0MxQnNFbXU1TGc";
		}
	}

	public void Share(){
		FB.FeedShare(
			string.Empty,
			new Uri(appLinkURL),
			"Facebook developers Circle UNN",
			"showing how to integrate Facebook to your games",
			"Check out the colour Switch clone",
			new Uri("https://drive.google.com/open?id=0B8BLk3cYwCG2c3lKU2xoNmhJajQ"),
			string.Empty,
			SharingCallback
		);
	}

	void SharingCallback(IResult result){
		if(result.Cancelled){
			Debug.Log("share cancled");
		}else if(!string.IsNullOrEmpty(result.Error)){
			Debug.Log("Error on sharing");
		}else if(!string.IsNullOrEmpty(result.RawResult)){
			Debug.Log("Successfully shared");
		}
	}

	public void Invite(){
		FB.Mobile.AppInvite(
			new Uri(appLinkURL),
			new Uri("https://drive.google.com/open?id=0B8BLk3cYwCG2c3lKU2xoNmhJajQ"),
			InvitingCallback
		);
	}

	void InvitingCallback(IResult result){
		if(result.Cancelled){
			Debug.Log("Invite cancled");
		}else if(!string.IsNullOrEmpty(result.Error)){
			Debug.Log("Error Inviting");
		}else if(!string.IsNullOrEmpty(result.RawResult)){
			Debug.Log("Successfully Invited");
		}
	}

	public void ShareWithUsers(){

		FB.AppRequest(
			"Come join me, feel free to beat my score LOL!",
			null,
			new List<object>(){"app_users"},
			null,
			null,
			null,
			null,
			SharingWithUsersCallback
		);
	}

	public void SharingWithUsersCallback(IAppRequestResult result){
		Debug.Log(result.RawResult);
	}
}
