using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{

    public bool IsMoving;
    public bool CanMove = true;
    private bool CanDodgeRoll = true;
    public bool AllowedToDodgeRoll = true;

    private Vector2 Direction;
    public float PlayerMovementSpeed = 0.5f;
    public float PlayerRotationSpeed = 1;
    private bool IsColliding = false;
    public string Tag;
    public float DodgeRollTime = 1;
    public float DodgeRollDelay = 2;
    
    public bool IsDodgeRolling = false;
    public float DodgeRollSpeed = 5;

    public Animator PlayerMovementAnimator;
    public Rigidbody PlayerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        IsMoving = false;
        Direction = Vector2.zero;
        PlayerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRigidBody.isKinematic = true;
        if (CanMove)
        {


            Vector3 forward = transform.forward;

            Debug.DrawRay(transform.position, forward);
            if (IsMoving)
            {
                PlayerMovementAnimator.SetBool("Dodgeroll", false);
                PlayerMovementAnimator.SetBool("moveBool", true);
                if (Direction.x != 0)
                {
                    transform.Rotate(0, Direction.x * Time.deltaTime * PlayerRotationSpeed, 0);


                }
                if (Direction.y != 0)
                {
                    PlayerRigidBody.isKinematic = false;
                    Vector3 translation;
                    if (IsDodgeRolling)
                    {
                        PlayerMovementAnimator.SetBool("Dodgeroll", true);
                        PlayerMovementAnimator.speed = 1f;
                        translation = transform.forward * DodgeRollSpeed;
    
                    }
                    else
                    {
                        PlayerMovementAnimator.speed = 1;
                        translation = transform.forward * PlayerMovementSpeed;
                    }
                    PlayerRigidBody.velocity = translation;
                }
            }
            else if(!IsMoving)
            {
                IsDodgeRolling = false;
                PlayerMovementAnimator.SetBool("moveBool", false);
            }

        }

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        AllowedToDodgeRoll = true;
        if(context.performed)
        {
            if(GetComponent<Sleep>().StartedSleep && GetComponent<Sleep>().Stamina > 30 ||
                !GetComponent<Sleep>().StartedSleep)
            {
                IsMoving = true;
                Direction = context.ReadValue<Vector2>();
            }

        }
        else if (context.canceled)
        {
            IsMoving = false;
        }
    }

    public void OnDodgeRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(CanDodgeRoll && AllowedToDodgeRoll && IsMoving && GetComponent<Sleep>().Stamina > 30)
                StartCoroutine(DodgeRoll());
        }
    }

    IEnumerator DodgeRoll()
    {
        CanDodgeRoll = false;
        IsDodgeRolling = true;
        PlayerMovementAnimator.SetBool("Dodgeroll", true);
        yield return new WaitForSeconds(DodgeRollTime);
        IsDodgeRolling = false;
        StartCoroutine(DodgeRollDelayFunc());
        
    }
    IEnumerator DodgeRollDelayFunc()
    {
        yield return new WaitForSeconds(DodgeRollDelay);
        CanDodgeRoll = true;
    }


    //Collisions
    private void OnCollisionEnter(Collision collision)
    {

        IsColliding = true;
        
    }
    private void OnCollisionExit(Collision collision)
    {
        IsColliding = false;
    }
}
