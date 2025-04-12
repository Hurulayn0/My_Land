using UnityEngine;

public enum PowerUpType { bagBooster }

[CreateAssetMenu(fileName = "PowerUpData", menuName = "Scriptable Object/Power Up Data", order = 1)]
public class PowerUpData : ScriptableObject
{
    public PowerUpType powerUpType;
    public int boostCount;
}

