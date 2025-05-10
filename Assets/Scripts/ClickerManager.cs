using System.IO;
using UnityEngine;
using UnityEngine   .UI;
#region Code Purpose
//This is the main game controller for my Clicker Game. It handles most of its core functions, like most of the UI, the user inputs, and contains the base data for the Upgrades.
//It is attached to the in-game GameObject called "ClickerManager". 
//There is a second script that handles the functionality of the Upgrades.
///There is a third script that connects this, the Upgrade script, and the Save manager together.
//There is a fourth script that handles Saves. The game saves in .Json format. 

#endregion
//I have separated large sections of Code into Regions for easier viewing. 

public class ClickerManager : MonoBehaviour
//This class is most of my games base data.
//It all starts at 0 (or at a base value), and will increase as the Player starts earning Points. 
{
    //Value Per Click
    public int clickValue = 1;
    //Score - used for Upgrade purchases
    public float score = 0;
    //total clicks
    public float tClicks = 0;
    //UI Display for Score
    public float tPoints = 0;
    //increase over Time
    public int valueOverTime = 0;
    //timer 
    public float timer = 0;

    //These are the visual UI displays used in-game.
    [SerializeField] Text _scoreDisplay;
    //This will display the player's Current Score (currency for the Shop).
    [SerializeField] Text _totalClickDisplay;
    //This will show the Total, historic click value, not including those from the Autoclickers.
    [SerializeField] Text _totalPointsDisplay;
    //This is the players Total, historic points value, without subtracting those spent on Upgrades. 
    [SerializeField] Text _ppsDisplay;
    //New for the Bitcoine Miner Display
    //This is for the Autoclicker / Farm upgrade specifically, which earns a set value per second for the player based on the Upgrade level. 

    #region Timer
    public void Update() 
    //This function is a Timer, and used later for the Upgrade system.
    {
        //This checks if the valueOverTime function is positive (wich it will be if it is turned on).
        if (valueOverTime > 0)
        {
            //If positive, the timer variable will begin counting with deltaTime.
            //deltaTime is based on seconds, or "how much time has passed since the last frame". 
            //This way we can track the real time. 
            timer += Time.deltaTime;
            if (timer > 1)
            {
                //Now, if the timer is over 1, the player's Score will increase by every second. 
                score += valueOverTime;
                //SO that the player's total points accurately reflects ALL of their earned points
                tPoints += valueOverTime;
                //We call the UI function to reflect the player's score in real-time. 
                UpdateAllUI();
                //and reset the timer, so the process starts again the next second. 
                timer = 0;
                //This way, the score will only increment by 1.
                //Without the reset, the timer would keep counting up every second.
                //And the value added to the score would go up with it.
                //We only want the value added to the score to be multipled by the amount of times the Upgrade tied to this timer has been purchased.
                //So we reset the timer.
            }
        }
    }
    #endregion Timer

    //Main Click Button Behaviour
    public void Click()
    {
        //increases Score by Click Value (default is 1, but is increased with Upgrades). 
        score += clickValue;
        //This tracks how many times the player has manually clicked. Gamers like stats!
        tClicks ++;
        //Player's total historic point score. 
        tPoints += (clickValue);//plus auto value
        //Calls the UI so that it is accurately Updated with every click!
        UpdateAllUI();
    }

    #region UI
    //Helper for the UI Update function, for updating Text elements
    private void UpdateText(Text textDisplay, float value, bool isCurrency)
    {
        //Paramenter added to detmernine whether the text is meant to display currency (like for the store Score)
        if (isCurrency)
        {
            //If the text is a currency, it will use this formatting: 
            textDisplay.text = value.ToString("C0");
        }
        else 
        {
            //If the text isn't currency, it will use this: 
            textDisplay.text = value.ToString("0");
        }
    }
    //END of helper.

    // New cleaner function to update all UI elemnts
    public void UpdateAllUI()
    {
        //The true or false refers to if the text being displayed is a currency.
        //This was set up in the previous helper function "UpdateText". 
        UpdateText(_scoreDisplay, score, true);
        //Score is the player's currency, so it is displayed as a currency, and thus we use "true".
        //The rest of these are not currencies, so we use "false". 
        UpdateText(_totalClickDisplay, tClicks, false);
        UpdateText(_totalPointsDisplay, tPoints, false);

        //New for the Bitcoine Miner Display
        //This display is to show how many Points per second the Autoclicker is earning for the player. 
        _ppsDisplay.text = $"Mining {valueOverTime} Bitcoines per second! Thanks you for your computer!!";
    }
    #endregion UI

    //Below is the old sloppy UI, where it was 3 separate functions. It worked but was messy and I wanted to simplify/neaten it up. 
    #region Original UpdateUI messy

    //public void UpdateUI()
    //{
    //    //this is a function that handles Updating the UI for the Score (the store currency)
    //    _scoreDisplay.text = $"{score:0}";
    //}

    //public void UpdatetCUI()
    //{
    //    //this is a function that handles Updating the UI 
    //    _totalClickDisplay.text = $"{tClicks:0}";
    //}

    //public void UpdatetPointsUI()
    //{
    //    _totalPointsDisplay.text = $"{tPoints:0}";
    //}

    #endregion
    //END old UI. 

    public void SetPrice(bool isAuto, int value, int price)
        //This uses a bool to first determing the Upgrade type being applied: Auto (isAuto), or manual.
    {
        if (isAuto)
        {
            //If an upgrade is toggled as Auto, its value is determined by the valueOverTime function. 
            valueOverTime += value;
        }
        else
        {
            //Otherwise, the upgrade enhances the base click value. 
            clickValue += value;
        }
        //Then, it deducts the cost (price) of the Upgrade from the player's Score. 
        score -= price;
        //Then all of the UI is updated, so that the player can see everything that is happening. 
        UpdateAllUI();
    }
    [SerializeField] public Text Announcements;

}
