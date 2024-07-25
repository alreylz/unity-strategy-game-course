using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles moving a unit somewhere (also animation of it
/// </summary>
public class MoveAction : MonoBehaviour
{
    private Vector3 _targetPosition;


    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _stoppingDistance = 0.1f;

    [SerializeField] private Unit _unit;
    [SerializeField] private Animator _unitAnimator;
    private bool _isMoving;


    [SerializeField] private int maxMoveRange = 2;


    private void Awake()
    {
        _targetPosition = transform.position;
        _isMoving = false;
    }

    void Start()
    {
        _unit = GetComponent<Unit>();
        _unitAnimator.SetBool("Moving", _isMoving);
        if (!_unitAnimator)
            _unitAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleMovement();
        _unitAnimator.SetBool("Moving", _isMoving);
    }


    // Exposes the ability to move the unit
    // public void Move(Vector3 targetPosition)
    // {
    //     this._targetPosition = targetPosition;
    // }

    //  Sets target in Grid coordinates (converting to world coordinates for actually moving
    public void Move(GridPosition targetPosition)
    {
        this._targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
    }


    /// <summary>
    /// For a given Unit, gets the positions that constitute a valid movement in the grid
    /// </summary>
    /// <returns></returns>
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositions = new List<GridPosition>();


        GridPosition unitGridPosition = _unit.GetGridPosition();

        Debug.Log("Evaluating grid positions");

        for (int x = -maxMoveRange; x <= maxMoveRange; x++)
        {
            for (int z = -maxMoveRange; z <= maxMoveRange; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);

                GridPosition evaluatedGridPosition = unitGridPosition + offsetGridPosition;


                // Debug.Log(evaluatedGridPosition);

                //Check if is valid cell position
                if (!LevelGrid.Instance.IsValidGridPosition(evaluatedGridPosition))
                    continue;

                //If its the Unit's own position (don't allow move)
                if (unitGridPosition == evaluatedGridPosition)
                    continue;

                if (LevelGrid.Instance.IsEmptyGridPosition(evaluatedGridPosition))
                    continue;

                validGridPositions.Add(evaluatedGridPosition);
            }
        }

        return validGridPositions;
    }


    // To be called in each frame to update movement
    void HandleMovement()
    {
        if (Vector3.Distance(transform.position, _targetPosition) > _stoppingDistance)
        {
            Vector3 directionVector = (_targetPosition - transform.position).normalized;
            _isMoving = true;
            transform.position += directionVector * Time.deltaTime * _moveSpeed;
            transform.forward = directionVector;
        }
        else
        {
            _isMoving = false;
        }
    }
}