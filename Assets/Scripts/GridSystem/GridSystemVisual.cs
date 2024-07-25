using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles the visuals for the grid in the level (showing a square in each cell)
/// </summary>
public class GridSystemVisual : MonoBehaviour
{
    [SerializeField] private Transform _gridVisualSinglePrefab;


    /// <summary>
    /// Keeps track of all visual objects in the grid
    /// </summary>
    private GridSystemVisualSingle[,] _gridObjects;


    private LevelGrid _levelGrid;

    // Start is called before the first frame update
    void Start()
    {
        _levelGrid = FindObjectOfType<LevelGrid>();

        _gridObjects = new GridSystemVisualSingle [LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < _levelGrid.GetWidth(); x++)
        {
            for (int z = 0; z < _levelGrid.GetHeight(); z++)
            {
                Transform gridObjectTransform = Instantiate(_gridVisualSinglePrefab,
                    LevelGrid.Instance.GetWorldPosition(new GridPosition(x, z)),
                    Quaternion.identity, transform);
                _gridObjects[x, z] = gridObjectTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
    }


    private void Update()
    {
        HideAllGridPositionVisuals();
        Unit unit = UnitActionSystem.Instance.GetSelectedUnit();
        List<GridPosition> allowedMovementGridPositions = unit.GetUnitMoveAction().GetValidActionGridPositionList();
        ShowGridPositionVisualSet(allowedMovementGridPositions);
    }


    public void HideAllGridPositionVisuals()
    {
        foreach (var gridObject in _gridObjects)
        {
            gridObject.SetActive(false);
        }
    }

    public void ShowGridPositionVisualSet(GridPosition[] gridPositions)
    {
        foreach (var position in gridPositions)
        {
            ShowGridPositionVisual(position);
        }
    }

    public void ShowGridPositionVisualSet(List<GridPosition> gridPositions)
    {
        foreach (var position in gridPositions)
        {
            ShowGridPositionVisual(position);
        }
    }

    private void ShowGridPositionVisual(GridPosition gridPosition)
    {
        if (!LevelGrid.Instance.IsValidGridPosition(gridPosition))
        {
            Debug.LogError("Trying to show grid position visual for an Invalid grid position");
            return;
        }

        _gridObjects[gridPosition.x, gridPosition.z].SetActive(true);
    }
}