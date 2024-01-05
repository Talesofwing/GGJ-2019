using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager> {
    public Vector2 _sizeRange = new Vector2 (5f, 10f);
    public float _smooth = 4;
    public bool Follow = false;

    private Transform[] _targets;
    private Vector3 _originalPos;
    private float _originalSize;
    private float _originalDistance;
    private Camera _cacheCamera;

    private void Awake () {
        _cacheCamera = GetComponent<Camera> ();

        _originalSize = _cacheCamera.orthographicSize;
        _originalPos = this.transform.position;
    }

    public void Reset () {
        CacheTransform.position = _originalPos;
        _cacheCamera.orthographicSize = _originalSize;
    }

    public void SetTargets (Transform[] targets) {
        _targets = targets;

        _originalDistance = GetDistance ();
    }

    public void SetTarget (Transform target) {
        SetTargets (new Transform [] { target });
    }

    private void Update () {
        if (!Follow)
            return;
    
        if (_targets == null || _targets.Length <= 0)
            return;

        float dt = Time.deltaTime;
        SetToCenterPoint (dt);
        SetOrthographicSize (dt);
    }

    private void SetToCenterPoint (float dt) {
        Vector3 centerPoint = Vector3.zero;
        int length = _targets.Length;
        for (int i = 0; i < length; i++) {
            Vector3 pos = _targets [i].position;
            centerPoint += pos;
        }
        centerPoint /= length;
        centerPoint.z = 0f;
        centerPoint.y = 0f;
        centerPoint += _originalPos;
        CacheTransform.position = Vector3.Lerp (CacheTransform.position, centerPoint, dt * _smooth);
    }

    private void SetOrthographicSize (float dt) {
        float distance = GetDistance ();
        float distanceRatio = CalcDistanceRatio (distance);
        float orthographicSize = _originalSize * distanceRatio;
        float targetSize = Mathf.Clamp (orthographicSize, _sizeRange.x, _sizeRange.y);
        _cacheCamera.orthographicSize = Mathf.Lerp (_cacheCamera.orthographicSize, targetSize, dt * _smooth);
    }

    private float CalcDistanceRatio (float distance) {
        return distance / _originalDistance;
    }

    private float GetDistance () {
        // 找出最左以及最右的角色
        Vector3 left = GetLeftPosition ();
        Vector3 right = GetRightPosition ();
        return right.x - left.x;
    }

    private Vector3 GetLeftPosition () {
        Vector3 result = Vector3.zero;
        int length = _targets.Length;
        for (int i = 0; i < length; i++) {
            Vector3 pos = _targets [i].position;
            if (pos.x <= result.x) {
                result = pos;
            }
        }
        return result;
    }

    private Vector3 GetRightPosition () {
        Vector3 result = Vector3.zero;
        int length = _targets.Length;
        for (int i = 0; i < length; i++) {
            Vector3 pos = _targets [i].position;
            if (pos.x >= result.x) {
                result = pos;
            }
        }
        return result;
    }

}
