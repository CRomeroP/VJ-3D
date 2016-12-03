using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform player;

	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(0, player.position.y + 4, player.position.z-15);
        transform.rotation = player.rotation;
        transform.Rotate(0f, -player.eulerAngles.y, 0f);
	}
}
