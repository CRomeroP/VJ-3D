using UnityEngine;
using System.Collections;

public class MoveShip : MonoBehaviour {
	
    public float turnSpeed = 5.0f;
    public float maxHeight;
    public float verticalRotSpeed = 1.0f;
    public float turnRotSpeed = 50.0f;
    public float engineForceStep = 5f;
    public float maxZrotation, stepZRotation;
    public float airResistance, mass, frictionCoeff;
    public float speed, engineForce, maxSpeed;
    private float currentHeight, currentAngle, rotationStep, currentTurnAngle, zRotation;

    private bool subir, subirMorro;
    private ParticleSystem spark;


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
            Vector3 aux = contact.point;
            spark.transform.position = aux;
            if (!spark.isPlaying) spark.Play();
        }
        Debug.Log("He colisionado en ...");
    }
        // Use this for initialization
    void Start () {
        currentHeight = maxHeight;
        subir = false;
        subirMorro = false;
        currentAngle = 0f;
        currentTurnAngle = 0f;
        gameObject.transform.Rotate(0f, 0f, 0f);
        speed =  0;
        engineForce = 0;
        zRotation = 0;
        spark = gameObject.transform.Find("Sparks").GetComponent<ParticleSystem>();
        spark.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // fuerzas de friccion del motor (afectan a la fuerza del motor)
        float engineFrictionForce = frictionCoeff*speed; 
        if (engineForce > 0)
        {
            engineForce -= engineFrictionForce * Time.deltaTime;
            if (engineForce < 0) engineForce = 0;
        }
        else if (engineForce < 0)
        {
            engineForce += engineFrictionForce * Time.deltaTime;
            if (engineForce > 0) engineForce = 0;
        }

        // fuerzas de friccion del aire (afectan a la velocidad de la nave)
        if (speed > maxSpeed) speed = maxSpeed;
        float accelerationResistance = airResistance * speed * (speed/2) / mass;
        float accelerationEngine = engineForce / mass;

        float acceleration = accelerationEngine;
        if (acceleration > 0) acceleration -= accelerationResistance;
        else if (acceleration < 0) acceleration += accelerationResistance;

        speed = Time.deltaTime * (acceleration - accelerationResistance) + speed;


        // giro
        float turnStep = turnRotSpeed * Time.deltaTime;
        float zStep = stepZRotation * Time.deltaTime;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            
  
            
            zRotation -= stepZRotation;
            if (zRotation < -maxZrotation) zRotation = -maxZrotation;
            if (zRotation > -maxZrotation)
                transform.Rotate(0f, 0f, -stepZRotation);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            

            
            zRotation += stepZRotation;
            if (zRotation > maxZrotation) zRotation = maxZrotation;
            if (zRotation < maxZrotation)
                transform.Rotate(0f, 0f, stepZRotation);
        }



        // aceleracion
        if (Input.GetKey(KeyCode.UpArrow))
        {
            engineForce += engineForceStep*Time.deltaTime;
           
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // float nextSpeed = Mathf.Abs(engineForce - engineForceStep) / mass;
            if (speed > 100) engineForce -= engineForceStep * Time.deltaTime;
        }


        if (speed < 0) { speed = 0;  engineForce = 0; }
        

        gameObject.transform.position += transform.forward*speed * Time.deltaTime;
        // TODO: cambiar pitch de ruido motor segun las revoluciones de este
 
        

    }

    void FixedUpdate()
    {

        // este esta en el centro de la nave

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        float currentYRotation = gameObject.transform.rotation.eulerAngles.y;
        
        if (Physics.Raycast(ray, out hit, 1000f))
        {
         
            gameObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            gameObject.transform.position += transform.up * maxHeight;
            gameObject.transform.rotation = Quaternion.Euler(hit.transform.rotation.eulerAngles.x, currentYRotation, hit.transform.rotation.eulerAngles.z);
            gameObject.transform.Rotate(0f, 0f, zRotation);
            gameObject.transform.RotateAround(hit.transform.up, -Time.deltaTime * Time.deltaTime * zRotation);
            
            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        

        // morro nave
        /*
        Vector3 posNave = new Vector3(0f, 0f, 8f);
        Quaternion rotation = Quaternion.Euler(0f, currentYRotation, zRotation);
        posNave = rotation * posNave;
        posNave += transform.position;
        Ray rayHead = new Ray(posNave, -transform.up);
        RaycastHit hitMorro;
        */
        /*
        if (Physics.Raycast(rayHead, out hitMorro, maxHeight))
        {
            Debug.DrawLine(rayHead.origin, hitMorro.point, Color.red);

            // corrijo la actual rotacion respecto el eje Y

            Vector3 rotationAux = hitMorro.transform.rotation.eulerAngles;
            rotationAux.y = currentYRotation;
            transform.rotation = Quaternion.Euler(rotationAux);

        }
        */
    }
}
