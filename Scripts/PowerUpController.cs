using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData; // Do�ru veri tipi kullan�ld�.
    [SerializeField] private int LockedUnitID;
    bool isPowerUpUsed;
    string powerupstatusKey = "powerupstatusKey";
    void Start()
    {
        isPowerUpUsed = GetPowerUpStatus();

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerExit(Collider other) // OnTriggerExit yerine OnTriggerEnter kullan�lmas� daha mant�kl�.
    {
        if (other.CompareTag("Player"))
        {
            if (powerUpData.powerUpType == PowerUpType.bagBooster&& !isPowerUpUsed)
            {
                isPowerUpUsed=true;
                BagController bagController = other.GetComponent<BagController>();
                bagController.BoostBagCapacity(powerUpData.boostCount);
                AudioManager.instance.PlayAudio(AudioClipType.grabClip);
                PlayerPrefs.SetString(powerupstatusKey,"used");
            }
        }

    }
    private bool GetPowerUpStatus()
    {
        string status = PlayerPrefs.GetString(powerupstatusKey,"ready");
        if (status.Equals("ready"))
        {
            return false;
        }
        return true;

    }
}
