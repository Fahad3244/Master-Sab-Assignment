using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Collectables : MonoBehaviour
{
    public GameObject effect;
    private Image spritePrefab; 
    private Canvas canvas;    

    private void Start()
    {
        spritePrefab = UiController.instance.spritePrefab;
        canvas = UiController.instance.canvas;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Instantiate(effect, this.transform.position, Quaternion.identity);
            
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
            
            Image instantiatedSprite = Instantiate(spritePrefab, canvas.transform);
            instantiatedSprite.rectTransform.position = screenPosition;
            Destroy(this.gameObject);
        }
    }
}

