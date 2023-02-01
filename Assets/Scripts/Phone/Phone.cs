using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Phone : MonoBehaviour
{
    // Start is called before the first frame update

    public bool IsRinging;
    public bool IsWaitingForRing;
    private int RingKillsPlayerTimeLocal;
    private int MinimumWaitTimeForRingLocal;
    private int MaximumWaitTimeForRingLocal;
    public GameObject Arrow;
    private bool OneTime;

    public GameObject AudioManager;

    public GameObject uicontroller;


    public Text text;
    void Start()
    {
        IsRinging = false;
        IsWaitingForRing = false;
        OneTime = true;
       // mat.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        uicontroller = GameObject.FindGameObjectWithTag("UI");
    }

    void StopRinging()
    {
        
        Arrow.GetComponentInChildren<WindowQuestpointer>().goTarget = null;
     
        StopAllCoroutines();
        IsRinging = false;
        
    }
    public IEnumerator RingTime()
    {
        print("IS WAITING FOR RING");
        IsWaitingForRing = true;
        
        float random = Random.Range(MinimumWaitTimeForRingLocal, MaximumWaitTimeForRingLocal - MinimumWaitTimeForRingLocal);
        yield return new WaitForSeconds(random);
        StartCoroutine(KillPlayerRing());
    }
    IEnumerator KillPlayerRing()
    {
        print("IS RINGING");
        AudioManager.GetComponent<AmbientAudioManager>().SetClip("Phone1");
       AudioManager.GetComponent<AmbientAudioManager>().PlayAudio();
        Arrow.GetComponentInChildren<WindowQuestpointer>().goTarget = this.gameObject;
        IsRinging = true;
        IsWaitingForRing = false;
        
       // mat.color = Color.green;
        yield return new WaitForSeconds(RingKillsPlayerTimeLocal);
        print("killed player");
        SceneManager.LoadScene(0);
        StopRinging();
    }

    public void SetLocalVars(int MinimumWaitTimeForRing, int MaximumWaitTimeForRing, int RingKillsPlayerTime)
    {
        MinimumWaitTimeForRingLocal = MinimumWaitTimeForRing;
        MaximumWaitTimeForRingLocal = MaximumWaitTimeForRing;
        RingKillsPlayerTimeLocal = RingKillsPlayerTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsRinging)
        {
            if (other.gameObject.tag == "Player")
            {
                OneTime = false;
                uicontroller.GetComponent<UIController>().UpdateScore();
                StopRinging();
                
            }
        }
        
    }
}

