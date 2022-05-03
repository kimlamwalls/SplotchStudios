using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureLog : MonoBehaviour
{
    private List<Message> Logs = new List<Message>();

    public GameObject LogPanel;
    public GameObject textPrefab;

    private readonly int MaxLogs = 2;
    public void AddEventMessage(string message)
    {
        if (Logs.Count >= MaxLogs)
        {
            var log = Logs[MaxLogs - 1];
            Destroy(log.textObj.gameObject);
            Logs.Remove(log);
        }
        
        var m = new Message();
        // instantiate new prefab inside the log panel
        var newLog = Instantiate(textPrefab, LogPanel.transform);
        m.textObj = newLog.GetComponent<Text>();
        m.textObj.text = message;
        m.msg = message;
        // insert the log at the start of the list
        Logs.Insert(0, m);

    }

    /// <summary>
    /// When player is low on sanity display one of these messages at random
    /// </summary>
    public void LowSanityMessage()
    {
        var possibleMessages = new []
        {
            "Where's the light?",
            "I think i'm seeing things",
            "What was that??",
            "I'm feeling a bit dizzy"
        };
        // select random message
        AddEventMessage(possibleMessages[Random.Range(0, possibleMessages.Length - 1)]);
    }
    
}


[System.Serializable]
public class Message
{
    public string msg;
    public Text textObj;
}