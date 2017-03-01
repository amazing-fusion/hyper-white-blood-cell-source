using UnityEngine;
using System.Collections.Generic;

namespace com.AmazingFusion
{
    public class LateUpdateManager : Singleton<LateUpdateManager>
    {
        [SerializeField]
        List<ILateTickable> _componentsList = new List<ILateTickable>();

        List<ILateTickable> _addList = new List<ILateTickable>();

        //float _lastUpdateTime = 0;
        System.DateTime _lastUpdateDateTime;

        public void Add(ILateTickable component)
        {
            _addList.Add(component);
        }

        public void Remove(ILateTickable component)
        {
            if (_componentsList.Contains(component)) {
                _componentsList[_componentsList.IndexOf(component)] = null;
            } else if (_addList.Contains(component)) {
                _addList.Remove(component);
            }
            //_componentsList.Remove(component);  //This not allow use Remove on Tick
        }

        // Update is called once per frame
        void LateUpdate()
        {
            foreach (ILateTickable component in _addList)
            {
                if (component != null && !_componentsList.Contains(component))
                {
                    _componentsList.Add(component);
                }
            }
            _addList.Clear();

            //float realDeltaTime = _lastUpdateTime == 0 ? Time.unscaledDeltaTime : Time.realtimeSinceStartup - _lastUpdateTime;
            //_lastUpdateTime = Time.realtimeSinceStartup;

            float realDeltaTime = (float)System.DateTime.Now.Subtract(_lastUpdateDateTime).TotalSeconds;
            _lastUpdateDateTime = System.DateTime.Now;

            List<ILateTickable> remove = new List<ILateTickable>();
            foreach (ILateTickable component in _componentsList)
            {
                if (component != null)
                {
                    component.LateTick(realDeltaTime);
                }
                else
                {
                    remove.Add(component);
                }
            }

            foreach (ILateTickable component in remove)
            {
                _componentsList.Remove(component);
            }
        }
    }
}