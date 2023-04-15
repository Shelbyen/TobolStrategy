using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSinc : MonoBehaviour
{
    public GameObject MainCamera;

    public void Awake()
    {
        MainCamera = Camera.main.gameObject;
    }

    public void Update()
    {
        transform.eulerAngles = MainCamera.transform.eulerAngles;
    }
}
