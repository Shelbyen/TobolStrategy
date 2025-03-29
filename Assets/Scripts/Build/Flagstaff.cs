using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flagstaff : House
{
    public GameObject Fortress;
    private StateManager StateManager;

    protected override void Awake()
    {
        base.Awake();
        Fortress.SetActive(false);
        StateManager = Camera.main.GetComponentInParent<StateManager>();
    }

    public override void BuildThis()
    {
        base.BuildThis();
        SceneManagerScript.SetResource();
        LinkManager.GetUIManager().OpenPage(0);
        Fortress.SetActive(true);
        Fortress.transform.parent = null;
        StateManager.StartStates();
    }

    public override void UpgradeThis()
    {
        base.UpgradeThis();
        LinkManager.GetUIManager().OpenPage(Level);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (Built) GameObject.Find("MainInterface - Canvas").GetComponent<PauseMenu>().OpenDefeatScreen(true, "Флагшток уничтожен");
    }
}