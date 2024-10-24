using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{

    public GameObject uiEndGame;
    public GameObject uiCoins;
    public GameObject player;

    private void Awake()
    {
        uiEndGame.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        uiEndGame.SetActive(true);
        uiCoins.SetActive(false);
        player.SetActive(false);

    }

}
