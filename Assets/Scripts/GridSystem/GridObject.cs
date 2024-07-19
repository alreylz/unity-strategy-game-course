using System.Collections.Generic;

/// <summary>
/// Objeto instanciado en cada posición del grid (cada celda),
/// indica contenido de la misma
/// </summary>
public class GridObject
{
    private GridPosition _gridPosition;
    private GridSystem _gridSystem;
    private List<Unit> _unitList;


    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        _unitList = new List<Unit>();
        this._gridSystem = gridSystem;
        this._gridPosition = gridPosition;
    }


    public void AddUnit(Unit unit)
    {
        _unitList.Add(unit);
    }

    public void AddUnitJustOnce(Unit unit)
    {
        if (!_unitList.Contains(unit))
            _unitList.Add(unit);
    }

    public void ClearUnit(Unit unit)
    {
        _unitList.Remove(unit);
    }


    public bool HasAnyUnit()
    {
        return _unitList.Count > 0;
    }
    
    public List<Unit> GetUnits() => _unitList;


    public override string ToString()
    {
        string allUnits = "";
        foreach (var unit in _unitList)
        {
            allUnits += $"{unit.name}\n";
        }

        return $"{_gridPosition.ToString()}\n" + allUnits;
    }
}