using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DestructableItemBase : MonoBehaviour
{
    [Header("Shake Settings")]
    public HealthBase health;

    public float shakeDuration = .1f;
    public int shakeForce = 5;

    public Vector3 shakeStrenght = Vector3.up;

    [Space]
    [Header("Drop Coins Settings")]
    public GameObject coinPhysicsPrefab;
    public Transform dropPosition;

    public int dropCoinsAmount;
    public float dropAnimDuration;
    public float dropSpawnTime;

    public Ease dropAnimEase = Ease.OutBack;

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
        transform.DOShakeScale(shakeDuration, shakeStrenght, shakeForce);
        DropCoinsAmount();
    }

    private void DropCoins()
    {
        var i = Instantiate(coinPhysicsPrefab);
        i.transform.position = dropPosition.position;
        i.transform.DOScale(0, dropAnimDuration).SetEase(dropAnimEase).From();
    }

    [NaughtyAttributes.Button]
    private void DropCoinsAmount()
    {

        StartCoroutine(DropCoinsCoroutine());
    }

    IEnumerator DropCoinsCoroutine()
    {
        for (int i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(dropSpawnTime);
        }
    }
}