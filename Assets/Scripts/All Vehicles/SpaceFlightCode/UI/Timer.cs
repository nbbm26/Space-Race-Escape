using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

    private Text text;
    

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
         text.text = string.Format("TIME LEFT: " + (int)PlayerHealth.timeLeft);
    }
}
