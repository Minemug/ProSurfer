using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioScript : MonoBehaviour
{

    public AudioSource music, effects;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0)
        {
            music.Stop();
        }
        if(MainManager.Instance != null)
        {
            effects.volume = MainManager.Instance.effectsVol;
        }else
        {
            Debug.Log("dzwiek nei od managera");
        }
    }
}
