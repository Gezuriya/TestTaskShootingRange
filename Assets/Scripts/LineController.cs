using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    LineRenderer _lineRender;
    Transform[] _points;

    private void Awake()
    {
        _lineRender = GetComponent<LineRenderer>();
    }

    public void SetLine(Transform[] _needPoints)
    {
        _lineRender.positionCount = _needPoints.Length;
        _points = _needPoints;
    }

    private void Update()
    {
        for(int i = 0; i < _points.Length; i++)
        {
            _lineRender.SetPosition(i, _points[i].position);
        }
    }
}
