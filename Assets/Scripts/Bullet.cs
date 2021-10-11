using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;   
    public void Shoot(float Speed)
    {        
        rigidbody.AddRelativeForce(new Vector3(0, Speed, 0));
    }
}
