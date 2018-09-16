using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscore : MonoBehaviour {

	public static highscore instance;
	public Text HighScore;

	int score = GameManagerScript.Score;

	void Awake(){
		if(instance == null){
			instance = this;
		}
		else if(instance != this){
			Destroy(gameObject);
		}
		Debug.Log( "This is d player:" + PlayerPrefs.GetInt("HighScore").ToString());
	}

	void Start(){
		HighScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
	}

	void Update(){
		if(score > PlayerPrefs.GetInt("HighScore", 0)){
			PlayerPrefs.SetInt("HighScore", score);
			HighScore.text = score.ToString();
		}
	}

	public void Reset(){
		if(Time.timeScale == 1){
			Time.timeScale = 0;
			PlayerPrefs.DeleteAll();
			HighScore.text = "0";
		}
	}
}
