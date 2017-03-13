using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash
{
    public class FirebaseAnalyticsManager : GlobalSingleton<FirebaseAnalyticsManager>
    {

        // Use this for initialization
        void Start()
        {
            FirebaseAnalytics.Init();
        }
    }
}
