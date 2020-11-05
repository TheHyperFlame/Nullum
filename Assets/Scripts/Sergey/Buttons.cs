using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Buttons : MonoBehaviour
{
    Button Mute_Button, Pause_Button;
    Scrollbar Volume_ScrollBar;
    private GameObject Pause_Panel;

    [SerializeField]
    Sprite Volume0_Sprite, Volume1_Sprite;

    private float Volume = 0.5f;

    private void Start()
    {
        Mute_Button = GameObject.Find("Mute_Button").GetComponent<Button>() ;
        Pause_Button = GameObject.Find("Pause_Button").GetComponent<Button>();
        Volume_ScrollBar = GameObject.Find("Volume_Scrollbar").GetComponent<Scrollbar>();
        Pause_Panel = GameObject.Find("Pause_Panel");
        Pause_Panel.SetActive(false);
        Pause_Button.GetComponent<AudioSource>().volume = Volume;
        Volume_ScrollBar.GetComponent<Scrollbar>().value = Volume;
        Mute_Button.GetComponent<Image>().sprite = Volume1_Sprite;
    }

    private bool MusicOn = true;
    public void MuteButton()
    {
        if (MusicOn)
        {
            Pause_Button.GetComponent<AudioSource>().mute = true;
            Mute_Button.GetComponent<Image>().sprite = Volume0_Sprite;
            MusicOn = false;
        }
        else
        {
            Pause_Button.GetComponent<AudioSource>().mute = false;
            Mute_Button.GetComponent<Image>().sprite = Volume1_Sprite;
            MusicOn = true;
        }
        
    }
    public void VolumeScrollBar()
    {
        Volume = Volume_ScrollBar.GetComponent<Scrollbar>().value;
        Pause_Button.GetComponent<AudioSource>().volume = Volume;
    }
 
    public void OpenPauseMenu()
    {
        Pause_Panel.SetActive(true);
    }
    public void ClosePausePanel()
    {
        Pause_Panel.SetActive(false);
    }

}
