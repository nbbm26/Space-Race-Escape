using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowPanel : MonoBehaviour {

    public GameObject Panel1,Panel2,Panel3,Panel4,Panel5;
    //Panel1 = mainMenuView
    //Panel2 = playingView
    //Panel3 = pauseView

    public void HideMainMenuView()
    {
        //Shows the right panel
        Panel1.gameObject.SetActive(false);
    }

    public void ShowsMainMenuView()
    {
        //Shows the right panel
        Panel1.gameObject.SetActive(true);
    }

    public void HidePlayingView()
    {
        //Shows the right panel
        Panel2.gameObject.SetActive(false);
    }

    public void ShowPlayingView()
    {
        //Shows the right panel
        Panel2.gameObject.SetActive(true);
    }

    public void HidePauseView()
    {
        //Shows the right panel
        Panel3.gameObject.SetActive(false);
    }

    public void ShowPauseView()
    {
        //Shows the right panel
        Panel3.gameObject.SetActive(true);
    }

    public void HideControlPauseView()
    {
        //Shows the right panel
        Panel4.gameObject.SetActive(false);
    }

    public void ShowControlPauseView()
    {
        //Shows the right panel
        Panel4.gameObject.SetActive(true);
    }
    public void HideControlMainView()
    {
        //Shows the right panel
        Panel5.gameObject.SetActive(false);
    }

    public void ShowControlMainView()
    {
        //Shows the right panel
        Panel5.gameObject.SetActive(true);
    }

}
