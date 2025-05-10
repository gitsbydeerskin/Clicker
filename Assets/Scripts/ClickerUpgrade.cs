using UnityEngine;
using UnityEngine.UI;

public class ClickerUpgrade : MonoBehaviour
#region Script Purpose
    //This is the companion script to the ClickerManager.cs 
    //It handles the game's Upgrades, which were partly set up in the other script. 
    //It also handles unique UI functions tied to the Upgrades, like the Store. 
#endregion

    //Large sections of codes have been split into Regions for easier viewing. 
{
    #region The Clicker Upgrade Options
    // Allowing the struct to be visible and editable within Unity's Inspector so I can test things live like different values. Used more in the early stages when testing the upgrades and the UI. 
    [System.Serializable]
    // Using Struct collection for the Upgrade values so that we can make as many as we want with varying values in real-time in Unity's Inspector in the UpgradesManager GameObject.
    public struct ClickerUpgradeOptions
    {
       public string name;
       public string description;
       public int value;
       public int price;
       public bool isAuto;
       //Each of the above values can be adjusted and changed in real-time during Design in the Inspector to edit or make new Upgrades. 
       public Text UI;
       public Button button;
       
    }


    //the following connects ClickerUpgrade to ClickerManager
    public ClickerUpgradeOptions[] options;
    //public ClickerJSONSave saveManager;
    public ClickerManager manager;
    // The upgrade store UI. 
    public GameObject shopPanel;
    // New panel to block inputes on the main game so that players can't earn Clicks while in the Upgrade Store.
    public GameObject blockingPanel;
#endregion

   
    private void Start()
    //This function iterates through each of the elements within the ClickerUpgradeOptions array and updates the UI.
    //It displays each value (set in the Unity Inspector) in the corresponding UI element. 
    //It is initialised at Start, meaning when the code is ran/the game is launched, before the first frame is rendered. 
    {
        for (int i = 0; i < options.Length; i++)
        {
            UpdateUI(i);
        }
    }

    #region Upgrade Purchase
    public void Upgrade(int index)
        //This function implements the upgrade Purchase process. 
    {
        // manager.score is referencing the public float score = 0; from ClickerManager. 
        //The upgrades are stored in the options[index].price array, from the ClickerUpgradeOptions struct.
        if (manager.score >= options[index].price)
            //We are checking if the ClickerManager score is greater or equal to the Upgrade's price by using >=
            //This means we are checking if the Player has enough money to even purchase the Upgrades. 
            //IF they do, the following code runs: 
        {
            manager.SetPrice(options[index].isAuto, options[index].value, options[index].price);
            //++ increments the "operand" by 1. 
            //So every time an Upgrade is purchased, the Value goes up by 1. 
            options[index].value ++; 
            // This multiples the cost of the Upgrades by 2. 
            options[index].price *= 2;
            //Updates the UI elements to reflect the new cost and value. 
            UpdateUI(index);
            //This means the Upgrades effectiveness and cost goes up with every purcahse, and this is reflected with the UpdateUI call. 
        }
    }
    #endregion

    //The following is the actual code that handles updating the UI when the player purchases an Upgrade.
    void UpdateUI(int index)
    { 
        //This uses String Interpolation so that it can reflect each individual Upgrade's data in a pre-defined format.
        //This will print as: "Upgrade Name. Upgrade description. Costs $1. Value: X"
        //no matter what Upgrade we apply it to. 
        options[index].UI.text = $"{options[index].name} : {options[index].description}\nPrice: {options[index].price:C} : Value: {options[index].value}";        
    }

    #region Button Control
    //The following controls the Upgrade buttons - It turns them off when the player can't afford the Upgrade, and back on when they can. 
    void isInteractable(int index)
    {
        //This cheks whether the player's Score is equal or greater than the Upgrade price, AND if the button is already OFF.
        if (manager.score >= options[index].price && options[index].button.interactable == false)
        {
            //If both conditions are met, the button is turned on. 
            options[index].button.interactable = true;
        }
        //This checks whether the Score is less than the price and the button is already ON. 
        else if(manager.score < options[index].price && options[index].button.interactable == true)
        {
            //If these are true, it turns the button off. 
            options[index].button.interactable = false;
        }
        //By checking if the button is already off or on before doing the opposite, we aren't constantly running the code. 
    }
    #endregion


    private void Update()
        //This calls the previous function every frame, so that the buttons are up to date and accurately reflect the player's ability to purchase Upgrades. 
    {
        //This checks the buttons interactibility for every Upgrade stored in the options array. 
        for (int i = 0; i < options.Length; i++)
        {
            isInteractable(i);
        }
    }
    // New function to toggle both the Store UI and the blocking Panel at the same time!
    public void ToggleUpgradeStore()
    {
        // Determines if the store is actually open or not: 
        bool isStoreActive = shopPanel.activeSelf;
        Debug.Log("ToggleUpgradeStore called. Current shopPanel active: " + isStoreActive);
        // Toggles the active state of the shopPanel and the blockingPanel.
        shopPanel.SetActive(!isStoreActive);
        blockingPanel.SetActive(!isStoreActive);
        // Pauses the game when the store is open and unpauses it when it closes. 
        Time.timeScale = !isStoreActive ? 0f : 1f;
    }
}




