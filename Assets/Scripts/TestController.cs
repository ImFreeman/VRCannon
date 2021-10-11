using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    [SerializeField] private Cannon cannon;
    [SerializeField] private float power = 0.5f;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            cannon.Shoot();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cannon.VerticlaRotation(-power);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cannon.VerticlaRotation(power);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cannon.HorizontalRotation(-power);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cannon.HorizontalRotation(power);
        }
    }    
}
