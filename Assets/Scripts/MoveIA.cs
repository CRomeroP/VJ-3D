using UnityEngine;
using System.Collections;

public class MoveIA : MonoBehaviour {

    private float currentHeight, currentAngle, rotationStep, currentTurnAngle, zRotation, currentYRotation, energyCooldown, explosionCooldown, rocketLaunchCooldown;
    public int currentRockets;
    public int maxRockets = 2;
    public float maxHeight;

    private ParticleSystem spark;

    private bool hasMissile;
    public Object missile;

    void OnCollisionEnter(Collision collision)
    {
        // TODO: pueden suceder diversas colisiones a la vez -> factoria de sparks?
        //       
        foreach (ContactPoint contact in collision.contacts)
        {
            // lo que se tiene q hacer
            // las chispas salen de la nave, no de la pared
            // en ese caso hay q mirar los puntos de colision y ver el angulo en el q estan respecto el centro de la nave
            // alejar del centro una determinada distancia (dependiendo del angulo la distancia no sera la misma (ej. derecha/izq, delante/atras)
            // con ese angulo orientar las chispas


            // reproducir sonido de choque
            if (collision.gameObject.tag != "misil")
            {
                Vector3 aux = contact.point;
                spark.transform.position = aux;
                if (energyCooldown <= 0f)
                {
                    //modifyEnergy(-2);
                    energyCooldown = 0.5f;

                }
                if (!spark.isPlaying) spark.Play();
            }
            else
            {

                if (explosionCooldown <= 0f)
                {
                   // modifyEnergy(-20);
                    explosionCooldown = 0.3f;
                }
            }
        }
        //Debug.Log("He colisionado en ...");
    }
    // Use this for initialization
    void Start () {
        currentHeight = maxHeight;
        currentAngle = 0f;
        currentTurnAngle = 0f;
        gameObject.transform.Rotate(0f, 0f, 0f);
        zRotation = 0;
        spark = gameObject.transform.Find("Sparks").GetComponent<ParticleSystem>();
        spark.Stop();
        currentYRotation = 0;
        hasMissile = true;
        energyCooldown = 0f;
        explosionCooldown = 0f;
        currentRockets = 0;
        rocketLaunchCooldown = 0f;
    }

    private void addRocket()
    {
        if (currentRockets < maxRockets) currentRockets += 1;
    }

    // Update is called once per frame
    void Update () {
	
	}

    void FixedUpdate()
    {

        // este esta en el centro de la nave

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        //float currentYRotation = gameObject.transform.rotation.eulerAngles.y;


        if (Physics.Raycast(ray, out hit, 1000f) && hit.transform.tag == "cicuito")// && hit.transform.tag != "baldosaEnergia" && hit.transform.tag != "baldosaCohete")
        {

            gameObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            gameObject.transform.position += transform.up * maxHeight;

            gameObject.transform.up = hit.normal;

            // importante el orden
            gameObject.transform.Rotate(0f, currentYRotation, 0f);
            gameObject.transform.Rotate(0f, 0f, zRotation);


            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        if (hit.transform.tag == "baldosaCohete")
        {
            if (hit.collider.transform.parent.GetComponent<BaldosaCoheteScript>().usar()) addRocket();
        }
    }
}
