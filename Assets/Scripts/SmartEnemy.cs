using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;




public class SmartEnemy : MonoBehaviour
{
  [SerializeField]
  private float distanceView = 4.5f;
   [SerializeField]
  private float timeStuned=1.0f;
  public Event debugar;
  public List<Transform> points;
  public Vector3[] pointPositions;
  private Vector3[] pathToPoint;
  private int curPathPoint = 0;
  [SerializeField]
  private float Speed_ = 0.0f;
  [SerializeField]
  private float Sprint_ = 1.5f;
  private int targetPoint = 0;
  [SerializeField]
  private float radius = 0.3f;
  private bool calculatedPath = false;
  private Vector3 Velocity_ = new Vector3();
  private Transform t;
  private int behaviour = 0;
  private Vector3 lastSeen;
  private bool seen = false;
  private Vector3 lastPosition;
  private float stuned = 1.0f;
  // Start is called before the first frame update
  void Start()
  {
    t = transform;
    pointPositions = new Vector3[points.Count];
    int i = 0;
    foreach (Transform p in points)
    {
      pointPositions[i] = p.position;
      i++;
    }
    calculatedPath = false;
    lastPosition = t.position;
  }

  // Update is called once per frame
  Vector3[] camino = null;
  void Update()
  {
    Vector3 myDir = t.position - lastPosition;
    lastPosition = t.position;
    Vector3 playerDir = WallCalculator.instance.player.position - t.position;
    playerDir.Normalize ();
    float disPlayer = Vector3.Distance(WallCalculator.instance.player.position, t.position);
    float anglePlayer = Vector3.Angle(myDir, playerDir);
    if (disPlayer <= distanceView && Mathf.Abs(anglePlayer) <45)
    {
      seen = true;
      behaviour = 1;
      /*
      RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDir);
      if (hit.collider != null && hit.collider.CompareTag("Player"))
      {
        
      }
      else
      {
        seen = false;
      }
      */

    }
    else
    {
      seen = false;
    }
    switch (behaviour)
    {
      case 0:
        Patrolling();
        break;
      case 1:

        Chase();
        break;
    }


    if (pathToPoint != null)
      for (int i = 0; i < pathToPoint.Length - 1; i++)
      {
        Debug.DrawLine(pathToPoint[i], pathToPoint[i + 1], Color.red);

      }
    if (stuned < timeStuned)
    {
      stuned += Time.deltaTime;
    }
    else if(stuned> timeStuned)
    {
      stuned = timeStuned;
    }
    if (stuned==timeStuned)
    {
      BroadcastMessage("UnStun");
    }
  }

  void Chase()
  {

    if (seen)
    {
      if (stuned>0&&Vector3.Distance(WallCalculator.instance.player.position,t.position)>1.0f)
      {
        Velocity_ = WallCalculator.instance.player.position - t.position;
        Velocity_.Normalize();
        t.position += Velocity_ * Speed_ * Time.deltaTime * Sprint_ * stuned;
        t.rotation = Quaternion.identity;
      }
      
      

    }
    else
    {
      lastSeen = WallCalculator.instance.player.position;
      pathToPoint = nextTargetTile(lastSeen);
      calculatedPath = true;
      behaviour = 0;
    }
  }

  void Patrolling()
  {
    if (!calculatedPath)
    {

      pathToPoint = nextTargetTile(pointPositions[targetPoint]);
      calculatedPath = true;
      curPathPoint = 0;
    }
    else if (Vector3.Distance(t.position, pointPositions[targetPoint]) < radius)
    {
      targetPoint = (targetPoint < pointPositions.Length - 1) ? targetPoint + 1 : 0;
      calculatedPath = false;
    }
    else
    {
      if (pathToPoint.Length<= curPathPoint)
      {
        calculatedPath = false;
        targetPoint = (targetPoint < pointPositions.Length - 1) ? targetPoint + 1 : 0;
      }
      else if (Vector3.Distance(t.position, pathToPoint[curPathPoint]) < radius)
      {
        if (curPathPoint < pathToPoint.Length - 1)
        {
          curPathPoint++;
        }
        else
        {
          curPathPoint = 0;
          calculatedPath = false;
          targetPoint = (targetPoint < pointPositions.Length - 1) ? targetPoint + 1 : 0;
        }
      }
      if(pathToPoint.Length <= curPathPoint)
      {
        calculatedPath = false;
        targetPoint = (targetPoint < pointPositions.Length - 1) ? targetPoint + 1 : 0;
      }
      else
      {
        Velocity_ = pathToPoint[curPathPoint] - t.position;
        Velocity_.Normalize();
        t.position += Velocity_ * Speed_ * Time.deltaTime* stuned;
        t.rotation = Quaternion.identity;
      }
      
    }
  }
  List<Explorer> explorers = new List<Explorer>();
  List<Explorer> nextExplorers = new List<Explorer>();
  List<Explorer> oldExplorers = new List<Explorer>();
  List<Vector3Int> used = new List<Vector3Int>();
  private Vector3[] nextTargetTile(Vector3 target)
  {

    Vector3[] result = null;
    Vector3Int min = WallCalculator.instance.getMin();
    Vector3Int myTilePos = WallCalculator.instance.tilemapWalls.WorldToCell(t.position) - min;
    Vector3Int targetTilePos = WallCalculator.instance.tilemapWalls.WorldToCell(target) - min;
    // grid = new int[WallCalculator.instance.bounds.size.y, WallCalculator.instance.bounds.size.x,3];

    explorers = new List<Explorer>();
    nextExplorers = new List<Explorer>();
    oldExplorers = new List<Explorer>();
    used = new List<Vector3Int>();
    used.Add(myTilePos);

    explorers.Add(new Explorer(myTilePos));
    bool found = false;
    Explorer theOne = null;
    //int debu = 0;
    while (explorers.Count > 0)
    {
      foreach (Explorer e in explorers)
      {

        int n = e.pos.x + e.pos.y * WallCalculator.instance.tilemapWalls.cellBounds.size.x;
        for (int i = 0; i < 4; i++)
        {
          Vector3Int newPos = e.pos + WallCalculator.instance.directions[i];
          //debu++;
          if (newPos.y < 0)
            Debug.Log("num");

          if (WallCalculator.instance.adjMatrix[n, i] && !used.Contains(newPos))
          {
            oldExplorers.Add(e);
            nextExplorers.Add(new Explorer(newPos, e));
            used.Add(newPos);



          }
          if (newPos != targetTilePos)
          {
            //nothing
          }
          else
          {
            theOne = e;
            found = true;
            goto TargetFound;
          }

        }

      }
      //explorers.Clear();

      // List<Explorer> temp = explorers;
      explorers = new List<Explorer>(nextExplorers.ToArray());
      //nextExplorers = temp;
      nextExplorers = new List<Explorer>();
    }
  TargetFound:
    if (found)
    {
      List<Vector3> path = new List<Vector3>();
      int n = WallCalculator.instance.adjMatrix.Length;
      Vector3 toCenter = new Vector3(0.5f, 0.5f);
      for (int i = 0; theOne != null && i < n; i++)
      {
        path.Add(WallCalculator.instance.tilemapWalls.CellToWorld(theOne.pos + min) + toCenter);
        theOne = theOne.previous;
      }
      path.Reverse();
      return path.ToArray();
    }


    //result = myTilePos;

    return null;
  }
  class Explorer
  {

    public Vector3Int pos;
    public Explorer previous;
    public Explorer(Vector3Int pos, Explorer previous)
    {
      this.pos = pos;
      this.previous = previous;
    }
    public Explorer(Vector3Int pos)
    {
      this.pos = pos;
    }
  }
  public void Stun()
  {
    stuned = 0;
    
  }

}
