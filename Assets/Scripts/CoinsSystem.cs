using TMPro;
using UnityEngine;

public class CoinsSystem : MonoBehaviour
{
    public int coinsNumb;
    [SerializeField] private TMP_Text coinsTxt;


    private void Update()
    {
        coinsTxt.text = coinsNumb.ToString();

        PlayerPrefs.SetInt("CoinAmount", coinsNumb);

        if (PlayerPrefs.HasKey("CoinAmount"))
        {
            coinsNumb = PlayerPrefs.GetInt("CoinAmount");
        }

    }

    public void AddCoins(int addedCoins)
    {
        coinsNumb += addedCoins;
        Debug.Log("Add Coins");
    }

    public void RemoveCoins(int removedCoins)
    {
        coinsNumb -= removedCoins;
    }
}
