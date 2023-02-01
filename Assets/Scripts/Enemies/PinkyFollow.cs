using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PinkyFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float EndScale;
    
    Vector3 EndPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePos();
    }

    void CalculatePos()
    {
        EndPos = Player.transform.position + Player.transform.forward * EndScale;


        RaycastHit hit;
        if (Physics.Raycast(EndPos + new Vector3(0, 10, 0), -transform.up, out hit, 1000, whatIsGround))
        {
            GetComponent<NavMeshAgent>().destination = EndPos;
        }
        else
        {
            GetComponent<NavMeshAgent>().destination = Player.transform.position;
        }
    }
}
