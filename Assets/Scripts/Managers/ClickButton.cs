using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    public AudioSource ClickSource;

    public void Click()
    {
        ClickSource.Play();
    }
}
