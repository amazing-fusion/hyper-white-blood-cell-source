using UnityEngine;
using System.Collections.Generic;

namespace com.PancakeTeam
{
    public class FixedUpdateManager :  Singleton<FixedUpdateManager>
    {
        [SerializeField]
        List<IFixedTickable> _componentsList = new List<IFixedTickable>();

        List<IFixedTickable> _addList = new List<IFixedTickable>();

        //float _lastUpdateTime = 0;
        System.DateTime _lastUpdateDateTime;

        public void Add(IFixedTickable component)
        {
            _addList.Add(component);
        }

        public void Remove(IFixedTickable component)
        {
            if (_componentsList.Contains(component)) {
                _componentsList[_componentsList.IndexOf(component)] = null;
            } else if (_addList.Contains(component)) {
                _addList.Remove(component);
            }
            //_componentsList.Remove(component); //This not allow use Remove on Tick
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            foreach (IFixedTickable component in _addList)
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

            List<IFixedTickable> remove = new List<IFixedTickable>();
            foreach (IFixedTickable component in _componentsList)
            {
                if (component != null)
                {
                    component.FixedTick(realDeltaTime);
                }
                else
                {
                    remove.Add(component);
                }
            }

            foreach (IFixedTickable component in remove)
            {
                _componentsList.Remove(component);
            }
        }
    }
}