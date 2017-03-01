using UnityEngine;
using System.Collections.Generic;

namespace com.AmazingFusion
{
    public class UpdateManager : Singleton<UpdateManager>
    {
        [SerializeField]
        List<ITickable> _componentsList = new List<ITickable>();

        List<ITickable> _addList = new List<ITickable>();

        //float _lastUpdateTime = 0;
        System.DateTime _lastUpdateDateTime;

        public void Add(ITickable component)
        {
            _addList.Add(component);
        }

        public void Remove(ITickable component)
        {
            if (_componentsList.Contains(component)) {
                _componentsList[_componentsList.IndexOf(component)] = null;
            } else if (_addList.Contains(component)) {
                _addList.Remove(component);
            }
            //_componentsList.Remove(component);  //This not allow use Remove on Tick
        }

        void Start()
        {
            _lastUpdateDateTime = System.DateTime.Now;
        }

        // Update is called once per frame
        void Update()
        {
            foreach (ITickable component in _addList)
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

            List<ITickable> remove = new List<ITickable>();
            foreach (ITickable component in _componentsList)
            {
                if (component != null)
                {
                    component.Tick(realDeltaTime);
                }
                else
                {
                    remove.Add(component);
                }
            }

            foreach (ITickable component in remove)
            {
                _componentsList.Remove(component);
            }
        }
    }
}
