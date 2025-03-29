using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonSwitch : MonoBehaviour
{
    [SerializeField] private Image TargetImage;
    [SerializeField] private Sprite SwitchOn;
    [SerializeField] private Sprite SwitchOff;

    public void SetStatus(bool Status)
    {
        if (Status) TargetImage.sprite = SwitchOn;
        else TargetImage.sprite = SwitchOff;
    }
}
