using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    public Text text; 
    private float timer;
    // private Animator animator;

    void Start()
    {
        // animator = GetComponent<Animator>();
        timer = PlayerPrefs.GetFloat("timeDisplay");

        // if(timer <= 10f)
        // {
        //     animator.SetBool("IsRecordTime",true);
        // }
        // else
        // {
        //     animator.SetBool("IsRecordTime",false);
        // }

        string timeDisplay = timer.ToString("N2"); //format pour les float , pour avoir 2 chiffres derrière la virgule
        text.text = timeDisplay;

    }
}
