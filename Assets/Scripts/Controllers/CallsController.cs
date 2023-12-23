using System.Collections;
using UnityEngine;

public class CallsController : MonoBehaviour
{
    public static CallsController Instance { get; private set; }

    [SerializeField] private GameObject _gameCompleted;

    [SerializeField] private AudioSource _callSource;

    [SerializeField] private DialogueNode[] _dialogueNodes;

    [SerializeField] private Transform _contentParent;
    [SerializeField] private GameObject _callInterface;
    [SerializeField] private float _afterCallsTime;

    private Dialogue _dialogue;

    private int _currentIndex = -1;

    private void Awake()
    {
        Instance = this;
        _dialogue = FindObjectOfType<Dialogue>();
    }

    public void SetNewDialogue()
    {
        StartCoroutine(NewDialogueCoroutine());
    }

    public void AcceptCall()
    {
        _callSource.Stop();
        _callSource.loop = false;

        _dialogue.SetNewDialogueNode(_dialogueNodes[_currentIndex]);
    }

    private void StartCalling()
    {
        ClearContent();
        _callInterface.SetActive(true);

        _callSource.Play();
        _callSource.loop = true;
    }

    private void ClearContent()
    {
        foreach (Transform child in _contentParent)
            Destroy(child.gameObject);
    }

    private IEnumerator NewDialogueCoroutine()
    {
        _currentIndex++;

        if (_currentIndex < 0 || _currentIndex >= _dialogueNodes.Length)
        {
            _gameCompleted.SetActive(true);
            FrenzyController.Instance.gameObject.SetActive(false);
            _dialogue.gameObject.SetActive(false);

            yield break;
        }

        yield return new WaitForSecondsRealtime(_afterCallsTime);

        StartCalling();        
    }
}
