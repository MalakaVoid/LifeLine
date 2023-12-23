using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public float _sensitivityX;
    [SerializeField] private float _sensitivityY;

    private float _defaultSensX;
    private float _defaultSensY;

    private float _yaw;
    private float _pitch;

    public void ChangeSensitivity(float multiplier)
    {
        float t = multiplier == 1 ? multiplier : multiplier * 1.5f;

        _sensitivityX = _defaultSensX / t;
        _sensitivityY = _defaultSensY / t;
    }

    public void SetSensitivity(float x, float y)
    {
        _sensitivityX = x;
        _sensitivityY = y;

        _defaultSensX = _sensitivityX;
        _defaultSensY = _sensitivityY;
    }

    private void Start()
    {
        _sensitivityX = PlayerPrefs.GetFloat("SensitivityX");
        _sensitivityY = PlayerPrefs.GetFloat("SensitivityY");

        _defaultSensX = _sensitivityX;
        _defaultSensY = _sensitivityY;

        Cursor.lockState = CursorLockMode.Locked;
	    Cursor.visible = false;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        //if (Cursor.lockState != CursorLockMode.Locked) return;

        _pitch -= Input.GetAxis("Mouse Y") * _sensitivityY;
        _pitch = Mathf.Clamp(_pitch, -30, 40);

        _yaw += Input.GetAxisRaw("Mouse X") * _sensitivityX;
        _yaw = Mathf.Clamp(_yaw, -70, 70);

        transform.localRotation = Quaternion.Euler(_pitch, _yaw, 0);
    }
}
