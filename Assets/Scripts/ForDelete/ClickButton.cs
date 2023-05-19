using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for delete
public class ClickButton : MonoBehaviour
{
    public AudioSource ClickSource;

    public void Click()
    {
        ClickSource.Play();
    }
}
