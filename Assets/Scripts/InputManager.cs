using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class InputManager
{
    static Dictionary<string, KeyCode> keyMapping;

    static string[] keyMaps = new string[]
    {
        "Cancel",
        "Accept",
        "Select",
        "Place",
        "CameraRotationControl",
        "BuildMode",
        "GridMode",
        "DestroyMode",
        "RotateLeft",
        "RotateRight",
        "RotateBuilding",
    };

    static KeyCode[] KeyCodes = new KeyCode[]
    {
        KeyCode.Escape,
        KeyCode.Space,
        KeyCode.Mouse0,
        KeyCode.Mouse1,
        KeyCode.Mouse2,
        KeyCode.Tab,
        KeyCode.G,
        KeyCode.Delete,
        KeyCode.Q,
        KeyCode.E,
        KeyCode.R,
    };

    static InputManager()
    {
        InitializeDictionary();
    }

    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<string, KeyCode>();
        for (int i = 0; i < keyMaps.Length; ++i)
        {
            keyMapping.Add(keyMaps[i], KeyCodes[i]);
        }
    }

    public static void SetKeyMap(string keyMap, KeyCode key)
    {
        if (!keyMapping.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        keyMapping[keyMap] = key;
        Debug.Log("Now " + key + " using for " + keyMap);
    }

    public static bool GetKeyDown(string keyMap)
    {
        return Input.GetKeyDown(keyMapping[keyMap]);
    }

    public static bool GetKey(string keyMap)
    {
        return Input.GetKey(keyMapping[keyMap]);
    }

    public static bool GetKeyUp(string keyMap)
    {
        return Input.GetKeyUp(keyMapping[keyMap]);
    }
}