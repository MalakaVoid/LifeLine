using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer _master;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start()
    {
        LoadValues(1);
        LoadValues(2);
        LoadValues(3);
        LoadValues(4);
    }

    public void SaveVolume(int id)
    {
        switch (id)
        {
            case 1:
                if (_masterSlider.value <= .11f)
                {
                    _master.SetFloat("Master_Volume", -80);
                    PlayerPrefs.SetFloat("Master Volume", _masterSlider.value);
                }
                else
                {
                    _master.SetFloat("Master_Volume", Mathf.Log10(_masterSlider.value) * 20);
                    PlayerPrefs.SetFloat("Master Volume", _masterSlider.value);
                }
                break;
            case 2:
                if (_musicSlider.value <= .11f)
                {
                    _master.SetFloat("Music_Volume", -80);
                    PlayerPrefs.SetFloat("Music Volume", _musicSlider.value);
                }
                else
                {
                    _master.SetFloat("Music_Volume", Mathf.Log10(_musicSlider.value) * 20);
                    PlayerPrefs.SetFloat("Music Volume", _musicSlider.value);
                }
                break;
            case 3:
                if (_sfxSlider.value <= .11f)
                {
                    _master.SetFloat("SFX_Volume", -80);
                    PlayerPrefs.SetFloat("SFX Volume", _sfxSlider.value);
                }
                else
                {
                    _master.SetFloat("SFX_Volume", Mathf.Log10(_sfxSlider.value) * 20);
                    PlayerPrefs.SetFloat("SFX Volume", _sfxSlider.value);
                }
                break;
        }
    }

    private void LoadValues(int id)
    {
        switch (id)
        {
            case 1:
                if (!PlayerPrefs.HasKey("Master Volume"))
                {
                    _masterSlider.value = 1;
                    _master.SetFloat("Master_Volume", Mathf.Log10(_masterSlider.value) * 20);
                }
                else
                {
                    _masterSlider.value = PlayerPrefs.GetFloat("Master Volume");
                    if (_masterSlider.value <= .11f)
                        _master.SetFloat("Master_Volume", -80);
                    else
                        _master.SetFloat("Master_Volume", Mathf.Log10(_masterSlider.value) * 20);
                }
                break;
            case 2:
                if (!PlayerPrefs.HasKey("Music Volume"))
                {
                    _musicSlider.value = 1.5f;
                    _master.SetFloat("Music_Volume", Mathf.Log10(_musicSlider.value) * 20);
                }
                else
                {
                    _musicSlider.value = PlayerPrefs.GetFloat("Music Volume");
                    if (_musicSlider.value <= .11f)
                        _master.SetFloat("Music_Volume", -80);
                    else
                        _master.SetFloat("Music_Volume", Mathf.Log10(_musicSlider.value) * 20);
                }
                break;
            case 3:
                if (!PlayerPrefs.HasKey("SFX Volume"))
                {
                    _sfxSlider.value = 2;
                    _master.SetFloat("SFX_Volume", Mathf.Log10(_sfxSlider.value) * 20);
                }
                else
                {
                    _sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume");
                    if (_sfxSlider.value <= .11f)
                        _master.SetFloat("SFX Volume", -80);
                    else
                        _master.SetFloat("SFX_Volume", Mathf.Log10(_sfxSlider.value) * 20);
                }
                break;
        }
    }
}
