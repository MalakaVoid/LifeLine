using UnityEngine;
using UnityEngine.UI;

public class FrenzyDisplayController : MonoBehaviour
{
    [SerializeField] private Image[] _barImages;
    [SerializeField] private Color[] _colors;

    private int _activeAmount;

    private void Awake()
    {
        _activeAmount = _barImages.Length;
    }

    public void DecreaseAmount()
    {
        _barImages[--_activeAmount].gameObject.SetActive(false);

        Color mainColor = CheckColors();
        foreach (Image item in _barImages)
            item.color = mainColor;
    }

    public void IncreaseAmount()
    {
        _barImages[_activeAmount++].gameObject.SetActive(true);

        Color mainColor = CheckColors();
        foreach (Image item in _barImages)
            item.color = mainColor;
    }

    private Color CheckColors()
    {
        if (_activeAmount <= _barImages.Length && _activeAmount > _barImages.Length * .6f)
        {
            return _colors[0];
        } else if (_activeAmount <= _barImages.Length * .6f && _activeAmount > _barImages.Length * .3f)
        {
            return _colors[1];
        } else
        {
            return _colors[2];
        }
    }
}
