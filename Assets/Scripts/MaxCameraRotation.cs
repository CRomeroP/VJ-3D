using UnityEngine;
using System.Collections;

public class MaxCameraRotation : MonoBehaviour {

    public GameObject player;

    void Start()
    {

    }


    void LateUpdate () {
        float currentZ = player.transform.rotation.eulerAngles.z;
        if (currentZ > 180f) currentZ -= 360;
        Debug.Log("rotacion Z " + currentZ);
        if (currentZ < -40f) currentZ = -40f;
        else if (currentZ > 40f) currentZ = 40f;
       
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentZ);
	}
}
