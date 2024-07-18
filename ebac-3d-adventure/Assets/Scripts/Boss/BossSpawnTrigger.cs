using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;

public class BossSpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _bossParent;
    [SerializeField] private string _tagToCompare;

    private void Awake()
    {
        _bossParent.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCompare))
        {
            _bossParent.gameObject.SetActive(true);
            var bossBase = _bossParent.GetComponent<BossBase>();
            bossBase.SwitchState(BossAction.INIT);
        }
    }
}
