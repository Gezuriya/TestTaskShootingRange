using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetController : MonoBehaviour
{
    int _health;
    string _typeOfTarget;
    [SerializeField] Sprite[] _sprites;
    bool _isMoving;
    int _moveSpeed = 5;
    SpriteRenderer _spriteRender;
    [SerializeField] TextMeshProUGUI _lifeText;
    float frequency;
    Vector2 amplitudeVector = new Vector2(0, 1.5f);
    Vector2 startPos;
    FactoryHandler _factory;


    public void Spawned(string type, FactoryHandler factory) //factory creating a type of target
    {
        _factory = factory;
        _spriteRender = GetComponent<SpriteRenderer>();
        _typeOfTarget = type;
        switch (type)
        {
            case "Ordinary":
                _health = 1; 
                _moveSpeed = 4;
                _spriteRender.sprite = _sprites[0];
                break;
            case "Elite":
                _health = 2;
                _moveSpeed = 6;
                _spriteRender.sprite = _sprites[1];
                transform.localScale = new Vector2(0.6f, 0.6f);
                break;
            case "Epic":
                _health = 3;
                _spriteRender.sprite = _sprites[2];
                transform.localScale = new Vector2(0.4f, 0.4f);
                startPos = transform.position;
                frequency = Random.Range(-0.2f, 0.6f);
                break;
        }
        _lifeText.text = _health.ToString();  
    }

    public void GetDamage() //target is getting hitted
    {
        _health--;
        _lifeText.text = _health.ToString();
        if (_health == 0)
        {
            _factory._spawnedObjects.Remove(gameObject);
            if(_factory._spawnedObjects.Count == 0)
            {
                FindObjectOfType<InterfaceHandler>().GameIsOver(true);
            }
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        //moving target according to its type
        if(_typeOfTarget == "Ordinary" || _typeOfTarget == "Elite")
        {
            if (_isMoving)
                transform.Translate(Vector2.left * _moveSpeed * Time.deltaTime);
            else
                transform.Translate(Vector2.right * _moveSpeed * Time.deltaTime);
        }
        else if(_typeOfTarget == "Epic")
        {
            float sin = Mathf.Sin(Time.time * frequency * 2f * Mathf.PI);
            Vector2 offset = amplitudeVector * sin;
            transform.position = startPos + offset;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Background" || collision.tag == "Enemy")
        {
            _isMoving = !_isMoving;
        }
    }
}
