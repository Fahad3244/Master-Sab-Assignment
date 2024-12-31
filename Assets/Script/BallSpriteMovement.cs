using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpriteMovement : MonoBehaviour
{
    public Vector3 targetPosition; 
    public float speed = 5f; 

    private RectTransform rectTransform;

    private void Start()
    {
        targetPosition = UiController.instance.spriteTarget.GetComponent<RectTransform>().position;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (rectTransform != null)
        {
            rectTransform.position = Vector3.Lerp(rectTransform.position, targetPosition, speed * Time.deltaTime);
            
            if (Vector3.Distance(rectTransform.position, targetPosition) < 0.1f)
            {
                UiController.instance.score++;
                UiController.instance.UpdateScore();
                Destroy(this);
            }
        }
    }
}
