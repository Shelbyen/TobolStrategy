using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputKeyChanger : MonoBehaviour
{
    public string ActiveKeyMap;
    public TMP_Text ActiveKeyButtonText;

    void Update()
    {
        if (ActiveKeyMap != "")
        {
            foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>())
            {
                if (Input.GetKeyDown(Key))
                {
                    ChangeKeyMap(ActiveKeyMap, Key);
                    ActiveKeyMap = "";
                }
            }
        }
    }

    public void StartKeyCheck(string KeyMap)
    {
        ActiveKeyMap = KeyMap;
    }

    public void SetButtonName(GameObject ActiveButton)
    {
        ActiveKeyButtonText = ActiveButton.GetComponentInChildren<TMP_Text>();
        ActiveKeyButtonText.text = "";
    }

    private void ChangeKeyMap(string KeyMap, KeyCode Key)
    {
        InputManager.SetKeyMap(KeyMap, Key);
        ActiveKeyButtonText.text = $"{Key}";
    }
}
