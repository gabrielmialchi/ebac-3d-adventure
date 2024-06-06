using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    [SerializeField] private Color takeDamageColor = Color.red;
    [SerializeField] private float flashDuration = .2f;
    [SerializeField] private LoopType loopType = LoopType.Yoyo;
    [SerializeField] private int loops = 2;

    private Color _defaultColor;
    private Tween _currentTween;

    private void Start()
    {
        _defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (!_currentTween.IsActive())
            _currentTween = meshRenderer.material.DOColor(takeDamageColor, "_EmissionColor", flashDuration).SetLoops(loops, loopType);
    }

}
