using UnityEngine;

[RequireComponent(typeof(Typewriter))]
public class SetupMessage : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _content;

    private Typewriter _typewriter;

    private void Awake() => _typewriter = GetComponent<Typewriter>();

    public void SetMessage(string text)
    {
        _content.SetText(text);
        _typewriter.StartTypewriter(text);
    }

    public float GetTypingDuration() => _typewriter.TimeBtwChars * _typewriter.Writer.Length;
}
