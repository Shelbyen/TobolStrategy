using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTerrainHeights : MonoBehaviour
{
    // public PaintTerrain paintTerrain;

    public int depth = 10;

    public int width = 256;
    public int height = 256;

    private HeightMapGenerator _heightMapGenerator;

    void Start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData.size = new Vector3(width, depth, height);
        _heightMapGenerator.Init(256);
        var map = _heightMapGenerator.Generate();
        // var waterLevel = HeightMapProvider.WaterLevel;

        terrain.terrainData.SetHeights(0, 0, map);

        // paintTerrain.StartPaint();

        // var waterObj = GameObject.Find("Water");
        // waterObj.transform.position = new Vector3(waterObj.transform.position.x, waterLevel * TerrainMain.terrainData.size.y, waterObj.transform.position.z);
    }
}
