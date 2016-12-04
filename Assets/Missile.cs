using UnityEngine;
using System.Collections;


public class Missile : MonoBehaviour {
    public float missileVelocity, maxHeight;
    private float currentY;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        currentY = transform.eulerAngles.y;
        Destroy(gameObject, 20f);
        Debug.Log("Hola en ...");
    }
	
    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = (GameObject)Instantiate(explosion, collision.transform.position, collision.transform.rotation);
        Debug.Log("Misil He colisionado en ...");
        Destroy(gameObject);
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
