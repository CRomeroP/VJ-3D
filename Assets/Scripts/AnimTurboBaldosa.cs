using UnityEngine;
using System.Collections;

public class AnimTurboBaldosa : MonoBehaviour {

    // Use this for initialization
    private float currentYRotation;
    void Start()
    {
        currentYRotation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        currentYRotation += 180 * Time.deltaTime;
        if (currentYRotation >= 360f) currentYRotation -= 360f;
        transform.localEulerAngles = new Vector3(-54.15f, currentYRotation, 0f);
    }
}
