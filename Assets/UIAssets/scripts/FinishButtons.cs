using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Text Button;
    public GameObject image;
    private Color inColor, outColor;
    public AudioSource song;

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 1;
        switch (Button.name)
        {
            case "PlayAgain":
                //load scene again, lock the mouse and unpause the game
                SceneManager.LoadScene("Level1");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case "BackToMenu":
                SceneManager.LoadScene("Menu");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.gameObject.SetActive(true);
        if (Button != null)
        {
            Button.color = inColor;
        }
        if (song != null)
            song.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.gameObject.SetActive(false);
        if (Button != null)
        {
            Button.color = outColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Button = gameObject.GetComponent<Text>();
        inColor = Color.black;
        outColor = Color.white;
    }

  
}