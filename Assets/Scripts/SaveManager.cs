using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour 
{
    public string filePath = Application.dataPath + "/ClickerSave.json";
    public SaveData data = new SaveData();
    // even though these are public in their own scripts, our references to them here need to be public because this script doesn't know what they are otherwise. Serialise wouldn't work as that is for the Unity editor. 
    public ClickerManager manager;
    public ClickerUpgrade upgrade;

    #region Save
    /*
     To save we want to
    1) Have a function that can Send our data from the game to SaveData class
    2) Have a function that can Write that to a file
    3) Have a function that runs both when we call
     */
    /// <summary>
    /// This function will allow you to send data to the SaveData class 
    /// Once it is sent we can then in our main Save function run the writing function to actually save
    /// </summary>
    void SendDataToSaveDataToSave()
        // This is a function that overwrites the original data 
    {
        data.score = manager.score;
        data.clickValue = manager.clickValue;
        data.tClicks = manager.tClicks;
        data.tPoints = manager.tPoints;
        data.valueOverTime = manager.valueOverTime;
        data.timer = manager.timer;

    }
    void WriteJSONFile(SaveData data)
        // This writes the values into the files
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }
    public void ActuallySave()
        // This function runs both previous ones when we call it, thus making that ACTUAL SAVE function.
    {
        SendDataToSaveDataToSave();
        WriteJSONFile(data);
    }
    #endregion
    #region Load
    /*
    To save we want to
  
   1) Have a function that can Read a file
   2) Have a function that can Send our data from the SaveData class to the game
   3) Have a function that runs both when we call
    */
    SaveData ReadJSONFile()
    {
        // This checks if there is a save file present and then reads it. If there isn't one it won't pull from the save. 
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }
    void SendDataToGameFromSaveData()
        // This sends the loaded data from the Save file into the game, overwriting the in-game data with the load data.  
    {
        manager.score = data.score;
        manager.clickValue = data.clickValue;
        manager.tClicks = data.tClicks; 
        manager.tPoints = data.tPoints;
        manager.valueOverTime = data.valueOverTime;
        manager.timer = data.timer;
    }
    public void ActuallyLoad()
        // This function does both of the last two functions in order, thus doing the full Load function.
    {
        data = ReadJSONFile();
        SendDataToGameFromSaveData();
        manager.UpdatetCUI();
        manager.UpdatetPointsUI();
        manager.UpdateUI();
    }
    #endregion

   
}
