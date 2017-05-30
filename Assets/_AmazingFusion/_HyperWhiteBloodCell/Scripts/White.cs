using UnityEngine;
using System.Collections;

namespace com.AmazingFusion.HyperWhiteBloodCellDash
{
    public class White : Singleton<White>
    {
        private Shader _shaderGUItext;
        private Shader _shaderSpritesDefault;
        

        void Start()
        {
            _shaderGUItext = Shader.Find("GUI/Text Shader");
            _shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
            
        }

        public void WhiteSprite(SpriteRenderer myRenderer, Color color)
        {
            myRenderer.material.shader = _shaderGUItext;
            myRenderer.color = color;
        }

        public void NormalSprite(SpriteRenderer myRenderer)
        {
            myRenderer.material.shader = _shaderSpritesDefault;
            myRenderer.color = Color.white;
        }
    }
}


