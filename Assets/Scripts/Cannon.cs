using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Cannon : MonoBehaviour
{
    [SerializeField] private BulletContainer bulletContainer;
    [SerializeField] private LinearMapping linearMappingHorizontal;
    [SerializeField] private LinearMapping linearMappingVertical;
    [SerializeField] private HoverButton button;
    public EventHandler TurnStartEvent;
    public EventHandler TurnEndEvent;

    private float _currentAngleX=0f;
    private float _currentAngleY=0f;
    private float _currentAngleZ=0f;

    private float _prevLinearMappingVertical = 0f;
    private float _prevLinearMappingHorizontal = 0f;

    public void Shoot()
    {
        bulletContainer.Shoot();
    }

    public void HorizontalRotation(float value)
    {
        TurnStartEvent?.Invoke(this, EventArgs.Empty);
        _currentAngleY += value;
        transform.rotation = Quaternion.Euler(_currentAngleX, _currentAngleY, _currentAngleZ);
        TurnEndEvent?.Invoke(this, EventArgs.Empty);
    }

    public void VerticlaRotation(float value)
    {
        TurnStartEvent?.Invoke(this, EventArgs.Empty);
        _currentAngleX = Mathf.Max(0, Mathf.Min(90, _currentAngleX + value));
        transform.rotation = Quaternion.Euler(_currentAngleX, _currentAngleY, _currentAngleZ);
        TurnEndEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {
        button.onButtonDown.AddListener(OnButtonDownHandler);
    }

    private void OnButtonDownHandler(Hand hand)
    {
        Shoot();
    }

    private void Update()
    {
        var v = linearMappingVertical.value - _prevLinearMappingVertical;
        if(Mathf.Abs(v)>=0.001)
        {
            VerticlaRotation(v * 90f);            
        }
        _prevLinearMappingVertical = linearMappingVertical.value;
        
        var h = linearMappingHorizontal.value - _prevLinearMappingHorizontal;
        if (Mathf.Abs(h) >= 0.001)
        {
            HorizontalRotation(h * 360f);
        }
        _prevLinearMappingHorizontal = linearMappingHorizontal.value;

    }
}
