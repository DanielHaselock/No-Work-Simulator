using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(1, 100)]
    [SerializeField] private int MinimumWaitTimeForRing;

    [Range(1, 100)]
    [SerializeField] private int MaximumWaitTimeForRing;

    [Range(5, 50)]
    [SerializeField] private int RingKillsPlayerTime;

    public GameObject Player;


    GameObject[] Phones;
    private bool IsChecking;

    private bool CheckPhones;

    void Start()
    {
        IsChecking = false;
        CheckPhones = false;
        Phones = GameObject.FindGameObjectsWithTag("Phone");
        SetVarsOnPhones();

    }

    // Update is called once per frame
    void Update()
    {

         CheckRingPhone();

        
    }

    void CheckRingPhone()
    {
        if (!IsChecking)
        {
            IsChecking = true;
            int random = Random.Range(0, Phones.Length);
            print("Phone" + random + "Initiated");
            StartCoroutine(Phones[random].GetComponent<Phone>().RingTime());
        }
        else
        {
            CheckPhones = true;
            for (int i = 0; i <= Phones.Length - 1; ++i)
            {
                if(Phones[i].gameObject.GetComponent<Phone>().IsWaitingForRing || Phones[i].gameObject.GetComponent<Phone>().IsRinging)
                {
                    CheckPhones = false;
                    
                    break;
                }
                
            }
            if(CheckPhones)
            {
                IsChecking = false;
            }
        }

    }

    void SetVarsOnPhones()
    {
        for(int i = 0; i <= Phones.Length - 1; ++i)
        {
            Phones[i].gameObject.GetComponent<Phone>().SetLocalVars(MinimumWaitTimeForRing, MaximumWaitTimeForRing, RingKillsPlayerTime);

        }
    }
}
