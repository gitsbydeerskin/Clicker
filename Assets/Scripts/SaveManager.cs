using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour 
{
    //Had to change this from Public to fix a bug. 
    //Anyway, this is us actually telling our game WHERE to put its save file. 
    string filePath = Application.dataPath + "/ClickerSave.json";
    //We're telling it to save to the Application's data path, and then name the save file "/ClickerSave.json". 
    //So for example, the save would be: C/Users/Unity/ClickerGame/ClickerSave.json.

    public SaveData data = new SaveData();
  
    public ClickerManager manager;
    public ClickerUpgrade upgrade;
    // even though these are public in their own scripts, our references to them here need to be public because this script doesn't know what they are otherwise. Serialise wouldn't work as that is for the Unity editor. 

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
        //It gets all of the important data from the ClickerManager and ClickerUpgrade scripts
        //That's why we had the reference them previously. 
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
        //This uses Unity's in-built method to serialise our previously defined data into a JSON-formatted string. 
        string json = JsonUtility.ToJson(data);
        //Now, we write this JSON-formatted string into a file. We can load this string back later in the Load section to restore our data. 
        File.WriteAllText(filePath, json);
        //There is no prevention in place for overwriting, which is intentional: players have only one save, and resume immediately from that save when they launch the game. 
    }
    public void ActuallySave()
        // This function runs both previous ones when we call it, thus making that ACTUAL SAVE function.
    {
        SendDataToSaveDataToSave();
        WriteJSONFile(data);
    }

    //This is a new function added so that the Player can exit AND save the game from the main screen. 
    public void SaveAndExit()
    {
        SendDataToSaveDataToSave();
        WriteJSONFile(data);
        //First we do the Save function. If we quit first, it wouldn't have time to save. 
        //Now, we detect if we're in the Unity Editor, in case we're testing: 
        #if UNITY_EDITOR
        //Then quit out of it: 
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //But if we're not in the Unity Editor, we Quit the game: 
        Application.Quit();
    }
    #endregion

    #region Load
    /*
    To load we want to
  
   1) Have a function that can Read a file
   2) Have a function that can Send our data from the SaveData class to the game
   3) Have a function that runs both when we call
    */
    SaveData ReadJSONFile()
    {
        // This checks if there is a save file present and then reads it. If there isn't one it won't pull from the save. 
        if (File.Exists(filePath))
        {
            //If the file is found, it reads all of its text in a string variable, which is JSON-formatted data representing our saved game state. 
            string json = File.ReadAllText(filePath);
            //This uses Unity's in-built conversion method to convert the JSON string into an object type. Ie, our data.
            return JsonUtility.FromJson<SaveData>(json);
        }
        //However, if there is no save data found, it will return null, ie, won't pull anything. 
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
        //Again, this is all the important data from earlier, that we save. 
    }
    public void ActuallyLoad()
        // This function does both of the last two functions in order, thus doing the full Load function.
    {
        data = ReadJSONFile();
        SendDataToGameFromSaveData();
        manager.UpdateAllUI();
    }
    #endregion

    //This function handles loading the save data when the player loads into the game. 
    private void Awake()
    {
        //First it checks if a save file exists. 
        if (File.Exists(filePath))
        {
            //If it does, it loads it. 
            string json = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<SaveData>(json);
            SendDataToGameFromSaveData();
            //This updates the UI to reflect the saved data. 
            manager.UpdateAllUI();
            //This is a cute message to greet the player. 
            Debug.Log("Welcome back slime! Get mining!");
        }
        //If it doesn't, this code will run: 
        else
        {
            //It is just a cute lil greeting message. 
            Debug.Log("First time huh? Welcome to hell.");
        }
    }
}
