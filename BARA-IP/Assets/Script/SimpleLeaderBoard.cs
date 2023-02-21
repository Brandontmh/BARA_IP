using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleLeaderBoard 
{

    public string userName;
    /*public int highScore;*/
    /*public long updatedOn;*/
    public float lowTimer;
    public int fails;

    public SimpleLeaderBoard() { }

    /*public SimpleLeaderBoard(string userName, int highScore)
    {
        this.userName = userName;
        this.highScore = highScore;
        this.updatedOn = GetTimeUnix();
        
    }*/

    public SimpleLeaderBoard(string userName, float lowTimer, int fails)
    {
        this.userName = userName;
        this.lowTimer = lowTimer;
        this.fails = fails ;

    }

    /*public long GetTimeUnix()
    {
        return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
    }*/

    public string SimpleLeaderBoardToJson()
    {
        return JsonUtility.ToJson(this);
    }

}
