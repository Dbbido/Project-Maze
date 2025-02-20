using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerExit : MonoBehaviour
{
    public TextMeshProUGUI winText;
    private bool hasWon = false;

    private void Awake()
    {
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasWon = true;

            winText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hasWon = false;
        if (other.CompareTag("Player"))
        {
            if (!hasWon)
            {
                winText.gameObject.SetActive(false);
            }
        }
    }
}
