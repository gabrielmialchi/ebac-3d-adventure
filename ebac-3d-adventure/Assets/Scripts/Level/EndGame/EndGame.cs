using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    public List<GameObject> endGameObjects;

    public int currentLevel;

    private bool _endGame = false;

    [Header("Events")]
    public UnityEvent OnEndLevelEvent;


    private void Awake()
    {
        endGameObjects.ForEach(i => i.SetActive(false));
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerBase player = other.transform.GetComponent<PlayerBase>();

        if (!_endGame && player != null)
        {
            ShowEndGame();
            OnEndLevelEvent?.Invoke();
        }
    }

    private void ShowEndGame()
    {
        _endGame = true;
        endGameObjects.ForEach(i => i.SetActive(true));

        foreach(var i in endGameObjects)
        {
            i.SetActive(true);
            i.transform.DOScale(0, .2f).SetEase(Ease.OutBack).From();
            SaveManager.Instance.SaveLastLevel(currentLevel);
        }
    }
}
