using UnityEngine;
using Steerings;

[RequireComponent(typeof(Arrive))]

public class FollowMouseClick : MonoBehaviour {


    private Arrive myArrive;
    private GameObject waypoint;

    void Start ()
    {

        waypoint = new GameObject("waypoint");
        waypoint.transform.position = new Vector3(0,0,0);

        myArrive = gameObject.GetComponent<Arrive>();
        myArrive.closeEnoughRadius = 0f;
        myArrive.target = waypoint;
        myArrive.rotationalPolicy = SteeringBehaviour.RotationalPolicy.NONE;

        
    }
    

    void Update () {
		
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 click = Input.mousePosition;
            Vector3 wantedPosition = Camera.main.ScreenToWorldPoint(new Vector3(click.x, click.y, 1f));
            wantedPosition.z = transform.position.z;
            waypoint.transform.position = wantedPosition;
            myArrive.target = waypoint;
        }

        DebugExtension.DebugArrow(gameObject.transform.position,
                                  gameObject.GetComponent<KinematicState>().linearVelocity, Color.red);

        // DebugExtension.DebugPoint(waypoint.transform.position, Color.blue, 3);
	}
}
