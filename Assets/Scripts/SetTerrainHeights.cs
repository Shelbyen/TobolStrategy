using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTerrainHeights : MonoBehaviour
{
    public int permission = 6;

    public int width = 1000;
    public int height = 1000;
    public int depth = 15;

    public bool isUpdate = false;
    private HeightMapGenerator _heightMapGenerator;

    private void Start() {
        if (isUpdate) {
            StartCoroutine(Timer());
        }
        else {
            _heightMapGenerator = new HeightMapGenerator();
            Terrain terrain = GetComponent<Terrain>();
            _heightMapGenerator.Init(permission, baseHeight: depth);
            terrain.terrainData = GenerateTerrain(terrain.terrainData); 
        }
    }

    public IEnumerator Timer()
    {
        while (true) {
            yield return new WaitForSeconds(3);

            _heightMapGenerator = new HeightMapGenerator();
            Terrain terrain = GetComponent<Terrain>();
            _heightMapGenerator.Init(permission, baseHeight: depth);
            terrain.terrainData = GenerateTerrain(terrain.terrainData); 
        }
    }

    TerrainData GenerateTerrain (TerrainData terrainData) {
        terrainData.heightmapResolution = (int)Mathf.Pow(2, permission) + 1;
        terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, _heightMapGenerator.Generate());

        return terrainData;
    }
}
