using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BuilderToggle : MonoBehaviour
{
    public GameObject Left;
    public GameObject Right;
    public Slider Slider;

    public void Awake()
    {
        Left.SetActive(false);
        Right.SetActive(true);
    }

    public void ChangeMode()
    {
        if (Slider.value > 0.5)
        {
            Left.SetActive(true);
            Right.SetActive(false);
        }
        else
        {
            Left.SetActive(false);
            Right.SetActive(true);
        }
    }
}