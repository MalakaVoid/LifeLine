using UnityEngine;
using UnityEngine.UI;

public class SensitivityController : MonoBehaviour
{
    [SerializeField] private Slider _xSensSlider;
    [SerializeField] private Slider _ySensSlider;

    private CameraController _cameraController;

    private void Awake()
    {
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("SensitivityX"))
            PlayerPrefs.SetFloat("SensitivityX", 2f);

        if (!PlayerPrefs.HasKey("SensitivityY"))
            PlayerPrefs.SetFloat("SensitivityY", 2f);

        LoadValues(1);
        LoadValues(2);
    }

    public void SaveSens(int id)
    {
        if (id == 1)
        {
            float sensitivityValue = _xSensSlider.value;
            PlayerPrefs.SetFloat("SensitivityX", sensitivityValue);
            LoadValues(1);
        }

        if (id == 2)
        {
            float sensitivityValue = _ySensSlider.value;
            PlayerPrefs.SetFloat("SensitivityY", sensitivityValue);
            LoadValues(2);
        }

        _cameraController.SetSensitivity(_xSensSlider.value, _ySensSlider.value);
    }

    private void LoadValues(int id)
    {
        if (id == 1)
        {
            float sensitivityValue = PlayerPrefs.GetFloat("SensitivityX");
            _xSensSlider.value = sensitivityValue;
        }

        if (id == 2)
        {
            float sensitivityValue = PlayerPrefs.GetFloat("SensitivityY");
            _ySensSlider.value = sensitivityValue;
        }
    }
}
