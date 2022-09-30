using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float _moveSpeed;
    CannonController _cannonCont;

    private void Start()
    {
        _cannonCont = FindObjectOfType<CannonController>();
    }
    void Update()
    {
        transform.Translate(Vector2.up * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Background":
                _cannonCont._bulletsOnScene.Remove(gameObject);
                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<TargetController>().GetDamage();
            _cannonCont._bulletsOnScene.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
