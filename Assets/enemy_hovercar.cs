using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using System.Runtime.CompilerServices;

/*
VECTOR MATH
--------------
 3
down vote
accepted

It is simple, really.

B = -1/2 * A, or B.x = -1/2 * A.x, B.y = -1/2 * A.y, B.z = -1/2 * A.z. This talks about vectors, btw. You would want to shift the result.
The formula is dead-simple. What am I missing?

EDIT

Your app knows the red dot location (let's abbreviate it as R vector). Your app also knows the A vector.
It needs to find the B vector that is on the same line as AR, on the other side of R as A, and such as A is twice as far than B. Well, in that case:

    Temporarily calculate vector V = (A - R)
    Now (It is simple :) ): B = R - 0.5 * V.

It is that simple, I promise. The capital letters represent vectors, which are generally 2-tuples or 3-tuples of 
real numbers (depending on whether you work in 2D or 3D).

There is not much to this, really. Any questions?



*/

/// <summary>
/// Ties all the primary ship components together.
/// </summary>


public class enemy_hovercar : MonoBehaviour//, Pathfinding
{    

    //    [Header ("Stats")]
    public float speed;
    public float torque;
    public float stoppingDistance;
    public float retreatDistance;
    public float nearToPlayerDistance;
    public float startTimeBTWShots;
    public float TimeBTWShots;

    //    [Header ("References")]
    private enemy_hovercar ShipScript;
    public GameObject shot;
    private GameObject player;
    private Transform playerTransform;
    private GameObject enemy;
    private Transform enemyTransform;
    private Vector3 nextWaypoint;
    private Rigidbody playerRigidBody;
    private Rigidbody enemyRigidBody;
    private Vector3 destination;

    // public bool isEnemy = false;

    // private enemy_hovercar_Input input;
    // private enemy_hovercar_Physics physics;    
    //private Rigidbody shipRigidBody;
    //private ParticleSystem theParticleL;
    //private ParticleSystem theParticleR;

    // Keep a static reference for whether or not this is the player ship. It can be used
    // by various gameplay mechanics. Returns the player ship if possible, otherwise null.
    // public static enemy_hovercar _enemy_hovercar { get { return _enemy_hovercarA; } }
    // private static enemy_hovercar _enemy_hovercarA;

    // Getters for external objects to reference things like input.
    //    public bool UsingMouseInput { get { return input.useMouseInput; } }
    // public Vector3 Velocity { get { return this.physics.Rigidbody.velocity; } }
    // public float Throttle { get { return input.throttle; } }

    private void Start()
    {
        // input = GetComponent<enemy_hovercar_Input>();
        // physics = GetComponent<enemy_hovercar_Physics>();
        //shipRigidBody = GetComponent<Rigidbody> ();
        player = GameObject.FindGameObjectWithTag("hovercar_targeting");
        playerTransform = GameObject.FindGameObjectWithTag("hovercar_targeting").transform;
        playerRigidBody = player.GetComponent<Rigidbody> ();
        enemy = this.gameObject;
        ShipScript = enemy.GetComponent<enemy_hovercar> ();
        enemyTransform = this.transform;
        enemyRigidBody = enemy.GetComponent<Rigidbody> ();
        nextWaypoint = GameObject.FindGameObjectWithTag ("Waypoint").transform.position;
        //theParticleL = GetComponentsInParent<ParticleSystem> ();
        //theParticleR = GetComponentsInParent<ParticleSystem> ();

    }

    private void Awake()
    {
        this.enabled = true;
        ShipScript = this.GetComponent<enemy_hovercar> ();
        ShipScript.enabled = true;
        // input = GetComponent<enemy_hovercar_Input>();
        // physics = GetComponent<enemy_hovercar_Physics>();
        //shipRigidBody = GetComponent<Rigidbody> ();
        player = GameObject.FindGameObjectWithTag("hovercar_targeting");
        playerTransform = GameObject.FindGameObjectWithTag("hovercar_targeting").transform;
        playerRigidBody = player.GetComponent<Rigidbody> ();
        enemy = this.gameObject;
        enemyTransform = this.transform;
        enemyRigidBody = enemy.GetComponent<Rigidbody> ();
        nextWaypoint = GameObject.FindGameObjectWithTag ("Waypoint").transform.position;
        //theParticleL = GetComponentsInParent<ParticleSystem> ();
        //theParticleR = GetComponentsInParent<ParticleSystem> ();

    }
    void OnCollisionEnter(Collision coll)
    {
        //Looks for rings
        if (coll.gameObject.CompareTag ("Laser")) {
            //          enemyStatusScript = coll.gameObject.GetComponent<EnemyStatus>();
            this.gameObject.GetComponent<enemy_health>().EcurrentHealth -= 5;
        }
    }
    private void Update()
    {
        this.enabled = true;
        ShipScript.enabled = true;
        // Pass the input to the physics to move the ship.
        // physics.SetPhysicsInput(new Vector3(input.strafe, 0.0f, input.throttle), new Vector3(input.pitch, input.yaw, input.roll));

        //make enemy move -- kept for reference: Quaternion.LookRotation(Vector3.MoveTowards (this.transform.position, playerTransform.position,1f)) Quaternion.Dot(enemyRigidBody.rotation, finalRotation)
        /*

        player = GameObject.FindGameObjectWithTag("hovercar_targeting");
        playerTransform = GameObject.FindGameObjectWithTag("hovercar_targeting").transform;
        Vector3 moveTowards = Vector3.MoveTowards (this.transform.position, playerTransform.position, 1);
            Vector3 relativePosition = playerTransform.position; // - this.transform.position); Quaternion.FromToRotation Quaternion.RotateTowards
        Quaternion targetRotation = Quaternion.FromToRotation(enemyRigidBody.velocity.normalized,moveTowards);

enemyRigidBody.MoveRotation(Quaternion.AngleAxis(0,Vector3.MoveTowards(enemyRigidBody.velocity,new Vector3(playerRigidBody.velocity.x,playerRigidBody.velocity.y,-1),500f))); //enemyRigidBody.rotation * finalRotation enemyrigidbody.moverotation

*/
        //          Vector3 relativePosition = playerTransform.position; // - this.transform.position); Quaternion.FromToRotation Quaternion.RotateTowards
        //          Quaternion targetRotation = Quaternion.LookRotation(enemyTransform.position,playerTransform.position);
        //          Quaternion finalRotation = Quaternion.RotateTowards (this.transform.rotation, targetRotation, (torque * Time.deltaTime));
        //      Quaternion inverse_targetRotation = new Quaternion(targetRotation.x-90f, targetRotation.y-90f, targetRotation.z,targetRotation.w);
        //      Quaternion inverse_finalRotation = Quaternion.RotateTowards (this.transform.rotation, inverse_targetRotation, (torque * Time.deltaTime));

        //          if (nearToPlayerDistance > 0 && Vector3.Distance (this.transform.position, playerTransform.position) > nearToPlayerDistance) {
        //              //destination must be set on waypoint setup points that are like an array that increments each time one is passed, basically a race track, or patrol route which would call a hard coding of setting the loop to [0] in the waypoint array
        //              //destination = FindPath (this.transform.position,destination);
        //          enemyTransform.rotation = Quaternion.RotateTowards (this.transform.rotation, targetRotation, (torque * Time.deltaTime));
        //              this.transform.position = Vector3.MoveTowards (this.transform.position, playerTransform.position, (speed * Time.deltaTime));
        //          }
        if (this.transform.position.y < 400f)
        {
            this.transform.position = Vector3.MoveTowards (this.transform.position, Vector3.up, (speed * Time.deltaTime));
//            continue;
        }
//        if (this.transform.position.y > 360f)
//        {
//            this.transform.position = Vector3.MoveTowards (this.transform.position, Vector3.down, (speed * Time.deltaTime));
////            continue;
//        }
        if (Vector3.Distance (this.transform.position, playerTransform.position) > stoppingDistance) {
            //              this.transform.rotation = Quaternion.RotateTowards (this.transform.rotation, targetRotation, (torque * Time.deltaTime));
            //              enemyRigidBody.MoveRotation(finalRotation);
            //FindPath (this.transform.position,playerTransform.position);
            enemyTransform.LookAt(playerTransform);
            this.transform.position = Vector3.MoveTowards (this.transform.position, playerTransform.position, (speed * Time.deltaTime));
        } else if (Vector3.Distance (this.transform.position, playerTransform.position) < retreatDistance) {
            //this is important, because it's basically how the enemy retreats, it seems best to setup some "random" invisible mess of waypoionts in the immediate area to sequence through like a dodge while evading player
            //destination = FindPath (this.transform.position,destination);
            //              this.transform.rotation = Quaternion.RotateTowards (this.transform.rotation, Quaternion.Inverse(targetRotation), (-1f * torque * Time.deltaTime));
            //          enemyRigidBody.MoveRotation(inverse_finalRotation);
            enemyTransform.LookAt(playerTransform);
            this.transform.position = Vector3.MoveTowards (this.transform.position, playerTransform.position, (-1f * speed * Time.deltaTime));
        } else if (Vector3.Distance(this.transform.position,playerTransform.position) < stoppingDistance){
            //              this.transform.rotation = Quaternion.RotateTowards (this.transform.rotation, targetRotation, (torque * Time.deltaTime));
            enemyTransform.LookAt(playerTransform);
            //          enemyTransform.rotation.Set(finalRotation);//           

            this.transform.position = transform.position;
        }
        //get rigid body and get velocity (magnitude of) change start speed of particles based on magnitude 
        //if(shipRigidBody.velocity.magnitude > 2.0f){
        //  theParticleL.startSpeed *= 2; 
        //  theParticleR.startSpeed *= 2; 
        //}


        // If this is the player ship, then set the static reference. If more than one ship
        // is set to player, then whatever happens to be the last ship to be updated will be
        // considered the player. Don't let this happen.
        //        if (isEnemy)
        //        _enemy_hovercarA = this;
    }
}
