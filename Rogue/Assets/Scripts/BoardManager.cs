using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {


    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    //public GameObject[] outerWallTiles;
    public GameObject upperOuterWallTile;
    public GameObject downOuterWallTile;
    public GameObject leftOuterWallTile;
    public GameObject rightOuterWallTile;
    //public GameObject[] cornerTiles;
    public GameObject upperLeftCornerTile;
    public GameObject upperRightCornerTile;
    public GameObject downLeftCornerTile;
    public GameObject downRightCornerTile;
    public GameObject[] exitDoor;
    public GameObject[] exitSign;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
                gridPositions.Add(new Vector3(x, y, 0f));

        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                {
                    if (x == -1)
                    {
                        toInstantiate = leftOuterWallTile;
                        if (y == -1)
                            toInstantiate = downLeftCornerTile;
                        if (y == rows)
                            toInstantiate = upperLeftCornerTile;
                    }
                    else if (x == columns-2 && y == rows)
                    {
                        toInstantiate = exitSign[0];
                    }
                    else if (x == columns-1 && y == rows)
                    {
                            toInstantiate = exitDoor[0];
                    }
                    else if (x == columns)
                    {
                        toInstantiate = rightOuterWallTile;
                        if (y == -1)
                            toInstantiate = downRightCornerTile;
                        if (y == rows)
                            toInstantiate = upperRightCornerTile;
                    }
                    else if(y == -1)
                    {
                        toInstantiate = downOuterWallTile;
                    }
                    else if(y == rows)
                    {
                        toInstantiate = upperOuterWallTile;
                    }
                }
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }              
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for(int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }
}
