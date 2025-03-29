using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemezovHut : Building
{
    public override void BuildThis()
    {
        ResourceManager.GetInstance().setMaxLv(3);
        base.BuildThis();
    }

    protected override void OnDestroy()
    {
        ResourceManager.GetInstance().setMaxLv(2);
        base.OnDestroy();
    }
}
