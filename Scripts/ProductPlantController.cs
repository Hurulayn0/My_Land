using System.Collections;
using UnityEngine;

public class ProductPlantController : MonoBehaviour
{
    private bool isReadyToPick;
    private Vector3 originalScale;
    [SerializeField] private ProductData productData;
    private BagController bagController;

    void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Empty update loop
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isReadyToPick)
        {
            bagController = other.GetComponent<BagController>();

            // Check if there is space in the bag before adding the product
            if (bagController != null && bagController.IsEmptySpace())
            {
                // Add product to the bag
                AudioManager.instance.PlayAudio(AudioClipType.grabClip);
                bagController.AddProductToBag(productData);
                Debug.Log("Product picked: " + productData.productType);
                isReadyToPick = false;
                StartCoroutine(ProductPicked());
            }
            else
            {
                Debug.Log("No space in the bag for this product.");
            }
        }
    }

    IEnumerator ProductPicked()
    {
        // Shrink the plant for effect
        Vector3 targetScale = originalScale / 3;
        transform.gameObject.LeanScale(targetScale, 1f); // Shrinking effect
        yield return new WaitForSeconds(10f); // Wait for 5 seconds
        transform.gameObject.LeanScale(originalScale, 1f).setEase(LeanTweenType.easeOutBack); // Growing effect

        // After the effect is done, allow picking again
        isReadyToPick = true;
    }








    //bu kod çok uzun kýsa yol için eklediðimiz asseti kullanacaðýz
    /* IEnumerator ProductPicked()//ýe belli bir zaman bekletmek istenilen iþlemler için kullanýlýr 
     {
         float duration = 1f;
         float timer = 0f;
         Vector3 targetScale = originalScale / 3;
         while (timer<duration)
         {
             float t = timer / duration;
             Vector3 newScale=Vector3.Lerp(originalScale, targetScale, t);//gittikçe küçülcek lerp de daha güzel bir geçiþ saðlamak için kullanýlýyor 
             transform.localScale = newScale;
             timer += Time.deltaTime;
             yield return null;
         }
         //artýk fideler küçük
         yield return new WaitForSeconds(5f);//5 saniye bekle 
         timer = 0f;
         float growBackDuration = 1f;
         while (timer < growBackDuration)
         {
             float t = timer / growBackDuration;
             Vector3 newScale = Vector3.Lerp(targetScale, originalScale, t);//gittikçe büyücek
             transform.localScale = newScale;
             timer += Time.deltaTime;
             yield return null;
         }
         isReadyToPick = true;//tekrardan toplanabilir 
         yield return null;


     }*/
}