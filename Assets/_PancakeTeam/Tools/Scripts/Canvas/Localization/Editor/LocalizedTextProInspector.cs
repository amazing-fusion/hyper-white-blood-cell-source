
namespace SmartLocalization.Editor{
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(LocalizedTextPro))]
public class LocalizedTextProInspector : Editor 
{
	private string selectedKey = null;
	
	void Awake()
	{
        LocalizedTextPro textObject = ((LocalizedTextPro)target);
		if(textObject != null)
		{
			selectedKey = textObject.localizedKey;
		}
	}
	
	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		
		selectedKey = LocalizedKeySelector.SelectKeyGUI(selectedKey, true, LocalizedObjectType.STRING);
		
		if(!Application.isPlaying && GUILayout.Button("Use Key", GUILayout.Width(70)))
		{
			LocalizedTextPro textObject = ((LocalizedTextPro)target);
			textObject.localizedKey = selectedKey;
		}
	}
	
}
}