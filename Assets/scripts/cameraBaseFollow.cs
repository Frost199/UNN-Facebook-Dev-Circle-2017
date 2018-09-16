using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBaseFollow : MonoBehaviour {

	public Camera cam;
	public float camHeight;
	private BoxCollider2D col;

	// Use this for initialization
	void Start () {
		col = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		camHeight = cam.transform.position.y - Camera.main.orthographicSize ;
		this.transform.position = new Vector2(0,camHeight);
		col.transform.position = new Vector2(0,camHeight);
	}
}
