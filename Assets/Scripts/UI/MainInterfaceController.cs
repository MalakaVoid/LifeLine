using UnityEngine;

public class MainInterfaceController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _chatWindow;
    [SerializeField] private GameObject _optMenu;

    public void ToggleOptionsMenu() => _optMenu.SetActive(!_optMenu.activeSelf);

    public void ToggleChat() => _chatWindow.alpha = _chatWindow.alpha == 1 ? 0 : 1;
}
