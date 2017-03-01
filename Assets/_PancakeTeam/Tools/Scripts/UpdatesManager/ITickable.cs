using UnityEngine;
using System.Collections;

namespace com.PancakeTeam
{
    public interface ITickable
    {

        void Tick(float realDeltaTime);
    }

    public interface IFixedTickable
    {

        void FixedTick(float realDeltaTime);
    }

    public interface ILateTickable
    {

        void LateTick(float realDeltaTime);
    }

}
