using UnityEngine;
using System.Collections;

public class CubeColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.GetComponent<Renderer>().material.color = Color.black;
	}
}
