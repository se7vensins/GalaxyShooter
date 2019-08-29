using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _yUpperLim;



    void Update()
    {
        lasermove();
        laserbounds();
    }

    private void lasermove()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void laserbounds()
    {
        if (transform.position.y >= _yUpperLim)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }
}
