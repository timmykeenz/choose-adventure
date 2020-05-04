using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(PlayerData data)
    {
        //Create a formatter object to securely save our game file
        BinaryFormatter formatter = new BinaryFormatter();
        //Gets a path that won't change for safe saving
        string path = Application.persistentDataPath + "/player.st";
        //Setup our file stream to read/write data
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            //Serialize the data (Turn into binary)
            formatter.Serialize(stream, data);
        } //At the end of the using block, the FileStream will be closed
    }

    public static PlayerData LoadPlayer()
    {
        //Grab the path we want to load
        string path = Application.persistentDataPath + "/player.st";
        //Check if the file exists
        if (File.Exists(path))
        {
            //Create a formatter object that can securely decrypt our save file
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data;
            //Setup our filestream to open the file
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                //Decrypt the file back into a readible format
                data = formatter.Deserialize(stream) as PlayerData;
            }
            //Return the data
            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
