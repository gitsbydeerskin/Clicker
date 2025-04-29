using UnityEngine;
using System;
using UnityEngine.UI;

public class ClickerUpgrade : MonoBehaviour
{
    [Serializable]
    public struct ClickerUpgradeOptions
    {
        public string name;
        public string description;
        public int value;
        public int price;
        public bool isAuto;
        public Text UI;
        public Button button;
    }
    public ClickerUpgradeOptions[] options;
    //the following connects ClickerUpgrade to ClickerManager
    public ClickerManager manager;

    public void Upgrade(int index)
    {
        // manager.score is referencing the public float score = 0; from ClickerManager. 
        if (manager.score >= options[index].price)
            //We are checking if the ClickerManager score is greater or equal to the Upgrade's price by using >=
            //The upgrades are stored in the options[index].price array, from the ClickerUpgradeOptions struct.
        {
            manager.SetPrice(options[index].isAuto, options[index].value, options[index].price);
            //++ increments the "operand" by 1. 
            options[index].value++;
            options[index].price += 2;
        }
    }
}


