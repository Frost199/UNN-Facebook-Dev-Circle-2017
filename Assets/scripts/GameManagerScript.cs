using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

	public static GameManagerScript instance;
	public Text scoreText;
	//public Text scoreText;

	public static int Score = 0;

	void Awake(){
		if(instance == null){
			instance = this;
		}
		else if(instance != this){
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		scoreText.text = GameManagerScript.Score.ToString();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void scoring(Text scoreText){
		Score++;
		scoreText.text = Score.ToString();
	}
}
