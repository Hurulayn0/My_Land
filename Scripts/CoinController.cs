using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int coinPrice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
            CahManager.instance.AddCoin(coinPrice);
            Destroy(gameObject);

        }
    }
}
