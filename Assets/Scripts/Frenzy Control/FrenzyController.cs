using UnityEngine;

[System.Serializable]
public class ActionPack
{
    [field: SerializeField] private FrenzyAction[] FrenzyActions { get; set; }

    public void ExecuteActions()
    {
        foreach (FrenzyAction action in FrenzyActions)
            action.StartAction();
    }

    public void RemoveActions()
    {
        foreach (FrenzyAction action in FrenzyActions)
            action.UndoAction();
    }
}

public class FrenzyController : MonoBehaviour
{
    public static FrenzyController Instance;

    [SerializeField] private float _maxFrenzy = 100;
    [Space(5)]

    [Header("Changers Setup")]
    [SerializeField, Tooltip("When wrong response")] private float _frenzyIncrement = 10;
    [SerializeField, Tooltip("When used medicine")] private float _frenzyDecrement = 10;
    [Space(5)]

    [SerializeField] private ActionPack[] _actionPack;

    private FrenzyDisplayController _displayController;

    private int _actionCounter = 0;
    private float _currentFrenzy = 0;

    private void Awake()
    {
        _displayController = FindObjectOfType<FrenzyDisplayController>();
        Instance = this;
    }

    public void HealFrenzy()
    {
        if (_currentFrenzy <= 0) return;

        _currentFrenzy -= _frenzyDecrement;

        _actionPack[--_actionCounter].RemoveActions();
        _displayController.IncreaseAmount();

        if (_currentFrenzy < 0) _currentFrenzy = 0;
    }

    public void AddFrenzy()
    {
        if (_currentFrenzy >= _maxFrenzy) return;

        _currentFrenzy += _frenzyIncrement;

        _actionPack[_actionCounter++].ExecuteActions();
        _displayController.DecreaseAmount();

        if (_currentFrenzy > _maxFrenzy) _currentFrenzy = _maxFrenzy;
    }
}
