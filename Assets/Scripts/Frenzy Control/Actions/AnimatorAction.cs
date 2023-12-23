using UnityEngine;

public class AnimatorAction : FrenzyAction
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _triggerString = "Move";
    [Space(5)]

    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _legs;
    [Space(5)]

    [SerializeField] private AudioSource _mainChairSound;
    [SerializeField] private AudioClip _rideSoundClip;
    [SerializeField] private AudioClip _rotateSoundClip;

    private Vector3 _startPositionVector;

    private Quaternion _mainBodyStartRotation;
    private Quaternion _legsStartRotation;

    public override void StartAction()
    {
        _animator.SetTrigger(_triggerString);

        _startPositionVector = transform.position;

        _mainBodyStartRotation = _body.transform.rotation;
        _legsStartRotation = _legs.transform.rotation;
    }

    public override void UndoAction()
    {
        _animator.SetTrigger("Reset");
        transform.position = _startPositionVector;

        _body.transform.rotation = _mainBodyStartRotation;
        _legs.transform.rotation = _legsStartRotation;
    }

    public void PlayRideSound()
    {
        _mainChairSound.Stop();
        _mainChairSound.clip = _rideSoundClip;
        _mainChairSound.Play();
    }

    public void PlayRotateSound()
    {
        _mainChairSound.Stop();
        _mainChairSound.clip = _rotateSoundClip;
        _mainChairSound.Play();
    }
}
