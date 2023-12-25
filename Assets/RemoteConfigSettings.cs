using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class RemoteConfigSettings : MonoBehaviour
{
    string Game_Version;

    MainMenuUI meinMenuUI;
     public void Awake()
    {
         meinMenuUI = FindObjectOfType<MainMenuUI>();
         FetchDataAsync();

    }

    public Task FetchDataAsync()
    {
        Debug.Log("Fetching data...");
        System.Threading.Tasks.Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }


    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval hasn't finished.");
            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync().ContinueWithOnMainThread(task => {

        Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

 

            foreach(var item in remoteConfig.AllValues)
            {
                string fetchedGameVersion = item.Value.StringValue;

                Debug.Log(fetchedGameVersion);

                if (fetchedGameVersion == Application.version)
                {
                    return;
                }
                else
                {
                    meinMenuUI.ActivateUpdatePanel();
                    meinMenuUI.GetUpdatePanelMessageText().text = remoteConfig.GetValue("Update_Message").StringValue;
                }
            }
 

        });
    }

  

}
