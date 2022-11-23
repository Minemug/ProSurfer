using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuWakeUp : MonoBehaviour
{
    public GameObject MainMenu, Options;
    // Start is called before the first frame update
    private void Start()
    {
        if (MainManager.Instance.OptionsScene)
        {
            MainManager.Instance.OptionsScene = false;
            MainMenu.SetActive(false);
            Options.SetActive(true);
        }
    }
}
