using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This must be done for saving purposes
[System.Serializable]
public class PlayerData
{
    //Setup our variables we want to save
    public int level;

    //Default constructor to setup the object
    public PlayerData(int level)
    {
        this.level = level;
    }
}
