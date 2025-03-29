using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowStats : MonoBehaviour
{
    [SerializeField] private GameObject Window;

    public virtual void SetWindowStatus(bool status)
    {
        Window.SetActive(status);
    }
}
