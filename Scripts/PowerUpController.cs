using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUpData powerUpData; // Doðru veri tipi kullanýldý.
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
    
    private void OnTriggerExit(Collider other) // OnTriggerExit yerine OnTriggerEnter kullanýlmasý daha mantýklý.
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
