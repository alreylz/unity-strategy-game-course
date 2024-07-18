using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Unit : MonoBehaviour, IThreeDSelectable
{
    private Vector3 _targetPosition; // Indica hacia el punto que se ha de mover el objeto.
    private bool _isMoving = false;
    private Animator _unitAnimator;

    private GridPosition _ownGridPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
        _unitAnimator = GetComponentInChildren<Animator>();
        _unitAnimator.SetBool("Moving", _isMoving);
    }

    private void Start()
    {
        //Setearme respecto al grid del mundo inicialmente
        _ownGridPosition = LevelGrid.Instance.GetGridPosition(this.transform.position);
        LevelGrid.Instance.SetUnitAtGridPosition(this, _ownGridPosition);
    }


    void Update()
    {
        float moveSpeed = 4f;


        float stoppingDistance = .1f;

        if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
        {
            Vector3 directionVector = (_targetPosition - transform.position).normalized;
            _isMoving = true;
            transform.position += directionVector * Time.deltaTime * moveSpeed;
            transform.forward = directionVector;
        }
        else
        {
            _isMoving = false;
        }


        // Update visuals / animations of unit
        _unitAnimator.SetBool("Moving", _isMoving);

        //Update position in the grid if necessary
        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (_ownGridPosition != gridPosition)
        {
            LevelGrid.Instance.ClearUnitAtGridPosition(this, _ownGridPosition);
            LevelGrid.Instance.SetUnitAtGridPosition(this, gridPosition);
            _ownGridPosition = gridPosition;
        }
    }


    public void Move(Vector3 targetPosition)
    {
        this._targetPosition = targetPosition;
    }

    //@todo: remove
    public void FaceTarget()
    {
        Quaternion lookRotation =
            Quaternion.LookRotation((_targetPosition - transform.position).normalized, Vector3.up);
        float remAngle = Quaternion.Angle(lookRotation, transform.rotation);
        if (remAngle > 1)
        {
            Debug.Log(
                $"Angle between target and current rotation: {Quaternion.Angle(lookRotation, transform.rotation)}");

            float refaceSpeed = 2f;
            // Rotate to face target
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, refaceSpeed * Time.deltaTime);
        }
    }
}