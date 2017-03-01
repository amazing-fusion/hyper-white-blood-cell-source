namespace SmartLocalization.Editor
{
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent (typeof (TMP_Text))]
public class LocalizedTextPro : MonoBehaviour 
{
	public string localizedKey = "INSERT_KEY_HERE";
        TMP_Text textObject;
	
	void Start () 
	{
		textObject = this.GetComponent<TMP_Text>();
	
		//Subscribe to the change language event
		LanguageManager languageManager = LanguageManager.Instance;
		languageManager.OnChangeLanguage += OnChangeLanguage;
		
		//Run the method one first time
		OnChangeLanguage(languageManager);
	}
	
	void OnDestroy()
	{
		if(LanguageManager.HasInstance)
		{
			LanguageManager.Instance.OnChangeLanguage -= OnChangeLanguage;
		}
	}
	
	void OnChangeLanguage(LanguageManager languageManager)
	{
		textObject.text = LanguageManager.Instance.GetTextValue(localizedKey);
	}
}
}