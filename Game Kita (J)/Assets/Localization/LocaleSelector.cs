using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.Localization.Settings;
using UnityEngine.UI;
public class LocaleSelector : MonoBehaviour
{
    public Dropdown localeDropdown;

    private bool active = false;

    void Start()
    {
        localeDropdown.onValueChanged.AddListener(ChangeLocale);
        // Optionally set the initial locale
        ChangeLocale(localeDropdown.value);
    }

    public void ChangeLocale(int localeID)
    {
        if (active)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        active = false;
    }
}