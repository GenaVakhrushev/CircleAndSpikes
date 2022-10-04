using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoinsCountManager : Singleton<CoinsCountManager>
{
    private static int coinsCount = 0;
    private static int coinsTotalCount;

    [SerializeField] private Text coinsCountText;

    private void Start()
    {
        coinsCount = 0;

        coinsTotalCount = FindObjectsOfType<Coin>().Length;
    }

    public static void OnCoinCollected()
    {
        coinsCount++;

        Instance.coinsCountText.text = coinsCount.ToString();

        if(coinsTotalCount == coinsCount)
        {
            GameStateManager.Win();
        }
    }
}
