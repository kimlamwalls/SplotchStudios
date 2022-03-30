using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureLog : MonoBehaviour
{


    private List<Message> Logs = new List<Message>();

    public GameObject LogPanel;
    public GameObject textPrefab;

    private readonly int MaxLogs = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEventMessage(string message)
    {
        if (Logs.Count >= MaxLogs)
        {
            var log = Logs[MaxLogs - 1];
            Destroy(log.textObj.gameObject);
            Logs.Remove(log);
        }

        //var defaultColour = new Color(1.0f, 1.0f, 1.0f, 0.2f);

        var m = new Message();
        // instantiate new prefab inside the log panel
        var newLog = Instantiate(textPrefab, LogPanel.transform);
        m.textObj = newLog.GetComponent<Text>();
        m.textObj.text = message;
        //m.textObj.color = defaultColour;
        m.msg = message;

        Logs.Insert(0, m);

    }

    private void OnMouseOver()
    {
        Debug.Log("mouse over");
    }

}


[System.Serializable]
public class Message
{
    public string msg;
    public Text textObj;
}