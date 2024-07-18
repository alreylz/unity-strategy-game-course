using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private GameObject _pointerVisual;

    [SerializeField] private LayerMask _controllerMasks;


    private static InputController _instance;

    private void Awake()
    {
        if (!_pointerVisual)
        {
            GameObject pointerVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointerVisual.transform.localScale *= 0.3f;
            _pointerVisual = pointerVisual;
        }

        // Singleton
        if (!_instance)
            _instance = this;
        else
            Destroy(this);
    }


    private void Update()
    {
        HandleInput();
    }


    public static T GetMouseSelectable<T>() where T : Component, IThreeDSelectable
    {
        Camera cam = Camera.main;
        if (!cam)
        {
            Debug.LogWarning("Camera not found in the scene");
            return null;
        }

        // Send a raycast from camera towards the scene and check whether it hits an object
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, _instance._controllerMasks))
        {
            Debug.Log($"MouseSelectable Raycast hit something {raycastHit.collider}");


            return raycastHit.transform.GetComponent<T>();
        }

        return null;
    }


    public static bool GetMouseWorldPosition(out Vector3 position)
    {
        position = Vector3.negativeInfinity;

        Camera cam = Camera.main;
        if (!cam)
        {
            Debug.LogWarning("Camera not found in the scene");
            return false;
        }

        // Send a raycast from camera towards the scene and check whether it hits an object
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float maxDistance = 10f;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistance, _instance._controllerMasks))
        {
            position = raycastHit.point;
            return true;
        }

        return false;
    }


    private void HandleInput()
    {
        //Get mouse position / cursor postion
        if (GetMouseWorldPosition(out Vector3 position))
        {
            _pointerVisual.SetActive(true);
            _pointerVisual.transform.position = position;
        }
        else
            _pointerVisual.SetActive(false);
    }
}