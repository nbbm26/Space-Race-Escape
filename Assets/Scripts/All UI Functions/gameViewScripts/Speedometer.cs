using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour {

    static Speedometer Needle;

    static float minAngle = 138.0f;
    static float maxAngle = -142.0f;

    // Use this for initialization
    void Start () {

        Needle = this;
	}

    public static void ShowSpeed(float speed, float min, float max)
    {
        float theNewAngle = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(min, max, speed));
        Needle.transform.eulerAngles = new Vector3(0, 0, theNewAngle);
    }
}
