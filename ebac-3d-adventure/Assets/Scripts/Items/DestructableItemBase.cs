using UnityEngine;
using DG.Tweening;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase health;

    [Header("Shake Settings")]
    public float duration = .1f;
    public int vibrato = 5;
    public Vector3 strenght = Vector3.up;

    private void OnValidate()
    {
        if (health == null) health = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        health.OnDamageAction += OnDamageAction;
    }

    private void OnDamageAction(HealthBase h)
    {
        transform.DOShakeScale(duration, strenght, vibrato);
    }
}