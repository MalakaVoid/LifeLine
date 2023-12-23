using UnityEngine;

public class RotateObject : FrenzyAction
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private AudioSource _source;

    private bool _stopped = false;

    private void Update()
    {
        print(_stopped);
        if (_stopped == false)
            transform.Rotate(0, 0, _rotateSpeed);
    }

    public override void StartAction()
    {
        _stopped = true;

        _source.Stop();
        _source.loop = false;
    }

    public override void UndoAction()
    {
        _stopped = false;

        _source.loop = true;
        _source.Play();
    }
}
