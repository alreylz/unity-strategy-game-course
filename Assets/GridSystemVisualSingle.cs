using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;


    private void Awake()
    {
        if (!_renderer)
            _renderer = GetComponent<MeshRenderer>();
        if (!_renderer)
            _renderer = GetComponentInChildren<MeshRenderer>();
    }


    public void SetActive(bool status)
    {
        _renderer.enabled = status;
    }
}