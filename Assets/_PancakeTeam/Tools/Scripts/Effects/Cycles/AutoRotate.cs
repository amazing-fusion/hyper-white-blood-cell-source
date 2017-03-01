using UnityEngine;
using System.Collections.Generic;
using MovementEffects;
using System;

namespace com.PancakeTeam
{
    public class AutoRotate : OptimizedBehaviour, ITickable
    {
        [SerializeField]
        Vector3 _angularSpeed;

        void OnEnable()
        {
            UpdateManager.Instance.Add(this);
        }

        void OnDisable()
        {
            if (UpdateManager.HasInstance)
            {
                UpdateManager.Instance.Remove(this);
            }
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
            Transform.Rotate(_angularSpeed * Time.deltaTime, Space.Self);
        }
    }
}