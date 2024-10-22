using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    [SerializeField] private Color takeDamageColor = Color.red;
    [SerializeField] private float flashDuration = .2f;
    [Space]
    [SerializeField] private LoopType loopType = LoopType.Yoyo;
    [SerializeField] private int loops = 2;

    private Tween _currentTween;
    [Space]
    public string colorParameter = "_EmissionColor";

    private void OnValidate()
    {
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if (skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (meshRenderer != null && !_currentTween.IsActive())
            _currentTween = meshRenderer.material.DOColor(takeDamageColor, colorParameter, flashDuration).SetLoops(loops, loopType);

        if (skinnedMeshRenderer != null && !_currentTween.IsActive())
            _currentTween = skinnedMeshRenderer.material.DOColor(takeDamageColor, colorParameter, flashDuration).SetLoops(loops, loopType);
    }
}
