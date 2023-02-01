using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sleep : MonoBehaviour
{
    // Start is called before the first frame update

    public float Stamina = 100;
    public float StaminaSpeed_Moving = 1;
    public float StaminaSpeed_Normal = 1;
    public float StaminaSleep = 1;
    private bool IsMoving_b = false;
    public bool StartedSleep = false;
    private int MinbarAfterSleep = 30;
    public Slider healthBar;

    public Animator PlayerMovementAnimator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        IsMoving();
        if (!IsMoving_b && StartedSleep)
        {
            IsSleeping();
        }
        else if(IsMoving_b && StartedSleep && Stamina > MinbarAfterSleep)
        {
            StartedSleep = false;
            IsMoving();
            IsAwake();
        }
        else 
        {
            IsMoving();
            IsAwake();
        }
      
        SetHealth(Stamina);
    }

    public void IsMoving()
    {
        Movement mov = GetComponent<Movement>();
        PlayerMovementAnimator.SetBool("Sleep", false);
        IsMoving_b = mov.IsMoving;
    }

    public void IsAwake()
    {
        
        if (IsMoving_b)
        {
            Stamina -= Time.deltaTime * StaminaSpeed_Moving;
        }
        else
        {
            Stamina -= Time.deltaTime * StaminaSpeed_Normal;
        }

    }

    public void CheckState()
    {
        if(Stamina <= 0)
        {
            Stamina = 0;
            Movement mov = GetComponent<Movement>();
            IsMoving_b = false;
            mov.IsMoving = false;
            mov.CanMove = false;
            mov.AllowedToDodgeRoll = false;
            StartedSleep = true;
        }
        else if(Stamina > 0)
        {
            Movement mov = GetComponent<Movement>();
            mov.CanMove = true;
        }

    }
    public void IsSleeping()
    {
        Movement mov = GetComponent<Movement>();
        PlayerMovementAnimator.SetBool("Sleep", true);
       
        mov.AllowedToDodgeRoll = false;
        if (Stamina < 100)
            Stamina += Time.deltaTime * StaminaSleep;
    }

    public void OnSleep()
    {
        StartedSleep = true;

    }

    public void SetHealth(float hp)
    {
        healthBar.value = hp;
    }

}
