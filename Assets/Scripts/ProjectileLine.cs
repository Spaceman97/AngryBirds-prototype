using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    static public ProjectileLine S;

    public float minDist = 0.1f;
    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }


    public GameObject poi {
        get {
            return (_poi);
        }
        set
        {
            _poi = value;
            if (_poi != null) {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoint();
            }
        }
    }


    public void Clear() {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }
    public void AddPoint()
    {
        //Вызывается при добавлении точки в линию
        Vector3 pt = _poi.transform.position;
        if (points.Count == 0)
        {
            //если это первая точка 
            Vector3 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;

            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                // Если точек нет, вернуть Vector3.zero
                return (Vector3.zero);
            }
            return (points[points.Count - 1]);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (poi == null)
        {
            // Если свойство poi содержит пустое значение, найти интересующий
            // объект
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                    poi = FollowCam.POI;
                }
                else
                {
                    return; // Выйти, если интересующий объект не найден
                }
            }
            else
            {
                return; // Выйти, если интересующий объект не найден
            }
        }
        // Если интересующий объект найден,
        // попытаться добавить точку с его координатами в каждом FixedUpdate
        AddPoint();
        if (FollowCam.POI == null)
        {
            // Если FollowCam.POI содержит null, записать nulll в poi
            poi = null;
        }
    }
}
