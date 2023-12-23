using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BloodLevelAction : FrenzyAction
{
    [SerializeField] private Material _bloodMaterial;
    [SerializeField] private string _propertyName;

    [SerializeField] private float _effectDuration;

    private void Awake()
    {
        _bloodMaterial.SetFloat(_propertyName, 0);
    }

    public override void StartAction()
    {
        _bloodMaterial.DOFloat(2, _propertyName, _effectDuration);
    }

    public override void UndoAction()
    {
        _bloodMaterial.SetFloat(_propertyName, 0);
    }
}
