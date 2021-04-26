//Created by Allen Devs 2020, free to use in your game, don't sweat it, enjoy!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueWindow : MonoBehaviour
{
    const string kAlphaCode = "<color=#00000000>";
    const float kMaxTextTime = 0.1f;
    public static int TextSpeed = 2;

    public TMP_Text Text;
    private string CurrentText;

    Animator Anim;

    void Start()
    {
        Anim = GetComponent<Animator>();
        if (Anim == null)
        {
            Debug.LogError("No Animator Controller on DialogueWindow: " + gameObject.name);
        }
    }

    public void Show(string text)
    {
        Anim?.SetBool("Open", true);
        CurrentText = text;       
    }

    public void Close()
    {        
        Anim?.SetBool("Open", false);
    }

    public void OnDialogueOpen()
    {
        StartCoroutine(DisplayText());
    }

    public void OnDialogueClosed()
    {
        StopAllCoroutines();
        Text.text = "";
    }

    private IEnumerator DisplayText()
    {
        if (Text == null)
        {
            Debug.LogError("Text is not linked in DialogueWindow: " + gameObject.name);
            yield return null;
        }

        Text.text = "";

        string originalText = CurrentText;
        string displayedText = "";
        int alphaIndex = 0;

        foreach(char c in CurrentText.ToCharArray())
        {
            alphaIndex++;
            Text.text = originalText;
            displayedText = Text.text.Insert(alphaIndex, kAlphaCode);
            Text.text = displayedText;

            yield return new WaitForSecondsRealtime(kMaxTextTime / TextSpeed);
        }

        yield return null;
    }
}
