using UnityEngine;
using UnityEngine.UI;

public class GraphicsController : MonoBehaviour
{
    [SerializeField, ColorUsage(false, true)] private Color _ambientDarkest;
    [SerializeField, ColorUsage(false, true)] private Color _ambientLightest;
    [Space(5)]

    [SerializeField] private Slider _slider;

    private void Start()
    {
        LoadValues();
    }

    private void Update()
    {
        RenderSettings.ambientLight = Color.Lerp(_ambientDarkest, _ambientLightest, _slider.value);
    }

    public void SaveValues()
    {
        float brightValue = _slider.value;
        PlayerPrefs.SetFloat("Brightness", brightValue);
        LoadValues();
    }



    private void LoadValues()
    {
        float brightValue = PlayerPrefs.GetFloat("Brightness");
        _slider.value = brightValue;


    }
}
