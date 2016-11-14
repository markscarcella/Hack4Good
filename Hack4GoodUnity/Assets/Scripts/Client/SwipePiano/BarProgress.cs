using UnityEngine;
using System.Collections;

public class BarProgress : MonoBehaviour {

	float height;
	float startX;

	// Use this for initialization
	void Start () {
		height = Camera.main.orthographicSize * 2.0f;
		startX = Camera.main.orthographicSize * Screen.width / Screen.height;
		transform.localScale = new Vector2(transform.localScale.x, height);
		transform.position = new Vector2(-startX, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
