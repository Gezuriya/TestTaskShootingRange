using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CannonController : MonoBehaviour
{
    [SerializeField]
    float _offset;

    [SerializeField] GameObject _bulletPref;
    [SerializeField] Transform _shootPoint;
    [SerializeField] GameObject _aim;
    [SerializeField] InterfaceHandler _interface;
    [SerializeField] LineRenderer _line;

    public List<GameObject> _bulletsOnScene;
    public bool _canShoot;

    private void Update()
    {
        //cannon looking at cursor
        if(Time.timeScale == 1)
        {
            Vector2 _cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float _rotation = Mathf.Atan2(_cursorPos.y, _cursorPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(_rotation + _offset, -60f, 60f));

            // aiming and shooting
            if (Input.GetMouseButton(0))
            { 
                var _ray = Physics2D.Raycast(_shootPoint.position, _shootPoint.position - transform.position, Mathf.Infinity);
                _aim.transform.position = _ray.point;
                _aim.SetActive(true);
                _line.enabled = true;
                _canShoot = true;
            }
            else if (Input.GetMouseButtonUp(0) && _canShoot)
            {
                if (_bulletsOnScene.Count == 0)
                    _bulletsOnScene.Add(Instantiate(_bulletPref, _shootPoint.position, transform.rotation));

                _aim.SetActive(false);
                _line.enabled = false;
            }
            _line.SetPosition(0, _aim.transform.position);
            _line.SetPosition(1, _shootPoint.transform.position);
        }
    }
}
