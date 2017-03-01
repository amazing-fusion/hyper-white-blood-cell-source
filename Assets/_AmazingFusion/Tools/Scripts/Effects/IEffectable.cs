using System;

namespace com.AmazingFusion {
    public interface IEffectable {

        void Play();

        event Action<IEffectable> OnEnd;
    }
}