using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LinkManager
{
    private static Builder builder;
    private static UIManagerScript uiManager;

    public static Builder GetBuilder()
    {
        return builder;
    }
    public static UIManagerScript GetUIManager()
    {
        return uiManager;
    }

    public static void Reload()
    {
        builder = GameObject.Find("Builder").GetComponent<Builder>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
    }
}
