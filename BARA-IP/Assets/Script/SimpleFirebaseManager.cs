using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

public class SimpleFirebaseManager : MonoBehaviour
{

    DatabaseReference dbPlayerStatsReference;
    DatabaseReference dbLeaderboardsReference;

    public void Awake()
    {
        InitializeFirebase();
    }

    public void InitializeFirebase()
    {
        dbPlayerStatsReference = FirebaseDatabase.DefaultInstance.GetReference("PlayerStats");
        dbLeaderboardsReference = FirebaseDatabase.DefaultInstance.GetReference("Leaderboards");

    }

   
    public void UpdatePlayerStats(string uuid, int fails, int firstTries, float timer, string displayName)
    {
        Query playerQuery = dbPlayerStatsReference.Child(uuid);

        playerQuery.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError("Sorry, there was an error creating your entries, ERROR" + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot playerStats = task.Result;
                if (playerStats.Exists)
                {
                    SimplePlayerStats sp = JsonUtility.FromJson<SimplePlayerStats>(playerStats.GetRawJsonValue());
                    sp.fails = fails;
                    sp.firstTries = firstTries;
                    
                    if(timer < sp.lowTimer)
                    {
                        sp.lowTimer = timer;
                        UpdatePlayerLeaderBoardEntry(uuid, sp.lowTimer, sp.fails);
                    }

                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                }
                else
                {
                    SimplePlayerStats sp = new SimplePlayerStats(displayName, fails, firstTries, timer);
                    SimpleLeaderBoard lb = new SimpleLeaderBoard(displayName, timer, fails);



                    dbPlayerStatsReference.Child(uuid).SetRawJsonValueAsync(sp.SimplePlayerStatsToJson());
                    dbLeaderboardsReference.Child(uuid).SetRawJsonValueAsync(lb.SimpleLeaderBoardToJson());


                }
            }
        });
    }

    public void UpdatePlayerLeaderBoardEntry(string uuid, float lowTimer, int fails)
    {
        dbLeaderboardsReference.Child(uuid).Child("LowestTime").SetValueAsync(lowTimer);
        dbLeaderboardsReference.Child(uuid).Child("fails").SetValueAsync(fails);
    }




}
