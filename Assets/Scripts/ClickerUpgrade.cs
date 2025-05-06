using UnityEngine;
using System;
using UnityEngine.UI;

public class ClickerUpgrade : MonoBehaviour
   
{
    [System.Serializable]
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
    //public ClickerJSONSave saveManager;
    public GameObject shopPanel;
    private void Start()
    {
        for (int i = 0; i < options.Length; i++)
        {
            UpdateUI(i);
        }
    }
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
            UpdateUI(index);

        }
    }
    void UpdateUI(int index)
    { 
        options[index].UI.text = $"{options[index].name} : {options[index].description}\nPrice: {options[index].price:C} : Value: {options[index].value}";        
    }
    void isInteractable(int index)
    {
        //if score is higher than price - we know the button should be on
        //but we dont want to turn on when we done need to every frame
        //so only turn it on if it is off 
        if (manager.score >= options[index].price && options[index].button.interactable == false)
        {
            options[index].button.interactable = true;
        }

        else if(manager.score < options[index].price && options[index].button.interactable == true)
        {
            options[index].button.interactable = false;
        }

    }
    private void Update()
    {
        for (int i = 0; i < options.Length; i++)
        {
            isInteractable(i);
        }
    }
  
}




