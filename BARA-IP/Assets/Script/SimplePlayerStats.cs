using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimplePlayerStats 
{
    public string userName;
    public int fails;
    public int firstTries;
    public float lowTimer;

    public SimplePlayerStats()
    {

    }


    public SimplePlayerStats(string userName, int fails, int firstTries, float lowTimer)
    {
        this.userName = userName;
        this.fails = fails;
        this.firstTries = firstTries;
        this.lowTimer = lowTimer;
    }
    public string SimplePlayerStatsToJson()
    {
        return JsonUtility.ToJson(this);
    }
}
