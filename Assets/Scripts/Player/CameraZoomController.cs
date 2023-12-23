using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera), typeof(CameraController))]
public class CameraZoomController : MonoBehaviour
{
    [SerializeField] private float _defaultFov = 90;
    [SerializeField] private float _closeFov = 50;
    [Space(5)]

    [SerializeField] private bool _zoom = true;

    [SerializeField] private float _zoomTime;
    [SerializeField] private float _zoomedPosition = 1.2f;
    [SerializeField] private float _defaultPosition = -1.165f;

    private Camera _camera;
    private CameraController _cameraController;

    private bool _zoomed = false;

    private void Awake() 
    { 
        _camera = GetComponent<Camera>();
        _cameraController = GetComponent<CameraController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _zoomed = !_zoomed;

            if (_zoom == false) _camera.transform.DOMoveX(_zoomed ? _zoomedPosition : _defaultPosition, _zoomTime);
            else _camera.DOFieldOfView(_zoomed ? _closeFov : _defaultFov, _zoomTime);
        }

        ChangeFov();
    }

    private void ChangeFov()
    {
        _cameraController.ChangeSensitivity(_defaultFov / _camera.fieldOfView);
        _cameraController.ChangeSensitivity(_defaultFov / _camera.fieldOfView);
    }
}
