using Cloth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public List<UIFillUpdater> uiHealthUpdater;
    [SerializeField] private ClothChanger _clothChanger;

    public int _currentLife;
    public int startLife = 10;
    public int maxLife = 100;

    public bool destroyOnKill = false;

    [SerializeField] private float _delayToDestroy = 3f;

    public Action<HealthBase> OnDamageAction;
    public Action<HealthBase> OnKillAction;

    public int damageDivider = 2;

    public int CurrentLife => _currentLife;

    [Space]
    [Header("Sounds")]
    public AudioSource damageAS;
    public AudioClip damageSound;
    public AudioClip healSound;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public virtual void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    public virtual void Kill()
    {
        if (destroyOnKill)
            Destroy(gameObject, _delayToDestroy);

        OnKillAction?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(int damage)
    {
        _currentLife -= damage / damageDivider;
        damageAS.PlayOneShot(damageSound);

        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        OnDamageAction?.Invoke(this);
        ScreenShake.Instance.Shake();

    }

    public void Damage(int damage, Vector3 direction)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if (uiHealthUpdater != null)
        {
            uiHealthUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageDivider(int damageDivider, float duration)
    {
        StartCoroutine(ChangeDamageDividerCoroutine(damageDivider, duration));
    }

    IEnumerator ChangeDamageDividerCoroutine(int divider, float duration)
    {
        damageDivider = divider;
        yield return new WaitForSeconds(duration);
        damageDivider = 1;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }

}
