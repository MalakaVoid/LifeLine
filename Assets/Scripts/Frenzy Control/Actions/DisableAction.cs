using UnityEngine;

public class DisableAction : FrenzyAction
{
    [SerializeField] private MeshRenderer _renderer;
    [Space(5)]

    [SerializeField] private Material _enabled;
    [SerializeField] private Material _disabled;
    [SerializeField] private int _matIndex = 1;
    [Space(3)]

    [SerializeField] private GameObject _lightSource;

    private Material _startMaterial;
    private bool _startLightSourceState;

    public override void StartAction()
    {
        TurnOff();
    }

    public override void UndoAction()
    {
        Restore();
    }

    private void TurnOff()
    {
        _startLightSourceState = _lightSource.activeSelf;
        _startMaterial = _renderer.materials[_matIndex];

        Material[] mats = _renderer.materials;
        mats[_matIndex] = _disabled;

        _renderer.materials = mats;

        _lightSource.SetActive(false);
    }

    private void Restore()
    {
        _lightSource.SetActive(_startLightSourceState);

        Material[] mats = _renderer.materials;
        mats[_matIndex] = _startMaterial;

        _renderer.materials = mats;
    }
}
