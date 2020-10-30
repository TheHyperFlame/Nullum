using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public void Mute()
    {
        this.GetComponent<AudioSource>().mute = true;
    }
 
}
