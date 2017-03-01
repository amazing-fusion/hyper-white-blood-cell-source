using System;

namespace com.PancakeTeam {
    public interface IEffectable {

        void Play();

        event Action<IEffectable> OnEnd;
    }
}