using UnityEngine;
using DG.Tweening;

public class OpenDraw : MonoBehaviour
{
    [SerializeField] private float _openXValue;
    [SerializeField] private float _closedXValue;
    [Space(5)]

    [SerializeField] private float _moveSpeed;

    private bool _open = false;

    private void OnMouseDown()
    {
        transform.DOMoveX(_open == false ? _openXValue : _closedXValue, _moveSpeed);

        _open = !_open;
    }
}
