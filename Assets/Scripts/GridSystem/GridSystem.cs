using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class GridSystem
{
    private int _width;

    private int _height;
    // private int floor;

    /// <summary>
    /// Defines correspondence between 3d units and grid units ( 1 unit = _cellsize * unit)
    /// </summary>
    private float _cellSize;


    /// <summary>
    /// Represents each cell in the grid system
    /// </summary>
    private GridObject[,] _gridObjectsArray;


    public GridSystem(int width, int height, float cellSize = 2f)
    {
        this._width = width;
        this._height = height;
        this._cellSize = cellSize;

        _gridObjectsArray = new GridObject[this._width, this._height];


        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                this._gridObjectsArray[x, z] = new GridObject(this, new GridPosition(x, z));
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * .2f, Color.white, 1000);
            }
        }
    }


    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return _gridObjectsArray[gridPosition.x, gridPosition.z];
    }
    

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x >= 0 &&
               gridPosition.x < _width &&
               gridPosition.z >= 0 &&
               gridPosition.x < _height;
    }
    
    public int GetWidth() => _width;
    public int GetHeight() => _height;


    // Converts grid coordinates in real 3d world coordinates
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * _cellSize;
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return GetWorldPosition(gridPosition.x, gridPosition.z);
    }


    //Converts a 3d position in the world to the grid position
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / _cellSize),
            Mathf.RoundToInt(worldPosition.z / _cellSize));
    }

    public void CreateDebugObjects(Transform debugPrefab, Transform hierarchyParent)
    {
        for (int x = 0; x < this._width; x++)
        {
            for (int z = 0; z < this._height; z++)
            {
                Transform debugCellObject =
                    GameObject.Instantiate(debugPrefab, GetWorldPosition(x, z), Quaternion.identity);
                debugCellObject.parent = hierarchyParent;
                DebugGridObjectVisual visual = debugCellObject.GetComponent<DebugGridObjectVisual>();
                visual.SetGridObject(_gridObjectsArray[x, z]);
            }
        }
    }
}