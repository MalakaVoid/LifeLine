using System.Collections.Generic;
using UnityEngine;

public class DisableLights : FrenzyAction
{
    [SerializeField] private Light[] _lights;

    private List<bool> _states;

    public override void StartAction()
    {
        foreach (var light in _lights)
        {
            _states.Add(light.gameObject.activeSelf);
            light.gameObject.SetActive(false);
        }
    }

    public override void UndoAction()
    {
        for (int i = 0; i < _lights.Length; i++) 
        {
            _lights[i].gameObject.SetActive(_states[i]);
        }
    }
}
