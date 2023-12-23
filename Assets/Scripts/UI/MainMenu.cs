using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;

    private Dialogue _dialogueController;

    private void Awake() => _dialogueController = FindObjectOfType<Dialogue>();

    private void OnEnable()
    {
        if (_dialogueController != null) _dialogueController.enabled = false;
    }

    public void StartGame()
    {
        _dialogueController.enabled = true;
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        print("A");
        Application.Quit();
    }
}
