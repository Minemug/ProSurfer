using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public float sensivity, fov, musicVol, effectsVol;
    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        sensivity = 1f;
        fov = 90f;
        musicVol = 70f;
        effectsVol = 70f;
        DontDestroyOnLoad(gameObject);
    }
}
