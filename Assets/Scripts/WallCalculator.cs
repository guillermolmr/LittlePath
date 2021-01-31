using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallCalculator : MonoBehaviour
{

  public static WallCalculator instance;
  public bool[,] walls;
  public bool[,] adjMatrix;
  private Vector3Int min;
  public Vector3Int[] directions;
  public Transform player;
  public Vector3Int getMin()
  {
    return min;
  }
  [SerializeField]
  public Tilemap tilemapWalls;
  public BoundsInt bounds;
  void Start()
  {
    instance = this;
    bounds = tilemapWalls.cellBounds;
    CalculateWalls();
    directions = new Vector3Int[4] { new Vector3Int(0, -1, 0), new Vector3Int(1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(-1, 0, 0) };
  }

  public void CalculateWalls()
  {

    Vector3Int size = tilemapWalls.cellBounds.size;
    min = tilemapWalls.cellBounds.min;

    walls = new bool[size.y, size.x];
    Vector3Int position = new Vector3Int(0, 0, 0);
    //string s = "";
    for (int j = 0; j < size.y; j++)
    {
      for (int i = 0; i < size.x; i++)
      {
        position.x = i + min.x;
        position.y = j + min.y;

        walls[j, i] = tilemapWalls.HasTile(position);
        //s += (walls[j, i]) ? "X" : " ";

      }
      //s += "\n";
    }
    //Debug.Log(s);

    int n = size.y * size.x;

    /*adjMatrix = new bool[n,n];
    
    for(int i = 0; i < n; i++)
    {
      int x = i % size.x;
      int y = i / size.x;
      adjMatrix[i, i] = false;
      if (x > 0 && !walls[y, x - 1]) adjMatrix[n, n - 1] = true;
      if (size.x-1 > x && !walls[y, x + 1]) adjMatrix[n, n + 1] = true;
      if (y > 0 && !walls[y-1,x]) adjMatrix[n, n -size.x] = true;
      if (size.y-1 > y && !walls[y+1, x]) adjMatrix[n, n + -size.x] = true;

    }*/
    /**
     *0 up,
     *1, right
     *2, down,
     *3 left
     */
    adjMatrix = new bool[n, 4];
    for (int i = 0; i < n; i++)
    {
      int x = i % size.x;
      int y = i / size.x;


      adjMatrix[i, 0] = (y > 0 && !walls[y - 1, x]);
      adjMatrix[i, 1] = (size.x - 1 > x && !walls[y, x + 1]);
      adjMatrix[i, 2] = (size.y - 1 > y && !walls[y + 1, x]);
      adjMatrix[i, 3] = (x > 0 && !walls[y, x - 1]);
      
      
      
    }
  }



}
