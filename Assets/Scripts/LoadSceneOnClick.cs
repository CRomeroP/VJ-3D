using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	// Use this for initialization

    public void LoadByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
