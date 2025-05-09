
using UnityEngine;
using UnityEngine.UI;

public class ClickerManager : MonoBehaviour
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

    [SerializeField] Text _scoreDisplay;
    [SerializeField] Text _totalClickDisplay;
    [SerializeField] Text _totalPointsDisplay;
    public void Update() //This function is used later for the Upgrade system.
    {
        if (valueOverTime > 0)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                score += valueOverTime;
                UpdateUI();
                timer = 0;
            }
        }
    }

    //Main Click Button Behaviour
    public void Click()
    {
        //increases Score by Click Value
        score += clickValue;
        UpdateUI();

        tClicks ++;
        UpdatetCUI();

        tPoints += (clickValue);//plus auto value
        UpdatetPointsUI();
    }
    public void UpdateUI()
    {
        //this is a function that handles Updating the UI for the Score (the store currency)
        _scoreDisplay.text = $"{score:0}";
    }

    public void UpdatetCUI()
    {
        //this is a function that handles Updating the UI 
        _totalClickDisplay.text = $"{tClicks:0}";
    }

    public void UpdatetPointsUI()
    {
        _totalPointsDisplay.text = $"{tPoints:0}";
    }

    public void SetPrice(bool isAuto, int value, int price)
    {
        if (isAuto)
        {
            valueOverTime += value;
        }
        else
        {
            clickValue += value;
        }
        score -= price;
        //This closes the Function
        UpdateUI();
    }



}
