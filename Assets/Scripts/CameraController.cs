using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Cinemachine;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _controllerObject;

    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotateSpeed = 100f;


    [SerializeField] private float _zoomSpeed = 2f;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;


    private CinemachineTransposer _cinemachineTransposer;
    private Vector3 _targetFollowOffset;


    private void Start()
    {
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
    }

    // Update is called once per frame
    void Update()
    {
        // Handle movement
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection += _controllerObject.forward;

        if (Input.GetKey(KeyCode.S))
            moveDirection -= _controllerObject.forward;

        if (Input.GetKey(KeyCode.A))
            moveDirection -= _controllerObject.right;

        if (Input.GetKey(KeyCode.D))
            moveDirection += _controllerObject.right;

        _controllerObject.position += moveDirection * _moveSpeed * Time.deltaTime;


        Vector3 rotationVector = Vector3.zero;

        // Rotate counter-clockwise
        if (Input.GetKey(KeyCode.Q))
            rotationVector.y += 1 * _rotateSpeed * Time.deltaTime;

        // Rotate clockwise
        if (Input.GetKey(KeyCode.E))
            rotationVector.y -= 1 * _rotateSpeed * Time.deltaTime;

        _controllerObject.Rotate(rotationVector, Space.Self);


        //Zoom


        float minZoomCap = 2;
        float maxZoomCap = 20;

        float mouseDelta = Input.mouseScrollDelta.y;
        // Debug.Log(mouseDelta);
        // Determines whether to zoom in or out
        float zoomDirection = mouseDelta == 0 ? 0 : mouseDelta > 0 ? 1 : -1;

        if (zoomDirection != 0)
        {
            // Debug.Log(zoomDirection);
            //@remove Flip
            zoomDirection *= -1;
            Vector3 zoomedVector = _targetFollowOffset + new Vector3(0, 1, 0) * zoomDirection * _zoomSpeed;

            Vector3 nuFollowOffset =
                Vector3.Lerp(_targetFollowOffset, zoomedVector, Time.deltaTime);

            // Compute value in between the zoom limits

            _cinemachineTransposer.m_FollowOffset.y = Mathf.Clamp(nuFollowOffset.y, minZoomCap, maxZoomCap);
            _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
        }
    }
}