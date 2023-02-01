using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowQuestpointer : MonoBehaviour
{
    MeshRenderer[] renderers;
    public GameObject Player;
    public GameObject goTarget;

    public float HeightAbovePlayer;
    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();
        goTarget = null;
    }
    void Update()
    {
        PositionArrow();
    }

    void PositionArrow()
    {
        if (!goTarget)
        {
            for(int i = 0; i < renderers.Length; ++i)
            {
                renderers[i].enabled = false;
            }
            return;
        }

        for (int i = 0; i < renderers.Length; ++i)
        {
            renderers[i].enabled = true;
        }


        transform.LookAt(goTarget.transform.position);

        Vector3 pos = Player.transform.position;
        pos.y = HeightAbovePlayer +  Player.transform.position.y + Player.transform.localScale.y;
        transform.position = pos;
    }
}
