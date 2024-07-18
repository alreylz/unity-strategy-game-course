using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugGridObjectVisual : MonoBehaviour
{
    [SerializeField] private TextMeshPro _text;


    private GridObject _gridObject;

    public void SetGridObject(GridObject gridObject)
    {
        this._gridObject = gridObject;
    }


    void Start()
    {
        if (_text == null)
        {
            Debug.LogError(
                "DebugGridObjectVisual needs a TextMeshPro element assigned in the inspector to work properly" +
                this.transform);
            return;
        }
    }

    private void Update()
    {
        _text.SetText(_gridObject?.ToString()); 
    }
}