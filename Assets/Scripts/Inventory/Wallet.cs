using UnityEngine.UI;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [SerializeField] int numMoney = 0;
    Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        text.text = "" + numMoney;
    }

    public void AddMoney(int money)
    {
        numMoney += money;
        text.text = ""+numMoney;

    }

    public void SpendMoney(int money)
    {
        numMoney -= money;
        text.text = "" + numMoney;
    }

    public int GetNumMoney() { return numMoney; }
    public bool HasEnoughMoney(int price) { return (numMoney >= price); }

    #region Singleton
    private static Wallet _instance;
    public static Wallet Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
}
