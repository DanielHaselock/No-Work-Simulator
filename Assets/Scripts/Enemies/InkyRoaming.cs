using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyRoaming : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    [SerializeField] private LayerMask whatIsGround;
    Vector3 WalkPoint;
    public LineRenderer line;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    private bool WalkPointSet;
    Vector3 lastPosition;
    float timeCheck;
    void Start()
    {
        timeCheck = Time.time;
        lastPosition = Vector3.zero;
        WalkPointSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        stuck();
        Patroling();
    }

    private void Patroling()
    {
        if (!WalkPointSet)
            CheckGround();

        if(!agent.hasPath)
        {
         //   Debug.DrawLine(WalkPoint + new Vector3(0, 10, 0), -transform.up);
           // agent.SetDestination(WalkPoint);
            print("setting");
        }

        //if (WalkPointSet)
        //{

        //}
        //else
            

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        Vector2 distanceToWalkPoint2d;

        distanceToWalkPoint2d.x = distanceToWalkPoint.x;
        distanceToWalkPoint2d.y = distanceToWalkPoint.z;

        if (distanceToWalkPoint2d.magnitude < 1.5f)
            WalkPointSet = false;
    }


    void CheckGround()
    {
        RaycastHit hit;

        Vector2 random = Random.insideUnitCircle;
        float alpha = random.magnitude;
        float distance = Random.Range(-100, 100);
        random = random.normalized * distance;

        WalkPoint = new Vector3(random.x + transform.position.x, transform.position.y, random.y + transform.position.z);
        
        if (Physics.Raycast(WalkPoint + new Vector3(0, 10, 0), -transform.up, out hit, 1000, whatIsGround))
        {
            
            agent.destination = WalkPoint;
            
            WalkPointSet = true;

            return;
        }
        //}
        //CheckGround();
    }


    private void stuck()
    {
        if (lastPosition == Vector3.zero)
        {
            lastPosition = transform.position;
        }

        if ((transform.position - lastPosition).magnitude > 100.0)
        {
            timeCheck = Time.time;
            lastPosition = transform.position;
        }
        if (Time.time - timeCheck > 5.0)
        {
            // we are stuck so lets move.
            // reset code here:

            WalkPointSet = false;
            timeCheck = Time.time;
            lastPosition = transform.position;
        }
    }

    public void Stop()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;

    }
    public void Resume()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;

    }

}
