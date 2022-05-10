using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.02f;

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]

    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private static DialogueManager instance;

    //private float time = 1.0f;

    //private float timer;

    private void Awake()
    {
        //timer = Time.time;
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // get all choices text
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        //yield return new WaitForSeconds(0.2f);
        UpdateVariableFromStoryEnd();
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for current dialogue line
            StartCoroutine(DisplayLine(currentStory.ContinueMaximally()));
            // display choices, if any, for this dialogue line
            //DisplayChoices();
            bool closeTriggered = (bool)currentStory.variablesState["closeTriggered"];
            // exits dialogue when closeTriggered becomes true in Ink/JSON
            if (closeTriggered == true)
            {
                ExitDialogueMode();
            }
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // set the text to full line, but set visible character to 0
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        //display each character at a time
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(typingSpeed);
        }

        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices than UI can support.");
        }

        // enable and initialise choices
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
    }

    //private void DisplayChoices()
    //{
    //    List<Choice> currentChoices = currentStory.currentChoices;

    //    if (currentChoices.Count > choices.Length)
    //    {
    //        Debug.LogError("More choices than UI can support.");
    //    }

    //    // enable and initialise choices
    //    int index = 0;
    //    foreach (Choice choice in currentChoices)
    //    {
    //        choices[index].gameObject.SetActive(true);
    //        choicesText[index].text = choice.text;
    //        index++;
    //    }
    //}

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        ContinueStory();
    }

    private void UpdateVariableFromStoryEnd()
    {
        var scriptEnding = currentStory.variablesState["scriptEnding"];
        Debug.Log(scriptEnding);
        if (scriptEnding.Equals("remember"))
        {
            //increases player movement speed
            PlayerMovement.GetInstance().modifyMoveSpeed(5);
        }
        else if (scriptEnding.Equals("calm"))
        {
            //decreases sanity multiplier
            PlayerMovement.GetInstance().modifySanityMultiplier(-0.2f);
        }
        else if (scriptEnding.Equals("touch"))
        {
            //increases attack range
            PlayerMovement.GetInstance().modifyAttackRange(0.1f);
            //increases sanity multiplier
            PlayerMovement.GetInstance().modifySanityMultiplier(0.3f);
        }
        else if (scriptEnding.Equals("stop"))
        {
            Debug.Log("stop script triggered");
        }
    }
}
