using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyDropStuff : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject InstanceObject;
    public LayerMask whatIsGround;

    public int MinimumTimeSpawn;
    public int MaximumTimeSpawn;

    private bool CoroutineStart;

    public Animator InkyAnim;
    public float MilkTime;
    void Start()
    {
        CoroutineStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!CoroutineStart)
        {
            StartCoroutine(SpawnPeriodically());
        }
    }


    public IEnumerator SpawnPeriodically()
    {
        CoroutineStart = true;
        float random = Random.Range(MinimumTimeSpawn, MaximumTimeSpawn - MinimumTimeSpawn);

        yield return new WaitForSeconds(random);
        SpawnOject();
        CoroutineStart = false;
    }

    void SpawnOject()
    {

        StartCoroutine(PutMilk());
    }

    IEnumerator PutMilk()
    {
        InkyAnim.SetBool("IsMilking", true);
        gameObject.GetComponent<InkyRoaming>().Stop();

        yield return new WaitForSeconds(MilkTime);
        RaycastHit hit;
        Vector3 endpoint;
        if (Physics.Raycast(transform.position + new Vector3(0, 10, 0), -transform.up, out hit, 1000, whatIsGround))
        {
            endpoint = hit.point;
            GameObject newObject = Instantiate(InstanceObject, new Vector3(endpoint.x, endpoint.y, endpoint.z), Quaternion.identity);
            //Animation here

        }
        else
        {
            print("unspawnable");
        }
        InkyAnim.SetBool("IsMilking", false);
        gameObject.GetComponent<InkyRoaming>().Resume();
    }

}
