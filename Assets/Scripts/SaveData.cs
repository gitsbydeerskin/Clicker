using UnityEngine;
[System.Serializable]

public class SaveData 
{
    //clicker manager
    public float score;
    public int clickValue;
    public float tClicks;
    public float tPoints;
    public int valueOverTime;
    public float timer;

    //the rest click upgrade
    public string[] upgradeNames;
    public string[] upgradeDescriptions;
    public int[] upgradeValues;
    public int[] upgradePrices;
    public bool[] isAutoUpgrades;
}
