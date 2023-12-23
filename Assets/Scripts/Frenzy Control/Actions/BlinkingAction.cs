using System.Collections;
using UnityEngine;

public class BlinkingAction : FrenzyAction
{
    [SerializeField] private float _minDelay = 2;
    [SerializeField] private float _maxDelay = 4;
    [Space(5)]

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
        StartCoroutine(Blink());
    }

    public override void UndoAction()
    {
        StopCoroutine(Blink());
        Restore();
    }

    private IEnumerator Blink()
    {
        _startLightSourceState = _lightSource.activeSelf;
        _startMaterial = _renderer.materials[_matIndex];

        while (true)
        {
            TurnOn();
            yield return new WaitForSecondsRealtime(Random.Range(_minDelay, _maxDelay));

            TurnOff();
            yield return new WaitForSecondsRealtime(Random.Range(_minDelay, _maxDelay));
        }
    }

    private void Restore()
    {
        _lightSource.SetActive(_startLightSourceState);

        Material[] mats = _renderer.materials;
        mats[_matIndex] = _startMaterial;

        _renderer.materials = mats;
    }

    private void TurnOff()
    {
        Material[] mats = _renderer.materials;
        mats[_matIndex] = _disabled;

        _renderer.materials = mats;

        _lightSource.SetActive(false);
    }

    private void TurnOn()
    {
        Material[] mats = _renderer.materials;
        mats[_matIndex] = _enabled;

        _renderer.materials = mats;

        _lightSource.SetActive(true);
    }
}
