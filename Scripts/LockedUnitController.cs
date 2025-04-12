using UnityEngine;
using TMPro;

public class LockedUnitController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int price;
    [Header("Objects")]
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private GameObject lockedUnit;
    [SerializeField] private GameObject unlockedUnit;
    private bool isPurchased;  //default de�eri false ba�lat�r 
    [SerializeField] private int ID;
    private string keyUnit = "keyUnit";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        priceText.text = price.ToString();
        LoadUnit();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPurchased)
        {
            unlockUnit();
            //�r�n� paras� yeterse a�
        }
    }
    private void unlockUnit()
    {
        if (CahManager.instance.TryBuyThisUnit(price))
        {
            AudioManager.instance.PlayAudio(AudioClipType.shopClip);
            unlock();
            SaveUnit();
        }
        //paras� var m� kontrol et 
        //varsa �r�n� a�


    }
    private void unlock()
    {
        isPurchased = true;
        lockedUnit.SetActive(false);  //kiltili objeyi ekrandn kald�rcaqk 
        unlockedUnit.SetActive(true); //tarlay� a���a ��karcak 
    }

    private void SaveUnit()
    {
        string key=keyUnit+ID.ToString();
        PlayerPrefs.SetString(key, "SAVED");
    }
    private void LoadUnit()
    {
        string key = keyUnit + ID.ToString();
        string status=PlayerPrefs.GetString(key);
        if (status.Equals("SAVED"))
        {
            unlock();
        }
    }
}
