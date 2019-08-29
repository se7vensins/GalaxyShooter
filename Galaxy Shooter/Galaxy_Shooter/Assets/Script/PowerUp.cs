using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _yLowerLim;
    [SerializeField] // 0 = tripleshot, 1 = speedup, 2 = shieldup
    private int powerUpID;

    [SerializeField]
    private AudioClip _audioClip;


    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= _yLowerLim)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_audioClip, transform.position,100f);
            Player _player = other.transform.GetComponent<Player>();
            if(_player != null)
            {
                switch (powerUpID)
                {
                    case 0:
                        _player.TripleShotEnabled();
                       
                        break;
                    case 1:
                        _player.SpeedUPEnabled();
                        break;
                    case 2:
                        _player.ShieldUpEnabled();
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}

