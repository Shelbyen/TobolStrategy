using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagstaff : House
{

    public GameObject Fortress;
    private UIManagerScript UIManager;
    private StateManager StateManager;

    public override void Awake()
    {
        base.Awake();
        Fortress.SetActive(false);
        UIManager = GameObject.Find("UIManager").GetComponent<UIManagerScript>();
        StateManager = Camera.main.GetComponentInParent<StateManager>();
    }

    public override void BuildThis()
    {
        base.BuildThis();
        SceneManagerScript.SetResource();
        UIManager.OpenPage(0);
        Fortress.SetActive(true);
        Fortress.transform.parent = null;
        StateManager.StartStates();
    }

    public override void UpgradeThis()
    {
        base.UpgradeThis();
        UIManager.OpenPage(Level);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        if (Built) GameObject.Find("MainInterface - Canvas").GetComponent<PauseMenu>().OpenDefeatScreen(true, "Флагшток уничтожен");
    }
}