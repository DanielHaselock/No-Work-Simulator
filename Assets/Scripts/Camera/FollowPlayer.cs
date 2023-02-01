using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    Vector3 NewPosition;
    public Vector3 CameraDisplacement = Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NewPosition.x = Player.transform.position.x;
        NewPosition.y = transform.position.y;
        NewPosition.z = Player.transform.position.z;

        NewPosition.x += CameraDisplacement.x;
        NewPosition.z += CameraDisplacement.z;
        NewPosition.y = CameraDisplacement.y;
        transform.position = NewPosition;
    }



}
