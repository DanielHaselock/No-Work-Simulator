using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collisions : MonoBehaviour
{
    // Start is called before the first frame update
    public string TagEnemies;
    public string TagMilk;

    public int MovementSlow;

    private bool IsInSlow;
    private float StartSpeed;
    void Start()
    {
        IsInSlow = false;
        StartSpeed = gameObject.GetComponent<Movement>().PlayerMovementSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if(IsInSlow)
        {
            gameObject.GetComponent<Movement>().PlayerMovementSpeed = MovementSlow;
        }
        else if(gameObject.GetComponent<Movement>().PlayerMovementSpeed != StartSpeed)
        {
            gameObject.GetComponent<Movement>().PlayerMovementSpeed = StartSpeed;
        }


    }
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == TagEnemies)
        {
            if(!gameObject.GetComponent<Movement>().IsDodgeRolling)
            {
                //Kill player 
                print("KILL");
                SceneManager.LoadScene(0);
            }


        }
        else if(collision.gameObject.tag == TagMilk)
        {
            
            IsInSlow = true;
            print("SLOW");
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == TagMilk)
        {
            IsInSlow = false;
        }
    }
}
