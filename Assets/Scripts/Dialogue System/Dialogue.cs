using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource _operatorVoiceSource;
    [SerializeField] private AudioSource _userVoiceSource;
    [Space(3)]

    [SerializeField] private AudioClip _startOperatorClip;
    [Space(5)]

    [Header("Visual Part Setup")]
    [SerializeField] private Transform _blocksParent;
    [Space(3)]

    [SerializeField] private GameObject _userBlock;
    [SerializeField] private GameObject _responseBlock;
    [SerializeField] private GameObject _endingBlock;
    [Space(3)]

    [Header("Responses")]
    [SerializeField] private GameObject _responsesParent;
    [SerializeField] private TMP_Text[] _responsesObjects;

    private DialogueNode _currentNode;
    private EndingSequence _endingSequence;
    private Timer _timer;

    public void SetNewDialogueNode(DialogueNode node)
    {
        _currentNode = node;
        CreateFirstNodes();
    }

    private void Awake()
    {
        _timer = FindObjectOfType<Timer>();
    }

    private void CreateFirstNodes()
    {
        ChooseResponseOption("Вы позвонили в 911, я вас слушаю");
        _operatorVoiceSource.clip = _startOperatorClip;
        _operatorVoiceSource.Play();
    }

    public void StartNode()
    {
        _operatorVoiceSource.Stop();
        _userVoiceSource.Stop();
        _responsesParent.SetActive(false);

        SetupMessage block;
        if (_currentNode == null) // the end
        {
            if (_endingSequence != null)
            {
                ProcessEnding(_endingSequence);
            } else
            {
                SetupMessage bl = Instantiate(_endingBlock, _blocksParent.position, _blocksParent.rotation, _blocksParent).GetComponent<SetupMessage>();
                bl.SetMessage("Конец");

                CallsController.Instance.SetNewDialogue();
            }

            return;
        }

        if (_currentNode.MessageAudioClip != null)
        {
            _userVoiceSource.clip = _currentNode.MessageAudioClip;

            _userVoiceSource.Play();
        }

        block = Instantiate(_userBlock, _blocksParent.position, _blocksParent.rotation, _blocksParent).GetComponent<SetupMessage>();
        block.SetMessage(_currentNode.MessageContent);

        StartCoroutine(WaitForTypeCoroutine(block));
    }

    private void ProcessEnding(EndingSequence sequence)
    {
        StartCoroutine(ShowEnding(sequence));
    }

    private IEnumerator ShowEnding(EndingSequence sequence)
    {
        foreach (EndingSequenceBlock block in sequence.EndingBlocks)
        {
            SetupMessage bl;
            if (block.SentFromUser == false)
            {
                bl = Instantiate(_endingBlock, _blocksParent.position, _blocksParent.rotation, _blocksParent).GetComponent<SetupMessage>();
                bl.SetMessage(block.BlockMessageText);
            } else
            {
                bl = Instantiate(_userBlock, _blocksParent.position, _blocksParent.rotation, _blocksParent).GetComponent<SetupMessage>();
                bl.SetMessage(block.BlockMessageText);
            }
            if (block != null && block.BlockClip != null)
            {
                _userVoiceSource.clip = block.BlockClip;
                _userVoiceSource.Play();
            }

            yield return new WaitForSecondsRealtime(bl.GetTypingDuration() + 1);
        }
        CallsController.Instance.SetNewDialogue();
    }

    private IEnumerator WaitForTypeCoroutine(SetupMessage block)
    {
        float waitTime = block.GetTypingDuration();
        int responsesAmount = _currentNode.UserResponses.Length;

        if (responsesAmount == 1)
        {
            yield return new WaitForSecondsRealtime(waitTime + 1);
            GiveResponse(_currentNode.UserResponses[0]);
            yield break;
        }

        yield return new WaitForSecondsRealtime(waitTime + 1);

        _responsesParent.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            _responsesObjects[i].SetText(_currentNode.UserResponses[i].ResponseContent);
        }

        _userVoiceSource.Stop();
        _timer.StartCycle(8, GetRandomUserResponse());
    }

    private UserResponse GetRandomUserResponse()
    {
        List<UserResponse> tempResponses = new();

        foreach (UserResponse response in _currentNode.UserResponses)
        {
            if (response.IsPositive == false)
                tempResponses.Add(response);
        }

        if (tempResponses.Count == 0)
        {
            if (_currentNode.UserResponses.Length == 0) 
                return null;
            else 
                return _currentNode.UserResponses[Random.Range(0, _currentNode.UserResponses.Length)];
        }

        return tempResponses[Random.Range(0, tempResponses.Count)];
    }

    public void ChooseResponseOption(string content)
    {
        SetupMessage block = Instantiate(_responseBlock, _blocksParent.position, _blocksParent.rotation, _blocksParent).GetComponent<SetupMessage>();
        block.SetMessage(content);

        StartCoroutine(ResponseCoroutine(block));
    }

    public void ChooseResponseOption(TMP_Text buttonText)
    {
        string content = buttonText.text;

        foreach (UserResponse response in _currentNode.UserResponses) 
        {
            if (response.ResponseContent == content)
            {
                GiveResponse(response);

                if (response.IsPositive == false)
                {
                    FrenzyController.Instance.AddFrenzy();
                }

                break;
            }
        }
    }

    public void GiveResponse(UserResponse response)
    {
        if (response.ResponseAudioClip != null)
        {
            _operatorVoiceSource.clip = response.ResponseAudioClip;
            _operatorVoiceSource.Play();
        }

        _timer.Stop();
        _responsesParent.SetActive(false);

        _currentNode = response.ResponseNode;
        _endingSequence = response.EndingSequenceProp;

        SetupMessage block = Instantiate(_responseBlock, _blocksParent.position, _blocksParent.rotation, _blocksParent).GetComponent<SetupMessage>();
        block.SetMessage(response.ResponseContent);

        StartCoroutine(ResponseCoroutine(block));
    }

    private IEnumerator ResponseCoroutine(SetupMessage block)
    {
        float waitTime = block.GetTypingDuration();

        yield return new WaitForSecondsRealtime(waitTime + 1f);

        StartNode();
    }
}