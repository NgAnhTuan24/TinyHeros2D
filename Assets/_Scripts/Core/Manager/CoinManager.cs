using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int CurrentCoin { get; private set; }

    public event Action<int> OnCoinChanged;

    public void AddCoin(int amount)
    {
        CurrentCoin += amount;

        OnCoinChanged?.Invoke(CurrentCoin);
    }

    public void SetCoin(int amount)
    {
        CurrentCoin = amount;

        OnCoinChanged?.Invoke(CurrentCoin);
    }

    public bool SpendCoin(int amount)
    {
        if (CurrentCoin < amount)
            return false;

        CurrentCoin -= amount;
        OnCoinChanged?.Invoke(CurrentCoin);
        return true;
    }
}
