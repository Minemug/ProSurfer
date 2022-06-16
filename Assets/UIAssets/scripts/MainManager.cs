using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public float sensivity;
    public float fov;

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
        DontDestroyOnLoad(gameObject);
    }
}
