using UnityEngine;

public class HeartbeatAction : FrenzyAction
{
    [SerializeField] private AudioSource _mainSource;

    public override void StartAction()
    {
        _mainSource.loop = true;
        _mainSource.Play();
    }

    public override void UndoAction()
    {
        _mainSource.Stop();
        _mainSource.loop = false;
    }
}
