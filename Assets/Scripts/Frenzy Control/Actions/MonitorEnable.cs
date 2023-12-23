using UnityEngine;

public class MonitorEnable : FrenzyAction
{
    [Header("Controllables")]
    [SerializeField] private Material _enabledMaterial;
    [SerializeField] private Material _disabledMaterial;
    [Space(3)]

    [SerializeField] private GameObject _lightSource;
    [Space(5)]

    [SerializeField] private MeshRenderer _meshToChange;
    [SerializeField] private int _materialIndex = 1;

    private bool _startState;
    private Material _startMaterial;

    public override void StartAction()
    {
        _startMaterial = _meshToChange.materials[_materialIndex];
        _startState = _lightSource.activeSelf;

        Material[] materials = _meshToChange.materials;
        materials[_materialIndex] = _enabledMaterial;
        _meshToChange.materials = materials;

        _lightSource.SetActive(true);
    }

    public override void UndoAction()
    {
        Material[] materials = _meshToChange.materials;
        materials[_materialIndex] = _startMaterial;
        _meshToChange.materials = materials;

        _lightSource.SetActive(_startState);
    }
}
