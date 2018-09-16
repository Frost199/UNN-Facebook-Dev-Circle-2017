using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {


	#region public variables
	public float jumpForce = 10f;
	public Text scoreText;
	public GameObject ground;
	public string currentColour;
	public Color blue;
	public Color yellow;
	public Color pink;
	public Color purple;
	public GameObject colourChanger;
	public GameObject[] obstacles;
	public Transform[] spawnPoints;
	public AudioClip scoredClip;
	public AudioClip ColourChangeClip;
	public AudioClip DeathClip;
	#endregion public variables

	#region private Variables
	private Rigidbody2D rb;
	private SpriteRenderer sr;
	#endregion private Variables


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		SetRandomColour();
		StartSpawnObstacle();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)){
			this.rb.velocity = Vector2.up * jumpForce;
			ground.SetActive(true);
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		int obstacleIndex = Random.Range(0, obstacles.Length);

		if(other.gameObject.tag == "Scored"){
			GameManagerScript.instance.scoring(scoreText);
			other.gameObject.SetActive(false);
			Instantiate(obstacles[obstacleIndex], new Vector2(transform.position.x, Mathf.Floor(transform.position.y + 16.62f)), obstacles[obstacleIndex].transform.rotation);
			AudioSource.PlayClipAtPoint(scoredClip, transform.position, 2f);
			return;
		}

		if(other.gameObject.tag == "ColourChanger"){
			SetRandomColour();
			Destroy(other.gameObject);
			Instantiate(colourChanger, new Vector2(transform.position.x, Mathf.Floor(transform.position.y + 8.62f)), transform.rotation);
			AudioSource.PlayClipAtPoint(ColourChangeClip, transform.position, 2f);
			return;
		}

		if(other.gameObject.tag != currentColour){
			Debug.Log("You died!!!");
			AudioSource.PlayClipAtPoint(DeathClip, transform.position, 2f);
			//Destroy(gameObject);
			SceneManager.LoadScene("menu");
		}

	}

	void SetRandomColour(){
		int rand = Random.Range(0, 4);

		switch(rand){
		case 0:
			currentColour = "Blue";
			sr.color = blue;
			break;
		case 1:
			currentColour = "Yellow";
			sr.color = yellow;
			break;
		case 2:
			currentColour = "Pink";
			sr.color = pink;
			break;
		case 3:
			currentColour = "Purple";
			sr.color = purple;
			break;
		}
	}

	public void StartSpawnObstacle(){

		int obstacleIndex = Random.Range(0, obstacles.Length);
		//int spawnIndex = Random.Range(0, spawnPoints.Length);

		Instantiate(obstacles[obstacleIndex], spawnPoints[0].position, spawnPoints[0].rotation);
		Instantiate(obstacles[obstacleIndex], spawnPoints[1].position, spawnPoints[1].rotation);
		Instantiate(colourChanger, colourChanger.transform.position, colourChanger.transform.rotation);
	}
		
}
