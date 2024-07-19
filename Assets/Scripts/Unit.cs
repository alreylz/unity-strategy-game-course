using UnityEngine;

/// <summary>
/// Creates 
/// </summary>
public class Unit : MonoBehaviour, IThreeDSelectable
{
    private GridPosition _ownGridPosition;


    //Things that the unit can do


    private MoveAction _moveAction;

    // private List<IAction> action;


    private void Start()
    {
        //Setearme respecto al grid del mundo inicialmente
        _ownGridPosition = LevelGrid.Instance.GetGridPosition(this.transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(this, _ownGridPosition);

        // Get all the things a unit can do 
        _moveAction = GetComponent<MoveAction>();
    }


    public MoveAction GetUnitMoveAction()
    {
        return _moveAction;
    }


    public GridPosition GetGridPosition() => _ownGridPosition;


    void Update()
    {
        // Update visuals / animations of unit

        //Update position in the grid if necessary
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (_ownGridPosition != gridPosition)
        {
            LevelGrid.Instance.ClearUnitAtGridPosition(this, _ownGridPosition);
            LevelGrid.Instance.SetUnitAtGridPosition(this, gridPosition);
            _ownGridPosition = gridPosition;
        }
    }
}