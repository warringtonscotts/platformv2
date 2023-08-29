using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemCollector : MonoBehaviour
{

    private int apples = 0;

    [SerializeField] private Text AppleText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("apple"))
        {
            Destroy(collision.gameObject);
            apples++;

            AppleText.text = "Apples: " + apples; 

        }
    }
}
