using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLVL : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image image;
    public AudioSource song;
    private Image Thumbnail;


    public void OnPointerClick(PointerEventData eventData)
    {
            switch (Thumbnail.name)
            {
                case "LVL1":
                    SceneManager.LoadScene("Level1");
                    break;
                case "LVL2":
                    SceneManager.LoadScene("Level2");
                    break;
                case "TUT":
                    SceneManager.LoadScene("Tutorial");
                    break;
                case "LVL3":
                    SceneManager.LoadScene("Level3");
                    break;
                
            }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.gameObject.SetActive(true);
        if (song != null)
            song.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.gameObject.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Thumbnail = gameObject.GetComponent<Image>();
    }
}

   
