using UnityEngine;
using System.Collections;


public class Missile : MonoBehaviour {
    public float missileVelocity, maxHeight;
    private float currentY;
	// Use this for initialization
	void Start () {
        currentY = transform.eulerAngles.y;
        Destroy(gameObject, 5f);
	}
	
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("He colisionado en ...");
    }
    void Update()
    {
        gameObject.transform.position += transform.forward * missileVelocity * Time.deltaTime;
    }

    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f))
        {

            gameObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            gameObject.transform.position += transform.up * maxHeight;

           

            gameObject.transform.up = hit.normal;
            gameObject.transform.Rotate(0f, currentY, 0f);

            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
    }
}
