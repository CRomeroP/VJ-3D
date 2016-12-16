using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MoveShip : MonoBehaviour
{

    public float turnSpeed = 5.0f;
    public float maxHeight;
    public float verticalRotSpeed = 1.0f;
    public float turnRotSpeed = 50.0f;
    public float engineForceStep = 5f;
    public float maxZrotation, stepZRotation;
    public float airResistance, mass, frictionCoeff;
    public float speed, engineForce, maxSpeed, energy;
    private float currentHeight, currentAngle, rotationStep, currentTurnAngle, zRotation, currentYRotation, energyCooldown, explosionCooldown, rocketLaunchCooldown;
    public int currentRockets;
    public int maxRockets = 2;
    public int laps, checkPointsPerLap;
    private float timeTurbo;

    private bool subir, subirMorro;
    private ParticleSystem spark;

    private bool hasMissile;
    public Object missile;

    private float checkCooldown;
    // factor: + - cantidad.
    private int currentChecks;

    public int currentLap()
    {
        return currentChecks / checkPointsPerLap;
    }

    private void endGame()
    {
        SceneManager.LoadScene(2);
    }

    private void partidaAcabada()
    {
        SceneManager.LoadScene(2);
    }
    private void modifyEnergy(int factor)
    {
        energy += factor;
        if (energy > 100f) energy = 100f;
        else if (energy <= 0f) endGame();

    }
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
            if (collision.gameObject.tag == "muro")
            {
                Vector3 aux = contact.point;
                spark.transform.position = aux;
                if (energyCooldown <= 0f)
                {
                    modifyEnergy(-2);
                    energyCooldown = 0.5f;

                }
                if (!spark.isPlaying) spark.Play();
            }
            else if (collision.gameObject.tag == "rocket")
            {

                if (explosionCooldown <= 0f)
                {
                    modifyEnergy(-20);
                    engineForce = 0;
                    speed = 0;
                    explosionCooldown = 0.3f;
                }
            }
        }
        //Debug.Log("He colisionado en ...");
    }
    // Use this for initialization
    void Start()
    {
        currentHeight = maxHeight;
        subir = false;
        subirMorro = false;
        currentAngle = 0f;
        currentTurnAngle = 0f;
        gameObject.transform.Rotate(0f, 0f, 0f);
        speed = 0;
        engineForce = 0;
        zRotation = 0;
        spark = gameObject.transform.Find("Sparks").GetComponent<ParticleSystem>();
        spark.Stop();
        currentYRotation = 0;
        hasMissile = true;
        energy = 100f;
        energyCooldown = 0f;
        explosionCooldown = 0f;
        currentRockets = 0;
        rocketLaunchCooldown = 0f;
        currentChecks = 0;
        checkCooldown = 0f;
        timeTurbo = 0;
    }

    private void addRocket()
    {
        if (currentRockets < maxRockets) currentRockets += 1;
    }

    void Update()
    {
        energyCooldown -= Time.deltaTime;
        explosionCooldown -= Time.deltaTime;
        rocketLaunchCooldown -= Time.deltaTime;
        checkCooldown -= Time.deltaTime;

        if (currentChecks / checkPointsPerLap > laps) partidaAcabada();
        // fuerzas de friccion del motor (afectan a la fuerza del motor)
        float engineFrictionForce = frictionCoeff * speed;
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
        if (speed > maxSpeed && timeTurbo == 0) speed = maxSpeed;
        float accelerationResistance = airResistance * speed * (speed / 2) / mass;
        float accelerationEngine = engineForce / mass;

        float acceleration = accelerationEngine;
        if (acceleration > 0) acceleration -= accelerationResistance;
        else if (acceleration < 0) acceleration += accelerationResistance;

        speed = Time.deltaTime * (acceleration - accelerationResistance) + speed + (5*timeTurbo);
        if (timeTurbo > 0)
            timeTurbo -= Time.deltaTime;
        if (timeTurbo < 0)
            timeTurbo = 0;


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
        if (Input.GetKey(KeyCode.UpArrow) && speed < maxSpeed)
        {
            engineForce += engineForceStep * Time.deltaTime;


        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            // float nextSpeed = Mathf.Abs(engineForce - engineForceStep) / mass;
            if (speed > 100) engineForce -= engineForceStep * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && rocketLaunchCooldown <= 0f && currentRockets > 0)
        {
            hasMissile = false;
            Vector3 offset = transform.forward * 30;
            GameObject obj = (GameObject)Instantiate(missile, transform.position + offset, transform.rotation);
            currentRockets -= 1;
            rocketLaunchCooldown = 0.5f;
        }


        if (speed < 0) { speed = 0; engineForce = 0; }

        if (Input.GetKey(KeyCode.R)) addRocket();
        if (Input.GetKey(KeyCode.C)) checkPointPassed();
        gameObject.transform.position += transform.forward * speed * Time.deltaTime;
        currentYRotation += -Time.deltaTime * zRotation;
        // TODO: cambiar pitch de ruido motor segun las revoluciones de este



    }

    void checkPointPassed()
    {

        checkCooldown = 5f;
        currentChecks += 1;
    }
    void FixedUpdate()
    {

        // este esta en el centro de la nave

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        //float currentYRotation = gameObject.transform.rotation.eulerAngles.y;


        if (Physics.Raycast(ray, out hit, 40f) && hit.transform.tag != "muro" && hit.transform.tag != "baldosaEnergia" && hit.transform.tag != "baldosaCohete")
        {
            gameObject.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            gameObject.transform.position += transform.up * maxHeight;

            gameObject.transform.up = hit.normal;

            // importante el orden
            /*gameObject.transform.Rotate(0f, currentYRotation, 0f);
            gameObject.transform.Rotate(0f, 0f, zRotation);*/


            Debug.DrawLine(ray.origin, hit.point, Color.red);
        }
        gameObject.transform.Rotate(0f, currentYRotation, 0f);
        gameObject.transform.Rotate(0f, 0f, zRotation);
        if (hit.transform.tag == "baldosaEnergia")
        {
            if (hit.collider.GetComponent<BaldosaScript>().usar()) modifyEnergy(20);
        }
        else if (hit.transform.tag == "baldosaCohete")
        {
            if (hit.collider.transform.parent.GetComponent<BaldosaCoheteScript>().usar()) addRocket();
        }
        else if (hit.transform.tag == "baldosaCheck" && checkCooldown <= 0f)
        {
            checkPointPassed();
        }
        else if (hit.transform.tag == "baldosaTurbo")
        {
            timeTurbo = 3;
        }
    }
}