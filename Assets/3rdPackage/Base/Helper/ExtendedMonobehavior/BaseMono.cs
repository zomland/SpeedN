﻿using System;
using System.Threading;
using UnityEngine;

namespace Base.Helper
{
    public class BaseMono : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Transform _transform;
        private GameObject _gameObject;

        protected CancellationTokenSource _cancellation = new CancellationTokenSource();
        public int InstanceId => CacheGameObject.GetInstanceID();

        public Transform CacheTransform
        {
            get
            {
                if (_transform == null)
                {
                    _transform = transform;
                }

                return _transform;
            }
        }

        public GameObject CacheGameObject
        {
            get
            {
                if (_gameObject == null)
                {
                    _gameObject = gameObject;
                }

                return _gameObject;
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = gameObject.GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        public bool Active
        {
            get => CacheGameObject.activeSelf;
            set => CacheGameObject.SetActive(value);
        }

        public Transform Parent
        {
            get => CacheTransform.parent;
            set => CacheTransform.SetParent(value);
        }

        public Vector3 Position
        {
            get => CacheTransform.position;
            set => CacheTransform.position = value;
        }

        public Quaternion Rotation
        {
            get => CacheTransform.rotation;
            set => CacheTransform.rotation = value;
        }

        public Vector3 EulerAngles
        {
            get => CacheTransform.eulerAngles;
            set => CacheTransform.eulerAngles = value;
        }

        public Vector3 Scale
        {
            get => CacheTransform.localScale;
            set => CacheTransform.localScale = value;
        }

        protected virtual void OnDestroy()
        {
            _cancellation.Cancel();
            _cancellation.Dispose();
        }

        public GameObject CacheInstantiate()
        {
            var obj = Instantiate(CacheGameObject);
            return obj;
        }

        public GameObject CacheInstantiate(Vector3 pos, Quaternion rotate, Transform parent)
        {
            var obj = Instantiate(CacheGameObject, parent.TransformPoint(pos), rotate, parent);
            return obj;
        }
        
        public Transform CacheInstantiate(Transform prefab, Vector3 pos, Quaternion rotate, Transform parent)
        {
            var obj = Instantiate(prefab, parent.TransformPoint(pos), rotate, parent);
            return obj;
        }
        
        public Component CacheInstantiate(Component prefab, Vector3 pos, Quaternion rotate, Transform parent)
        {
            var obj = Instantiate(prefab, parent.TransformPoint(pos), rotate, parent);
            return obj;
        }
    }
}

