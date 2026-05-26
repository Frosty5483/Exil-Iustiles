using TMPro;
using UnityEngine;

public class openTruhe : MonoBehaviour
{
    [SerializeField] private TMP_Text popTxt;

    [SerializeField] private Animator playerAnim;

    public Animator chestAnim;

    [SerializeField] CoinsSystem coinSys;

    private bool hiddenSafe;

    public int coinAmountMin;
    public int coinAmountMax;

    private bool cantOpen;

    public string playerPrefString;

    private void Start()
    {
        coinSys = Utils.FindWithTagAcrossAllScenes("CoinSys").GetComponent<CoinsSystem>();

        popTxt.text = "";
        hiddenSafe = false;
        if (!PlayerPrefs.HasKey(playerPrefString))
        {
            PlayerPrefs.SetInt(playerPrefString, 0);
        }
        else if (PlayerPrefs.GetInt(playerPrefString) == 1)
        {
            cantOpen = true;
        }
    }

    private void Update()
    {
        coinSys = Utils.FindWithTagAcrossAllScenes("CoinSys").GetComponent<CoinsSystem>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && cantOpen == false)
        {
            popTxt.text = "Press [F] to pick up";
            if (!hiddenSafe && Input.GetKey(KeyCode.F))
            {
                cantOpen = true;
                hiddenSafe = true;
                hiddenSafe = false;
                popTxt.text = "";
                chestAnim.SetTrigger("Collect");
                PlayerPrefs.SetInt(playerPrefString, 1);
                if (playerAnim.gameObject.GetComponent<PlayerMoveNew>().inFPS == false)
                {
                    playerAnim.SetTrigger("pickUp");
                    coinSys.AddCoins(Random.Range(coinAmountMin, coinAmountMax));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            popTxt.text = "";
        }
    }
}
