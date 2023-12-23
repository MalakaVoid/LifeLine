using UnityEngine;

[CreateAssetMenu(fileName = "Ending Controller", menuName = "Dialogue/Ending Controller", order = 1)]
public class EndingSequence : ScriptableObject
{
    [field: SerializeField] public EndingSequenceBlock[] EndingBlocks { get; set; }
}

[System.Serializable]
public class EndingSequenceBlock
{
    [field: SerializeField, TextArea] public string BlockMessageText { get; set; }
    [field: SerializeField] public bool SentFromUser { get; set; }
    [field: SerializeField] public AudioClip BlockClip { get; set; }
}
