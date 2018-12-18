using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    bool Pause = false;

    void Update()
    {
        //If the INPUT KEY P changes the pause boolean, this will update.
        if (Pause == false)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }

        //This controls the pause if the key P is pressed
        if (Input.GetKey(KeyCode.P))
        {
            if (Pause == true)
            {
                Pause = false;
            }
            else
            {
                Pause = true;
            }
        }
    }

    public void PauseResumeGame() {

        if (Pause == true)
        {
            Pause = false;
        }
        else
        {
            Pause = true;
        }
    }

}
