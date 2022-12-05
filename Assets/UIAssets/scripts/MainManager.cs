using Fragsurf.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public float sensivity, fov, musicVol, effectsVol;
    public bool OptionsScene;
    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        PlayerOptions data = SaveSystem.LoadOptions();
        if (data != null)
        {
            sensivity = data.Sensivity;
            fov = data.Fov;
            musicVol = data.MusicVolume;
            effectsVol = data.Effectsvolume;
        }
        else
        {
            sensivity = 1;
            fov = 90;
            musicVol = 40;
            effectsVol = 40;
        }
        OptionsScene = false;
        DontDestroyOnLoad(gameObject);
    }
}
