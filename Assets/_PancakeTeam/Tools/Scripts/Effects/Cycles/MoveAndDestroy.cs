using UnityEngine;
using System.Collections;

namespace com.PancakeTeam {
    public class MoveAndDestroy : OptimizedBehaviour, ITickable
    {
        public enum DestroyMode
        {
            Component,
            GameObject,
            Parent,
            Root
        }

        [SerializeField]
        Vector2 _velocity;

        [SerializeField]
        float _duration;

        [SerializeField]
        DestroyMode _mode = DestroyMode.GameObject;

        RectTransform _rectTransform;
        float _startTime;

        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }

            set
            {
                _velocity = value;
            }
        }

        public float Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                _duration = value;
            }
        }

        public DestroyMode Mode
        {
            get
            {
                return _mode;
            }

            set
            {
                _mode = value;
            }
        }

        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startTime = Time.time;
            UpdateManager.Instance.Add(this);
        }

        void OnDestroy()
        {
            if (UpdateManager.HasInstance)
            {
                UpdateManager.Instance.Remove(this);
            }
        }

        public void Tick(float realDeltaTime)
        {
            if (Time.time > _startTime + _duration)
            {
                switch (_mode)
                {
                    case DestroyMode.Component:
                        Destroy(this);
                        break;
                    case DestroyMode.GameObject:
                        Destroy(gameObject);
                        break;
                    case DestroyMode.Parent:
                        Destroy(transform.parent.gameObject);
                        break;
                    case DestroyMode.Root:
                        Destroy(transform.root.gameObject);
                        break;
                }
            }
            else
            {
                _rectTransform.anchoredPosition += _velocity * Time.deltaTime;
            }
        }
    }

}
