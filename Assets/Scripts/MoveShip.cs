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




    // Use this for initialization
    void Start () {
        currentHeight = maxHeight;
        subir = false;
        subirMorro = false;
        currentAngle = 0f;
        currentTurnAngle = 0f;
        //gameObject.transform.Rotate(0f, 0f, 20f);
        speed =  0;
        engineForce = 0;
        zRotation = 0;
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
        

        
        Debug.Log("angulo y " + gameObject.transform.rotation.y);
        gameObject.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
 
        //gameObject.transform.RotateAround(Vector3.up, Time.deltaTime * zRotation / 10);


        /*
        if (subir) gameObject.transform.Translate(0f, +0.03f, 0f);
        else gameObject.transform.Translate(0f, -0.03f, 0f);
        */


    }

    void FixedUpdate()
    {

        // este esta en el centro de la nave

        Ray ray = new Ray(transform.position, -transform.up);       
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f))
        {
            float currentYRotation = gameObject.transform.rotation.eulerAngles.y;
            gameObject.transform.position =  new Vector3(gameObject.transform.position.x, hit.transform.position.y + maxHeight, gameObject.transform.position.z);

            gameObject.transform.rotation = Quaternion.Euler(hit.transform.rotation.x, currentYRotation, hit.transform.rotation.z);
            gameObject.transform.Rotate(0f, 0f, zRotation);
            gameObject.transform.RotateAround(hit.transform.up, -Time.deltaTime * Time.deltaTime * zRotation);

            Debug.DrawLine(ray.origin, hit.point);
        }
        

        // morro nave
        /*
        Vector3 posNave = new Vector3(0f, 0f, 8f);
        Quaternion rotation = Quaternion.Euler(0f, currentTurnAngle, zRotation);
        posNave = rotation*posNave;
        posNave += transform.position;
        Ray rayHead = new Ray(posNave, -transform.up);
        RaycastHit hitMorro;

        subirMorro = Physics.Raycast(rayHead, out hitMorro,maxHeight);
        Debug.DrawLine(rayHead.origin, hitMorro.point, Color.red);
        
        // corrijo la actual rotacion respecto el eje Y
        
        Vector3 rotationAux = hitMorro.transform.rotation.eulerAngles;
        rotationAux.y = currentTurnAngle;
        transform.rotation = Quaternion.Euler(rotationAux);
        */
    }
}
