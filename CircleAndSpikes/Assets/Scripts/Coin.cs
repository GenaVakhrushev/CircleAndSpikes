using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        CoinsCountManager.OnCoinCollected();

        Destroy(gameObject);
    }
}
