using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brogger : MonoBehaviour
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
	public Bitey bty;
	public AudioSource noise;
	public AudioSource foot;
	public AudioSource bite;
	public AudioSource die;
	public float timtam = 0;

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

        // If near/can see a player, switch into "ORIENTATION", which switches into "AGGRO"
        float distance = Vector3.Distance(p.transform.position, transform.position);
        if (distance < aggroDist && state != "AGGRO" && state != "ORIENTATION" && state != "ATTACK") {
            state = "ORIENTATION";
            wanderTime = 0;
        } else if (distance > aggroDist * 1.3f && state == "AGGRO") { // This factor will likely need to change.
            state = "WANDER";
            // It will slow down, then come to a stop at the player's position at the moment of loss of interest (assuming it can get there in time)
            wanderTime = 0;
        }

        // Get ready for some shit
        if (state == "ORIENTATION") {
            waypoint = p.transform.position;
            wanderTime = wanderTime + Time.deltaTime;
            if (wanderTime < spinTime) {
                transform.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + wanderTime * wanderTime * 500, 0)));
            }
            else {
                Debug.Log(".");
                // Okay, so get the look rotation toward the player, but then only use the Y component.
                transform.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(new Vector3(0, Quaternion.LookRotation(waypoint - transform.position, Vector3.up).eulerAngles.y, 0)));
                state = "AGGRO";
                wanderTime = 0;
            }
        }

        // If wandering, make sure there is one waypoint.
        if (state == "WANDER") {
            // Debug.Log(wanderTime);
            // If a waypoint is inaccessible, allow it to time out.
            wanderTime = wanderTime + Time.deltaTime;
            if(wanderTime > maxWanderTime) {
                NewWaypoint();
                wanderTime = 0;
			}
			timtam += Time.deltaTime;
			if (timtam > .7f)
			{
				foot.Play();
				timtam = 0;
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
        } else if(state == "AGGRO") {
            waypoint = p.transform.position;
            wanderTime = wanderTime + Time.deltaTime;
            currentSpeed = Mathf.Lerp(currentSpeed, movementSpeed, movementSpeed * Time.deltaTime);

            // Take current angle and take angle toward player. Lerp the angle toward the player angle.
            // If the angle is too much, go into "ORIENTATION"
            // Vector2 direction = new Vector2(transform.position.x, transform.position.z) - new Vector2(p.transform.position.x, p.transform.position.z);
            Quaternion intention = Quaternion.LookRotation(waypoint - transform.position, Vector3.up);
            intention = Quaternion.Euler(new Vector3(0, intention.eulerAngles.y, 0));
            // Quaternion.LookRotation(player.position - cam.transform.position, Vector3.up).eulerAngles;
            Debug.Log(Quaternion.LookRotation(transform.forward, Vector3.up).eulerAngles.y - intention.eulerAngles.y);
            if (Mathf.Abs(Quaternion.LookRotation(transform.forward, Vector3.up).eulerAngles.y - intention.eulerAngles.y) > 30 && wanderTime > 0.1f) {
                state = "ORIENTATION";
                wanderTime = 0;
			}
			timtam += Time.deltaTime;
			if (timtam > .7f)
			{
				foot.Play();
				timtam = 0;
			}

			// ==============================================================================================================================
			// vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv

			// If close enough, queue up an attack???
			if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(waypoint.x, waypoint.z)) < 1) {
                state = "ATTACK";
                wanderTime = 0;
				anim.SetTrigger("Attack");
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
            if(wanderTime > 1) {
                state = "AGGRO";
                wanderTime = 0;
            }
        }

		if (Random.value < 0.002f && !noise.isPlaying)
			noise.Play();
		bty.SetState(state);
    }

    private void FixedUpdate() {
        
        // Handle drag - when idle, the brog should stop quickly.
        if (state != "IDLE") {
            transform.GetComponent<Rigidbody>().drag = 1;
        } else {
            transform.GetComponent<Rigidbody>().drag = 10;
        }


        // Turn if you're aggro and not spinning /OR/ if you're wandering
        if ( (state == "AGGRO" && wanderTime > 0.1f) || state == "WANDER") {
            Quaternion intention = Quaternion.LookRotation(waypoint - transform.position, Vector3.up);
            intention = Quaternion.Euler(new Vector3(0, intention.eulerAngles.y, 0));
            transform.GetComponent<Rigidbody>().MoveRotation(Quaternion.Slerp(Quaternion.LookRotation(transform.forward), intention, turnRate * Time.deltaTime));
        }

        // Handle movement
        if (state != "IDLE" && state != "ATTACK" && state != "ORIENTATION")
        {
            // transform.position = Vector3.MoveTowards(transform.position, waypoint, currentSpeed * Time.deltaTime);

            transform.GetComponent<Rigidbody>().velocity = Vector3.Lerp(transform.GetComponent<Rigidbody>().velocity, transform.forward * currentSpeed, 3 * Time.deltaTime);
        }

        // Always tell it how fast to be goin
        anim.SetFloat("Speed", currentSpeed / movementSpeed);
        // Debug.Log(currentSpeed / movementSpeed);

        // Fix rotation I guess
        // transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

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
                // Debug.Log(waypoint);
            }
        }
        return;
    }

	public bool Attacking()
	{
		if (state == "ATTACK")
			return true;

		return false;
	}
}
