using UnityEngine;

public class DeathAction : FrenzyAction
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private Dialogue _dialogueController;
    [SerializeField] private FrenzyController _frenzyController;

    public override void StartAction()
    {
        _gameOverScreen.SetActive(true);

        _dialogueController.gameObject.SetActive(false);
        _frenzyController.gameObject.SetActive(false);
    }

    public override void UndoAction()
    {
        return;
    }
}
