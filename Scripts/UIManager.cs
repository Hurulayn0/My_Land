using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI coinCountText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {   //en kolay singleton yapma metodu  yoksa yarat varsa sil bu þekilde tek bir obje kalýyor 
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowCoinCountOnScreen(int coins)
    {
        coinCountText.text = coins.ToString();
    }

}