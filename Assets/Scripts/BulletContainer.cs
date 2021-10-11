using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContainer : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Cannon cannon;
    [SerializeField] private Bullet _loadedBullet;    
    private List<Powder> _powders=new List<Powder>();

    private void Start()
    {
        cannon.TurnStartEvent += TurnStartEventHandler;
        cannon.TurnEndEvent += TurnEndEventHandler;
    }

    private void TurnStartEventHandler(object sender, EventArgs e)
    {
        if (_loadedBullet != null)
        {
            _loadedBullet.transform.SetParent(container);
        }
        foreach (var powder in _powders)
        {
            if (powder != null)
            {
                powder.transform.SetParent(container);
            }
        }
    }

    private void TurnEndEventHandler(object sender, EventArgs e)
    {
        if (_loadedBullet != null)
        {
            _loadedBullet.transform.parent = null;
        }
        foreach (var powder in _powders)
        {
            if (powder != null)
            {
                powder.transform.parent = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision with{other.name}");
        var bullet = other.GetComponent<Bullet>();
        if(bullet!=null)
        {
            _loadedBullet = bullet;
            _loadedBullet.transform.rotation = this.transform.rotation;
            return;
        }
        var powder = other.GetComponent<Powder>();
        if(powder!=null)
        {            
            _powders.Add(powder);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Collision exit");

        var bullet = other.GetComponent<Bullet>();
        if(bullet!=null)
        {
            _loadedBullet.transform.parent = null;
            _loadedBullet = null;
            return;
        }

        var powder = other.GetComponent<Powder>();
        if (powder != null)
        {
            _powders.Remove(powder);
            powder.transform.parent = null;
        }
    }
    
    public void Shoot()
    {
        if(_loadedBullet!=null)
        {
            var totalForce=0f;
            foreach (var powder in _powders)
            {
                totalForce += powder.Force;
                Destroy(powder.gameObject);
            }
            _powders = new List<Powder>();
            _loadedBullet.transform.rotation = this.transform.rotation;
            _loadedBullet.Shoot(totalForce);
        }
    }
}
