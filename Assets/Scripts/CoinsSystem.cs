using TMPro;
using UnityEngine;

public class CoinsSystem : MonoBehaviour
{
    public int coinsNumb;
    [SerializeField] private TMP_Text coinsTxt;

    private void Update()
    {
        coinsTxt.text = coinsNumb.ToString();
    }

    public void AddCoins(int addedCoins)
    {
        coinsNumb += addedCoins;
    }

    public void RemoveCoins(int removedCoins)
    {
        coinsNumb -= removedCoins;
    }
}
