using UnityEngine;
using System.Collections;

public class MoveShip : MonoBehaviour {
	
	public float speed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKey(KeyCode.RightArrow))
			gameObject.transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
		if(Input.GetKey(KeyCode.LeftArrow))
			gameObject.transform.Translate(-speed * Time.deltaTime, 0.0f, 0.0f);
		if(Input.GetKey(KeyCode.UpArrow))
			gameObject.transform.Translate(0.0f, 0.0f,speed * Time.deltaTime);

	
	}
}
