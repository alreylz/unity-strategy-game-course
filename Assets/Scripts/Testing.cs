using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit chosenUnit;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log($"Printing valid actions for unit {chosenUnit}");
            List<GridPosition> gridPositions = chosenUnit.GetUnitMoveAction().GetValidActionGridPositionList();
            foreach (var pos in gridPositions)
            {
                Debug.Log(pos);
            }
        }
    }
}