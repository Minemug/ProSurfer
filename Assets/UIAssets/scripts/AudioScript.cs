using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{

    public AudioSource music, effects, menuMusic;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            menuMusic.Stop();
        }

        if(MainManager.Instance != null)
        {
            effects.volume = MainManager.Instance.effectsVol;
            music.volume = MainManager.Instance.musicVol;
        }else
        {
            Debug.Log("dzwiek nie ustawiony przez managera");
        }
    }
}
