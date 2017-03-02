using UnityEngine;
using System.Collections;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class White : Singleton<White>
    {
        private Shader _shaderGUItext;
        private Shader _shaderSpritesDefault;
        

        void Awake()
        {
            _shaderGUItext = Shader.Find("GUI/Text Shader");
            _shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
            
        }

        public void WhiteSprite(SpriteRenderer myRenderer)
        {
            myRenderer.material.shader = _shaderGUItext;
            myRenderer.color = Color.red;
        }

        public void NormalSprite(SpriteRenderer myRenderer)
        {
            myRenderer.material.shader = _shaderSpritesDefault;
            myRenderer.color = Color.white;
        }
    }
}


