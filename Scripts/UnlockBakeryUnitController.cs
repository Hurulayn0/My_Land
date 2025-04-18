using UnityEngine;
using TMPro;

public class UnlockBakeryUnitController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bakeryText;
    [SerializeField] private int maxStoredProductCount;
    [SerializeField] private ProductType productType;
    private int storedProductCount;
    [SerializeField] private int UseProductInSeconds=10;
    [SerializeField] private Transform coinTransform;
    [SerializeField] private GameObject coinGO;
    private float time;
    [SerializeField] private ParticleSystem smokeParticle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayProductCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (storedProductCount > 0)
        {
            time += Time.deltaTime;

            if (time >= UseProductInSeconds)
            {
                time = 0.0f;
                UseProduct();
            }
        }
        
    }
    private void DisplayProductCount()
    {
        bakeryText.text = storedProductCount.ToString() + "/" + maxStoredProductCount.ToString();
        ControlSmokeEfffect();
    }
    public ProductType GetNeededProductType()
    {
        return productType;
    }
    public bool StoreProduct()
    {
        if (maxStoredProductCount == storedProductCount)
        {
            return false;
        }
        storedProductCount++;
        DisplayProductCount();
        return true;

    }
    private void UseProduct()
    {
        storedProductCount--;
        DisplayProductCount() ;
        CreateCoin();
    }

    private void CreateCoin()
    {
        Vector3 position=Random.insideUnitSphere*1f;
        Vector3 InstantiatePos = coinTransform.position;
        Instantiate(coinGO, InstantiatePos, Quaternion.identity);

    }
    private void ControlSmokeEfffect()
    {
        if (storedProductCount ==0) 
        {
            if (smokeParticle.isPlaying)
            {
                smokeParticle.Stop();
            }
        }
        else
        {
            if (smokeParticle.isStopped) 
            {
                smokeParticle.Play();
            }
        }


    }
}
