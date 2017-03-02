using UnityEngine;
using System.Collections;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class White : Singleton<White>
    {
        //private SpriteRenderer myRenderer;
        private Shader _shaderGUItext;
        private Shader _shaderSpritesDefault;
        

        void Start()
        {
            //myRenderer = gameObject.GetComponent<SpriteRenderer>();
            _shaderGUItext = Shader.Find("GUI/Text Shader");
            Debug.Log(_shaderGUItext);
            _shaderSpritesDefault = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used
            
        }

        public void WhiteSprite(SpriteRenderer myRenderer)
        {
            myRenderer.material.shader = _shaderGUItext;
            Debug.Log(myRenderer.material.shader);
            myRenderer.color = Color.white;
        }

        public void NormalSprite(SpriteRenderer myRenderer)
        {
            myRenderer.material.shader = _shaderSpritesDefault;
            myRenderer.color = Color.white;
        }
    }
}


