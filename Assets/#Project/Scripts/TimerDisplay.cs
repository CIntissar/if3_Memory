using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDisplay : MonoBehaviour
{
    public Text text; 
    private float timer;
    void Start()
    {
        timer = PlayerPrefs.GetFloat("timeDisplay");

        string timeDisplay = timer.ToString("N2"); //format pour les float , pour avoir 2 chiffres derrière la virgule
        text.text = timeDisplay;
    }

}
