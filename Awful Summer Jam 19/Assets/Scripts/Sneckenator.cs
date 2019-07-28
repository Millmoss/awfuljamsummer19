using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneckenator : MonoBehaviour
{
    public string state = "IDLE";
    private Vector3 home;
    public Vector3 waypoint;
    public float movementSpeed = 5;
    private float currentSpeed;
    public GameObject p; // player
    public float wanderTime = 0; // Globally-used action clock. Determines the timing of most actions.
    public float maxWanderTime;
    public float wanderDistance;
    public float aggroDist;
    public Animator anim;
    public float turnRate;
    public float spinTime;

    // Start is called before the first frame update
    void Start()
    {
        state = "WANDER";

        home = transform.position;
        waypoint = home;

        // Locate the player if p has no value (could easily by avoided with other stuff but for now)
        if (p == null)
        {
            p = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update() {
        // Debug.Log(state);

        // If near/can see a player, switch into "AGGRO".
        float distance = Vector3.Distance(p.transform.position, transform.position);
        if (distance < aggroDist && state != "AGGRO" && state != "ATTACK") {
            state = "AGGRO";
            wanderTime = 0;
        }
        else if (distance > aggroDist * 1.3f && state == "AGGRO") { // This factor will likely need to change.
            state = "WANDER";
            // It will slow down, then come to a stop at the player's position at the moment of loss of interest (assuming it can get there in time)
            wanderTime = 0;
        }

        // If wandering, make sure there is one waypoint.
        if (state == "WANDER") {
            // Debug.Log(wanderTime);
            // If a waypoint is inaccessible, allow it to time out.
            wanderTime = wanderTime + Time.deltaTime;
            if (wanderTime > maxWanderTime) {
                NewWaypoint();
                wanderTime = 0;
            }

            // Check that waypoint isn't too close to the player.
            // If too close, make a new one with random changes to x and z.

            // When moving, we don't want to be as urgent as if chasing the player.
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(waypoint.x, waypoint.z)) > 0.2f) {
                currentSpeed = Mathf.Lerp(currentSpeed, movementSpeed / 3, movementSpeed / 3 * Time.deltaTime);
            } else {
                // Come to a stop if close enough to the objective.
                state = "IDLE";
            }
        } else if (state == "AGGRO") {

            // Circling player code not very good.
            waypoint = p.transform.position;
            // waypoint.x = waypoint.x + 5/(1+wanderTime/3) * Mathf.Sin(wanderTime * 0.5f);
            // waypoint.z = waypoint.z + 5/(1+wanderTime/3) * Mathf.Cos(wanderTime * 0.5f);
            wanderTime = wanderTime + Time.deltaTime;
            currentSpeed = Mathf.Lerp(currentSpeed, movementSpeed, movementSpeed * Time.deltaTime);

            // ==============================================================================================================================
            // vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

            // If close enough, queue up an attack???
            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(waypoint.x, waypoint.z)) < 1) {
                state = "ATTACK";
                wanderTime = 0;
                /// anim.Play("Bite");
            }

            // ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            // ==============================================================================================================================
        }
        // Also important - leave room for an idle state to exist.
        if (state == "IDLE") {
            // Debug.Log("!");
            wanderTime = wanderTime + Time.deltaTime / 2;
            currentSpeed = Mathf.Lerp(currentSpeed, 0, movementSpeed / 3 * Time.deltaTime);
            if (wanderTime > maxWanderTime) {
                state = "WANDER";
                NewWaypoint();
                wanderTime = 0;
            }
        }

        if (state == "ATTACK") {
            // Attack time should be constant probably.
            wanderTime = wanderTime + Time.deltaTime;
            if (wanderTime > 1) {
                state = "AGGRO";
                wanderTime = 0;
            }
        }
    }

    private void FixedUpdate() {

        // Snecken's drag coefficient shouldn't change.

        // Turn if you're aggro /OR/ if you're wandering
        if ((state == "AGGRO" && wanderTime > 0.1f) || state == "WANDER") {
            Quaternion intention = Quaternion.LookRotation(waypoint - transform.position, Vector3.up);
            intention = Quaternion.Euler(new Vector3(0, intention.eulerAngles.y + 20 * Mathf.Sin(wanderTime*3), 0));
            transform.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(Quaternion.LookRotation(transform.forward), intention, turnRate * Time.deltaTime));
        }

        // Handle movement
        if (state != "IDLE" && state != "ATTACK") {
            // transform.position = Vector3.MoveTowards(transform.position, waypoint, currentSpeed * Time.deltaTime);

            transform.GetComponent<Rigidbody>().velocity = Vector3.Lerp(transform.GetComponent<Rigidbody>().velocity, transform.forward * currentSpeed, 3 * Time.deltaTime);
        }

        // Always tell it how fast to be goin
        // anim.SetFloat("Speed", currentSpeed / movementSpeed);
        // Debug.Log(currentSpeed / movementSpeed);

        // Fix rotation I guess
        // transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

    }

    // Create a new spot for the Snecken to go to.
    void NewWaypoint() {
        Vector3 tempWaypoint;
        bool success = false;
        // For now, assuming all points are good. Maybe could use the good navmesh to ratify generated points, or bad navmesh to eliminate.
        while (!success) {

            // Uniform distribution of waypoints within a radius.
            float angle = Random.value * 2 * Mathf.PI;
            float radius = Mathf.Sqrt(Random.value) * wanderDistance;
            tempWaypoint = new Vector3(home.x + radius * Mathf.Cos(angle), home.y, home.z + radius * Mathf.Sin(angle)); // This should work!???!??!

            // Ratify position placeholder
            if (true) {
                success = true;
                waypoint = tempWaypoint;
                // Debug.Log(waypoint);
            }
        }
        return;
    }
}