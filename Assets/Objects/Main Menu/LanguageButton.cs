using Polyglot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageButton : MonoBehaviour
{
    private int currentSelected = 0;
    private int nmbrOfLanguages = 0;
    private void Awake()
    {
        currentSelected = Localization.Instance.SelectedLanguageIndex;
        nmbrOfLanguages = Localization.Instance.EnglishLanguageNames.Count;
    }
    public void StepThroughLanguages()
    {
        var languageNames = Localization.Instance.LocalizedLanguageNames;
        currentSelected++;
        if (currentSelected >= nmbrOfLanguages)
        {
            currentSelected = 0;
        }
        Localization.Instance.SelectLanguage(currentSelected);
    }
}
