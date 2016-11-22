using UnityEngine;
using System.Collections;

public class MoveShip : MonoBehaviour {
	
    public float turnSpeed = 5.0f;
    public float maxHeight;
    public float verticalRotSpeed = 1.0f;
    public float turnRotSpeed = 50.0f;
    public float engineForceStep = 5f;
    public float airResistance, mass, frictionCoeff;
    public float speed, engineForce, maxSpeed;
    private float currentHeight, currentAngle, rotationStep, currentTurnAngle;

    private bool subir, subirMorro;


    // Use this for initialization
    void Start () {
        currentHeight = maxHeight;
        subir = false;
        subirMorro = false;
        currentAngle = 0f;
        currentTurnAngle = 0f;
        gameObject.transform.Rotate(-currentAngle, 0f, 0f);
        speed =  0;
        engineForce = 0;
        


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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentTurnAngle += turnStep;
            transform.Rotate(0f, turnStep, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentTurnAngle -= turnStep;
            transform.Rotate(0f, -turnStep, 0f);
        }

        if (currentAngle >= 360f) currentAngle -= 360f;

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
        gameObject.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);




        if (subir) gameObject.transform.Translate(0f, +0.03f, 0f);
        else gameObject.transform.Translate(0f, -0.03f, 0f);

        
                
    }

    void FixedUpdate()
    {
        


        // este esta en el centro de la nave
        Ray ray = new Ray(transform.position, -transform.up);       
        RaycastHit hit;
        subir = Physics.Raycast(ray, out hit, maxHeight);
        Debug.DrawLine(ray.origin, hit.point);

        // morro nave
        Vector3 posNave = new Vector3(0f, 0f, 8f);
        Quaternion rotation = Quaternion.Euler(0f, currentTurnAngle, 0f);
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
        
    }
}
