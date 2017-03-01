using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class DamageController : OptimizedBehaviour {

        public event System.Action OnLifesChange;
        public event System.Action OnDieEnd;

    }
}