using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocalizeText : MonoBehaviour
{
    [SerializeField] private string key;
    private TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        if (key != "") text.text = Localization.ReturnTranslation(key);
    }
}
