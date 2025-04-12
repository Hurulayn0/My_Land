using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BagController : MonoBehaviour
{
    [SerializeField] private Transform bag;
    public List<ProductData> productDataList;
    private Vector3 productSize;
    [SerializeField] TextMeshPro maxText;
    private int maxBagCapacity = 5; // Maximum capacity of the bag
    private bool isRemovingProducts = false; // Flag to control removal process
    private bool isInShopPoint = false; // Flag to check if we are in ShopPoint
    [SerializeField] private Transform shopPointTarget; // ShopPoint target object
    public TextMeshProUGUI productText;
    private string bagCapacityKey = "bagCapacityKey";

    void Start()
    {
        // Initialize max bag capacity
        maxBagCapacity = LoadBagCapacity();
        productText.text = "PUT HERE ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShopPoint"))
        {
            
            isInShopPoint = true;

            if (!isRemovingProducts)
            {
                PlayShopSound();
                StartCoroutine(RemoveProductsOneByOne());
            }
        }
        if (other.CompareTag("UnlockBakeryUnit"))
        {
            UnlockBakeryUnitController bakeryUnit = other.GetComponent<UnlockBakeryUnitController>();
            if (bakeryUnit == null) return; // Eðer bileþen eksikse çýk

            ProductType neededType = bakeryUnit.GetNeededProductType();
            
            for (int i = productDataList.Count - 1; i >= 0; i--)
            {
                if (productDataList[i].productType == neededType) // Ürün istenen mi?
                {
                    if (bakeryUnit.StoreProduct())
                    {
                        PlayShopSound();
                        Destroy(bag.transform.GetChild(i).gameObject);
                        productDataList.RemoveAt(i);
                    }
                }
            }
            StartCoroutine(PutProductsInOrder());
            ControlBagCapacity();
        }
    }

    


    private void SellProductToShop(ProductData productData)
    {
        // Simulate selling the product to the shop
        CahManager.instance.ExchangeProduct(productData);
    }

    public void AddProductToBag(ProductData productData)
    {
        GameObject boxProduct = Instantiate(productData.productPrefab, Vector3.zero, Quaternion.identity);
        boxProduct.transform.SetParent(bag, true);


        CalculateObjectSize(boxProduct);
        float yPosition = CalculateNewYPositionOfBox();
        boxProduct.transform.localRotation = Quaternion.identity;
        boxProduct.transform.localPosition = Vector3.zero;
        boxProduct.transform.localPosition = new Vector3(0, yPosition, 0);
        productDataList.Add(productData);
        ControlBagCapacity();
        
    }

    private float CalculateNewYPositionOfBox()
    {
        // ürünün sahnedeki yüksekliði * ürünün adedi;
        float newYPos = productSize.y * productDataList.Count;
        return newYPos;
    }

    private void CalculateObjectSize(GameObject gameObject)
    {
        if (productSize == Vector3.zero)
        {
            MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
            productSize = renderer.bounds.size;

        }
    }

    private void ControlBagCapacity()
    {
        if (productDataList.Count >= maxBagCapacity)
        {
            SetMaxTextOn(); // Show max text if bag is full
        }
        else
        {
            SetMaxTextOff(); // Hide max text if there is space
        }
    }

    private void SetMaxTextOn()
    {
        if (!maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(true); // Show max text
        }
    }

    private void SetMaxTextOff()
    {
        if (maxText.isActiveAndEnabled)
        {
            maxText.gameObject.SetActive(false); // Hide max text
        }
       
    }
  

    public bool IsEmptySpace()
    {
        return productDataList.Count < maxBagCapacity; // Check if there is space
    }

    private IEnumerator RemoveProductsOneByOne()
    {
        isRemovingProducts = true; // Ürün çýkarma iþlemi baþladý  
        
        for (int i = productDataList.Count - 1; i >= 0; i--)
        {
            if (!isInShopPoint) // Eðer ShopPoint'ten çýkýldýysa iþlemi durdur  
            {
                break;
            }

            SellProductToShop(productDataList[i]);

            // Ürünü ShopPoint'e taþý ve pozisyon ayarla  
            Transform productTransform = bag.transform.GetChild(i);
            productTransform.SetParent(shopPointTarget, true);

            // Y pozisyonunu hesapla  
            float newYPosition = (productSize.y ) * shopPointTarget.childCount;
            productTransform.localPosition = new Vector3(0, newYPosition, 0); // Ürünü Y eksenine göre ayarla  

            // Boyutunu koru  
            productTransform.localScale = Vector3.one;
            productTransform.localRotation = Quaternion.identity; // Yönü koru  

            productDataList.RemoveAt(i); // Listeyi güncelle  
            yield return new WaitForSeconds(0.5f); // Bekleme süresi  

            // Control bag capacity after each removal to hide "max" text if needed  
            ControlBagCapacity();
            
        }
        isRemovingProducts = false; // Ýþlem tamamlandý  

        if (productDataList.Count == 0)
        {
            productText.text = string.Empty; // Daha güvenli ve önerilen yöntem
        }

    }

    private IEnumerator PutProductsInOrder()
    {
        yield return new WaitForSeconds(0.15f);
        for(int i = 0; i < bag.childCount; i++)
        {
            float newYPos = productSize.y * i;
            bag.GetChild(i).transform.localPosition = new Vector3(0, newYPos, 0);
        }
    }
    private void PlayShopSound()
    {
        if(productDataList.Count > 0)
        {
            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
            AudioManager.instance.StopBackGroundMusic();
        }
    }
    public void BoostBagCapacity(int boostCount)
    {
        maxBagCapacity+= boostCount;
        PlayerPrefs.SetInt(bagCapacityKey, maxBagCapacity);
        ControlBagCapacity();
    }
    private int LoadBagCapacity()
    {
        int maxBag=PlayerPrefs.GetInt(bagCapacityKey,5);
        return maxBag;
    } 




}