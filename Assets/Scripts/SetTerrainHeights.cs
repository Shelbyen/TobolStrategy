using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTerrainHeights : MonoBehaviour
{
    public Terrain TerrainMain;
    // public PaintTerrain paintTerrain;

    private HeightMapGenerator _heightMapGenerator;

    void Start()
    {
        _heightMapGenerator.Init(256);
        var map = HeightMapGenerator.Generate();
        // var waterLevel = HeightMapProvider.WaterLevel;

        TerrainMain.terrainData.SetHeights(0, 0, map);

        // paintTerrain.StartPaint();

        // var waterObj = GameObject.Find("Water");
        // waterObj.transform.position = new Vector3(waterObj.transform.position.x, waterLevel * TerrainMain.terrainData.size.y, waterObj.transform.position.z);
    }
}
