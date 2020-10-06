using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    public string url = "https://dev3r02.elysium.today/inventory/status";
    public string auth = "";

    public void PutItem(BackpackItem item)
    {
        SendItemEvent(item.id, "put");
    }

    public void PullItem(BackpackItem item)
    {
        SendItemEvent(item.id, "pull");
    }

    private void SendItemEvent(int id, string action)
    {
        string json = $"{{\"id\": {id},\"action\": \"{action}\"}}";
        StartCoroutine(JSONCoroutine(json));
    }

    IEnumerator JSONCoroutine(string json)
    {
        var www = UnityWebRequest.Post(url, json);
        www.SetRequestHeader("auth", auth);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("All systems go!");
        }
    }
}
