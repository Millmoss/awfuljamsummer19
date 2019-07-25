using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brogger : MonoBehaviour
{
    public string state = "IDLE";
    private Vector3 home;
    private Vector3 waypoint;
    public float movementSpeed = 5;
    private float currentSpeed;
    public GameObject p; // player
    private float wanderTime = 0;
    public float maxWanderTime;
    public float wanderDistance;
    public float aggroDist;
    public Animator anim;

    // Start is called before the first frame update
    void Start() {
        state = "WANDER";

        home = transform.position;
        waypoint = home;

        // Locate the player if p has no value (could easily by avoided with other stuff but for now)
        if(p == null) {
            p = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update() {
        // Debug.Log(state);

        // If near/can see a player, switch into "AGGRO".
        float distance = Vector3.Distance(p.transform.position, transform.position);
        if (distance < aggroDist && state != "AGGRO" && state != "ORIENTATION") {
            state = "ORIENTATION";
            wanderTime = 0;
        } else if (distance > aggroDist * 1.3f && state == "AGGRO") { // This factor will likely need to change.
            state = "WANDER";
            // It will slow down, then come to a stop at the player's position at the moment of loss of interest (assuming it can get there in time)
            wanderTime = 0;
        }

        // Get ready for some shit
        if(state == "ORIENTATION") {
            wanderTime = wanderTime + Time.deltaTime;
            if(wanderTime < 0.5f) {
                // Face the player
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + wanderTime * wanderTime * 500, transform.eulerAngles.z);
            } else {
                transform.LookAt(p.transform);
                state = "AGGRO";
            }
        }

        // If wandering, make sure there is one waypoint.
        if(state == "WANDER") {
            // Debug.Log(wanderTime);
            // If a waypoint is inaccessible, allow it to time out.
            wanderTime = wanderTime + Time.deltaTime;
            if(wanderTime > maxWanderTime) {
                NewWaypoint();
                wanderTime = 0;
            }

            // Check that waypoint isn't too close to the player.
            // If too close, make a new one with random changes to x and z.

            // When moving, we don't want to be as urgent as if chasing the player.
            if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(waypoint.x, waypoint.z)) > 0.1f) {
                currentSpeed = Mathf.Lerp(currentSpeed, movementSpeed / 3, movementSpeed / 3 * Time.deltaTime);
            } else {
                currentSpeed = Mathf.Lerp(currentSpeed, 0, movementSpeed / 3 * Time.deltaTime);
            }
        } else if(state == "AGGRO") {
            waypoint = p.transform.position;
            currentSpeed = Mathf.Lerp(currentSpeed, movementSpeed, movementSpeed * Time.deltaTime);

            // If close enough, queue up an attack???
        }

        // Handle movement
        if(state != "IDLE" && state != "ATTACK" && state != "ORIENTATION") {
            transform.position = Vector3.MoveTowards(transform.position, waypoint, currentSpeed * Time.deltaTime);
        }

        // Always tell it how fast to be goin
        anim.SetFloat("Speed", currentSpeed / movementSpeed);
        // Debug.Log(currentSpeed / movementSpeed);

    }

    private void FixedUpdate()
    {
        if(state == "AGGRO")
        {
            // transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.MoveTowardsAngle(transform.eulerAngles.y, ))
        }
    }

    // Create a new spot for the Brog to go to.
    void NewWaypoint() {
        Vector3 tempWaypoint;
        bool success = false;
        // For now, assuming all points are good. Maybe could use the good navmesh to ratify generated points, or bad navmesh to eliminate.
        while(!success) {

            // Uniform distribution of waypoints within a radius.
            float angle = Random.value * 2 * Mathf.PI;
            float radius = Mathf.Sqrt(Random.value) * wanderDistance;
            tempWaypoint = new Vector3(home.x + radius * Mathf.Cos(angle), home.y, home.z + radius * Mathf.Sin(angle)); // This should work!???!??!

            // Ratify position placeholder
            if(true) {
                success = true;
                waypoint = tempWaypoint;
                Debug.Log(waypoint);
            }
        }
        return;
    }
}
