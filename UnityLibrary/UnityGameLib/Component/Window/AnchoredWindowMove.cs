using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using CommonlyUsed;
using DesignPattern;

public class AnchoredWindowMove : MonoBehaviour,IUpdateManager
{
    [SerializeField,Header("移動させたいcomponent")]
    RectTransform rectTransform;

    [SerializeField,Header("目的の座標")]
    Vector2 target;

    [SerializeField,Header("移動の速さ")]
    float speed;

    Vector2 vec2;

    private void OnDisable()
    {
        rectTransform.anchoredPosition = vec2;
    }

    void Start() 
    {
        vec2 = rectTransform.anchoredPosition;
     
        UpdateManager.Instance.Bind(this,FrameControl.ON);  
    }

    public void OnUpdate(double deltaTime)
    {
        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition,target,speed * Time.deltaTime);
    }
}
    