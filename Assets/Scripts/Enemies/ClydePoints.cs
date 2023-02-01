using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ClydePoints : MonoBehaviour
{


    //[SerializeField] private AnimationCurve curve;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private LayerMask whatIsGround;
    //[SerializeField] private bool showDebugLine = false;
   // [SerializeField] private float detectionFOV;

    //Patroling
    [SerializeField] private Vector3 walkPoint;
    private bool walkPointSet;

    private float normalSpeed;

    private LineRenderer lineRenderer1;
    private LineRenderer lineRenderer2;

    [SerializeField] private GameObject LineRendererChild1;
    [SerializeField] private GameObject LineRendererChild2;


    Vector3 lastPosition;
    float timeCheck;
 
    //private float offsetmodif;
    public bool WalkPointSet { get => walkPointSet; set => walkPointSet = value; }
    public Vector3 WalkPoint { get => walkPoint; set => walkPoint = value; }
    // Start is called before the first frame update
    void Start()
    {
        timeCheck = Time.time;
        lastPosition = Vector3.zero;
        //offsetmodif = 0.5f;
        lineRenderer1 = LineRendererChild1.GetComponent<LineRenderer>();
        lineRenderer2 = LineRendererChild2.GetComponent<LineRenderer>();

        Vector3 newpos = lineRenderer1.transform.position;
       // newpos.y = 0;
        lineRenderer1.transform.position = newpos;


        Vector3 newpos2 = lineRenderer2.transform.position;
       // newpos2.y = 0;
        lineRenderer2.transform.position = newpos;

        normalSpeed = agent.speed;

        walkPointSet = false;

    }

    private void Update()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         stuck();    
         Patroling();

    }

    private void Patroling()
    {
        agent.speed = normalSpeed;
        if (!WalkPointSet)
            SearchNewWalkPoint();

        if (WalkPointSet)
        {

        }
        else
            agent.SetDestination(WalkPoint);

        Vector3 distanceToWalkPoint = transform.position - WalkPoint;

        Vector2 distanceToWalkPoint2d;

        distanceToWalkPoint2d.x = distanceToWalkPoint.x;
        distanceToWalkPoint2d.y = distanceToWalkPoint.z;

        if (distanceToWalkPoint2d.magnitude < 1.5f)
            WalkPointSet = false;
    }


    Vector3 GetPositionAroundObject(Transform tx, float radius, float radius2)
    {
        if(radius2 > radius)
        {
            float tmp = radius2;
            radius2 = radius;
            radius = tmp;
        }

        Vector2 random = Random.insideUnitCircle;
        float alpha = random.magnitude;
        float distance = Mathf.Lerp(radius, radius2, alpha);
        random = random.normalized * distance;

        Vector3 offset = new Vector3(random.x, tx.transform.position.y, random.y);
        return tx.position + offset;
    }
    private void SearchNewWalkPoint()
    {
        RaycastHit hit;

        Vector3 RandomPoint2 = GetPositionAroundObject(LineRendererChild1.transform, LineRendererChild1.GetComponent<outline>().radius, LineRendererChild2.GetComponent<outline>().radius);
        WalkPoint = RandomPoint2;
        walkPoint.y = transform.position.y;

        if (Physics.Raycast(WalkPoint + new Vector3(0, 500, 0), -transform.up, out hit, 5000, whatIsGround))
        {
      

           Debug.DrawLine(WalkPoint, LineRendererChild2.transform.position);

            if (Vector3.Distance(WalkPoint, LineRendererChild2.transform.position) > LineRendererChild2.GetComponent<outline>().radius)
            {
                agent.destination = WalkPoint;
                walkPointSet = true;
                return;
            
            }
        }

       
    }



    private bool SetDestination(Vector3 targetDestination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetDestination, out hit, 100f, NavMesh.AllAreas))
        {
            Vector3 tmp = hit.position;

            WalkPoint = new Vector3(tmp.x, 0, tmp.z);

            agent.destination = WalkPoint;
            return true;
        }

        return false;
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

}
