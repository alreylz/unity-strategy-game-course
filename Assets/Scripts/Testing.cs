using UnityEngine;

public class Testing : MonoBehaviour
{
    private GridSystem _gridSystem;


    [SerializeField] private Transform debugCellPrefab;


    void Start()
    {
        _gridSystem = new GridSystem(10, 10);
        if (debugCellPrefab)
            _gridSystem.CreateDebugObjects(debugCellPrefab, transform.root);
    }

    void Update()
    {
        InputController.GetMouseWorldPosition(out Vector3 mousePosition);

        Debug.Log($"The mouse in grid position : {_gridSystem.GetGridPosition(mousePosition)}");
    }
}