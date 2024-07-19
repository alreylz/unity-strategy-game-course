using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the grid instance in the scene of a given level.
/// Offers methods to interact with the contents of the grid cells.
/// </summary>
public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    private GridSystem _gridSystem;

    /// <summary>
    /// Visual prefab to show data about the grid cells in the Scene 
    /// </summary>
    [SerializeField] private Transform debugCellPrefab;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning("LevelGrid is a Singleton and more than one instance has been found " + this.transform);
            Destroy(this);
        }
    }


    void Start()
    {
        //Create the grid for a given level
        _gridSystem = new GridSystem(10, 10);
        if (debugCellPrefab)
            _gridSystem.CreateDebugObjects(debugCellPrefab, transform);
    }


    public void SetUnitAtGridPosition(Unit unit, GridPosition gridPosition)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);

        // gridObject.AddUnit(unit);
        gridObject.AddUnitJustOnce(unit); // only adds the element as content of the grid if it's not in there already
    }


    public List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
    {
        return _gridSystem.GetGridObject(gridPosition).GetUnits();
    }

    public void ClearUnitAtGridPosition(Unit unit, GridPosition gridPosition)
    {
        GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
        gridObject.ClearUnit(unit);
    }


    public GridPosition GetGridPosition(Vector3 worldPosition) => _gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => _gridSystem.GetWorldPosition(gridPosition);


    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return _gridSystem.IsValidGridPosition(gridPosition);
    }

    public bool IsEmptyGridPosition(GridPosition gridPosition)
    {
        return _gridSystem.GetGridObject(gridPosition).HasAnyUnit();
    }
}