#pragma strict

var missileVelocity : float = 300;
var turn : float = 20;
var homingMissile : Rigidbody;
var fuseDelay : float;
//var missileMod : GameObject;
//var SmokePrefab : ParticleSystem;
//var missileClip : AudioClip;

private var target : Transform;

function Start () {

Fire();

}


function FixedUpdate ()

{
	if(target == null || homingMissile == null)
		return;
		
	homingMissile.velocity = transform.forward * missileVelocity;
	
	var targetRotation = Quaternion.LookRotation(target.position - transform.position);
	
	homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));

}


function Fire ()

{
	yield WaitForSeconds (fuseDelay);
	//AudioSource.PlayClipAtPoint(missileClip, transform.position);
	
	var distance = Mathf.Infinity;
	
	for (var go : GameObject in GameObject.FindGameObjectsWithTag("target"))
		{
			var diff = (go.transform.position - transform.position).sqrMagnitude;
			
			if(diff < distance)
			{
				distance = diff;
				target = go.transform;
			}
			
		
		}

}

function OnCollisionEnter (theCollision : Collision)
{

	if(theCollision.gameObject.name == "Cube")
	{
		//SmokePrefab.emissionRate = 0.0f;
		//Destroy(missileMod.gameObject);
		//yield WaitForSeconds(1);
		Destroy(gameObject);
	}

}

