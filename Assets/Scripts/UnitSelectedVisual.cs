using System;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit _unit;
    private MeshRenderer _meshRenderer;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }


    private void Start()
    {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
    }


    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
    {
        bool isSelectedThisUnit = UnitActionSystem.Instance.GetSelectedUnit() == _unit;
        _meshRenderer.enabled = isSelectedThisUnit;
    }
}