using UnityEngine;

public class FrenzyHealer : MonoBehaviour
{
    [SerializeField] private GameObject[] _pills;
    [SerializeField] private int _pillsCount = 3;

    private int _currentPillsAmount;

    private void Awake()
    {
        _currentPillsAmount = _pillsCount;

        for (int i = 0; i < _pills.Length; i++)
        {
            if (i < _pillsCount) _pills[i].SetActive(true);
            else _pills[i].SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        _pills[--_currentPillsAmount].SetActive(false);
        if (FrenzyController.Instance.gameObject.activeSelf) FrenzyController.Instance.HealFrenzy();
    }
}
