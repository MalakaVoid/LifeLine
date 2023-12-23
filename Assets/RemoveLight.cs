using UnityEngine;

public class RemoveLight : FrenzyAction
{
    [SerializeField] private MeshRenderer _roofRenderer;
    [Space(5)]

    [SerializeField] private Material _offMaterial;
    [SerializeField] private int _matIndex = 1;

    private Material _startMaterial;
    private LightmapData[] _startData;

    public override void StartAction()
    {
        _startMaterial = _roofRenderer.materials[_matIndex];
        _startData = LightmapSettings.lightmaps;

        LightmapSettings.lightmaps = new LightmapData[] { };

        Material[] mats = _roofRenderer.materials;
        mats[_matIndex] = _offMaterial;
        _roofRenderer.materials = mats;
    }

    public override void UndoAction()
    {
        LightmapSettings.lightmaps = _startData;

        Material[] mats = _roofRenderer.materials;
        mats[_matIndex] = _startMaterial;
        _roofRenderer.materials = mats;
    }
}
