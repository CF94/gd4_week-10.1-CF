using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    //stores a reference to the Waypoint system this object will use
    [SerializeField] private Waypoints waypoints;
    private float moveSpeed = 5f;
    public float distanceThreshold = 0.1f;
    private Transform currentWaypoint;
    
    void Start()
    {
        //set initial position to first waypoint
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;

        //Set next waypoint target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
        }
    }
}
