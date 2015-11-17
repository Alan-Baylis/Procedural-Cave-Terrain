using UnityEngine;
using System.Collections;
using System;

//Celular Automata
public class GenerateTerrain : MonoBehaviour {

    public GameObject tile;

    public int width, height;

    public string seed;
    public bool useRandomSeed;

    public GameObject tileParent;

    [Range(0,100)]
    public int randomFillPercent;

    int[,] map;

	// Use this for initialization
	void Start () 
    {
        GenerateMap();
	}

    private void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }

        InstantiateTiles();
    }

    private void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = System.DateTime.Now.ToString();
        }

        System.Random psuedoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }else
                {
                    if (psuedoRandom.Next(0, 100) < randomFillPercent)
                    {
                        map[x, y] = 1;
                    }
                    else
                    {
                        map[x, y] = 0;
                    }
                }
                
            }
        }

    }

    private void SmoothMap() 
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighnourWallTiles = GetSurroundingWallCount(x, y);
                if (neighnourWallTiles > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighnourWallTiles < 4)
                {
                    map[x, y] = 0;
                }
            }
        }

        

    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX -1 ; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else 
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void InstantiateTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

                if (map[x, y] == 1)
                {
                    GameObject terrainTile = Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                    terrainTile.transform.parent = tileParent.transform;
                }
                
            }
        }
    }

    private void ClearMap()
    {
        foreach(Transform child in tileParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClearMap();
            GenerateMap();
        }
	}
}
