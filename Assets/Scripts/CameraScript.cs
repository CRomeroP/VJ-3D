using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

	// Update is called once per frame

    void Start()
    {
        offset = transform.position - player.transform.position;

    }
	void LateUpdate ()
    {
        
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform.position);

    }
}
