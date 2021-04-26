using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject helpScreen;
    public void ShowHelp()
    {
        helpScreen.SetActive(true);
    }
    public void HideHelp()
    {
        helpScreen.SetActive(false);
    }
    void Start()
    {
    }
}
