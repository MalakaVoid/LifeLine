using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Node", menuName = "Dialogue/Node", order = 2)]
public class DialogueNode : ScriptableObject
{
    [field: SerializeField, TextArea] public string MessageContent { get; set; }
    [field: SerializeField] public AudioClip MessageAudioClip { get; set; }
    [field: Space(3)]

    [field: SerializeField] public UserResponse[] UserResponses { get; set; }
}

[System.Serializable]
public class UserResponse
{
    [field: SerializeField, TextArea] public string ResponseContent { get; set; }
    [field: SerializeField] public bool IsPositive { get; set; }
    [field: SerializeField] public AudioClip ResponseAudioClip { get; set; }
    [field: Space(5)]

    [field: SerializeField] public DialogueNode ResponseNode { get; set; }
    [field: SerializeField] public EndingSequence EndingSequenceProp { get; set; }
}