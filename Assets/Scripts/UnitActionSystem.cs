using System;
using UnityEngine;


/// <summary>
/// Handles the Selection of Units and the actions they can take
/// </summary>
public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit _selectedUnit;


    public event EventHandler OnSelectedUnitChanged;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("UnitActionSystem is a Singleton but more instances were found " + this.transform);
            Destroy(this);
        }
    }


    void Start()
    {
        _selectedUnit = null;
    }

    void Update()
    {
        // LEFT MOUSE BUTTON (Unit selection)
        if (Input.GetMouseButtonDown(0))
        {
            HandleUnitSelection();
        }

        // RIGHT MOUSE BUTTON for moving the selected unit (if any)
        if (Input.GetMouseButtonDown(1))
        {
            HandleUnitMovement();
        }
    }


    void SetSelectedUnit(Unit unit)
    {
        _selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetSelectedUnit()
    {
        return _selectedUnit;
    }


    void HandleUnitMovement()
    {
        Debug.Log("Right mouse button pressed");
        if (InputController.GetMouseWorldPosition(out Vector3 worldPosSelected))
            _selectedUnit?.Move(worldPosSelected);
    }

    void HandleUnitSelection()
    {
        Debug.Log("Left mouse button pressed");

        Unit unit = InputController.GetMouseSelectable<Unit>();
        if (unit != _selectedUnit)
            SetSelectedUnit(unit);

        if (_selectedUnit)
            Debug.Log($"Successfully selected a Unit {_selectedUnit?.gameObject.name}");
    }
}