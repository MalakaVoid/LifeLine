using DG.Tweening;
using UnityEngine;

[System.Serializable]
public struct ParticlesParams
{
    [field: SerializeField] public float MinSize { get; private set; }
    [field: SerializeField] public float MaxSize { get; private set; }
    [field: Space(5)]

    [field: SerializeField] public float MinLifetime { get; private set; }
    [field: SerializeField] public float MaxLifetime { get; private set; }
    [field: Space(5)]

    [field: SerializeField] public float MinSpeed { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: Space(5)]

    [field: SerializeField] public Color MainColor { get; private set; }
}

public class SnowBloodAction : FrenzyAction
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem _frontParticles;
    [SerializeField] private ParticleSystem _rightParticles;
    [Space(5)]

    [SerializeField] private Material _trailMaterial;

    [Header("Params")]
    [SerializeField] private ParticlesParams _frontSnowParticlesParams;
    [SerializeField] private ParticlesParams _rightSnowParticlesParams;
    [Space(3)]

    [SerializeField] private ParticlesParams _frontBloodParticlesParams;
    [SerializeField] private ParticlesParams _rightBloodParticlesParams;

    public override void StartAction()
    {
        ToBlood();
    }

    public override void UndoAction()
    {
        ToSnow();
    }

    private void ToSnow()
    {
        ChangeParticleSystem(_frontParticles, _frontSnowParticlesParams, true);
        ChangeParticleSystem(_rightParticles, _rightSnowParticlesParams, true);
    }

    private void ToBlood()
    {
        ChangeParticleSystem(_frontParticles, _frontBloodParticlesParams, false);
        ChangeParticleSystem(_rightParticles, _rightBloodParticlesParams, false);
    }

    private void ChangeParticleSystem(ParticleSystem partSystem, ParticlesParams partParams, bool isNoised)
    {
        var mainModule = partSystem.main;

        mainModule.startSize = new ParticleSystem.MinMaxCurve(partParams.MinSize, partParams.MaxSize);
        mainModule.startSpeed = new ParticleSystem.MinMaxCurve(partParams.MinSpeed, partParams.MaxSpeed);
        mainModule.startLifetime = new ParticleSystem.MinMaxCurve(partParams.MinLifetime, partParams.MaxLifetime);

        mainModule.startColor = partParams.MainColor;

        var noiseModule = partSystem.noise;
        noiseModule.enabled = isNoised;

        var trailModule = partSystem.trails;
        trailModule.enabled = !isNoised;

        var renderModule = partSystem.GetComponent<ParticleSystemRenderer>();
        renderModule.trailMaterial = _trailMaterial;

        partSystem.DORestart();
    }
}
