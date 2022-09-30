using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class InterfaceHandler : MonoBehaviour
{
    public int _timer; 
    int _timeLeft;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] CannonController _cannonCont;
    [SerializeField] FactoryHandler _factory;
    public GameObject _startPan, _winPan, _loosePan;
    void Start()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        _timeLeft = _timer;
        _cannonCont._canShoot = false;
        _startPan.SetActive(false);
        _winPan.SetActive(false);
        _loosePan.SetActive(false);
        Time.timeScale = 1;
        _factory.Spawn();
        StartCoroutine(Timer());
    }

    //timer controller
    IEnumerator Timer()
    {
        while(_timeLeft >= 0)
        {
            if(_timeLeft % 60 >= 10)
                _timerText.text = (_timeLeft / 60).ToString() + ":" + _timeLeft % 60;
            else
                _timerText.text = (_timeLeft / 60).ToString() + ":0" + _timeLeft % 60;
            yield return new WaitForSeconds(1);
            _timeLeft--;
            if (_timeLeft == 0)
                GameIsOver(false);
        }
    }
    public void GameIsOver(bool isWin)
    {
        Time.timeScale = 0;
        StopAllCoroutines();
        if (isWin)
        {
            _winPan.SetActive(true);
        }
        else
        {
            foreach(GameObject target in _factory._spawnedObjects)
            {
                Destroy(target);
            }
            _factory._spawnedObjects.Clear();
            _loosePan.SetActive(true);
        }
    }

}
