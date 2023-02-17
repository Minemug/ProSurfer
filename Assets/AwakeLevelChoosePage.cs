using Fragsurf.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeLevelChoosePage : MonoBehaviour
{
    public GameObject[] locks;

    private void Awake()
    {
        PlayerWonLevels data = SaveSystem.LoadWonLevels();
        if (data != null)
        {
            for (int i = 0; i < locks.Length; i++)
            {
                locks[i].SetActive(data.wonLevels[i+1]);
            }
        }
    }
}
