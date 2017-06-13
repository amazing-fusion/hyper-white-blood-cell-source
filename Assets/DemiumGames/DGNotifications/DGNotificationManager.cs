using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


namespace DemiumGames{

	public class DGNotificationManager : MonoBehaviour {

        [System.Serializable]
        public class DGNotificationCallback : UnityEngine.Events.UnityEvent<int> { };

		
	    private AndroidJavaClass intermediateClass;
	    private AndroidJavaClass notificationManager; 
	    private static DGNotificationManager instance;

	    private Dictionary<int, DGNotificationCallback> callbackDictionary; 
	   

	    void Awake()
	    {
	        if (instance == null)
	        {
	            if (callbackDictionary == null)
	                callbackDictionary = new Dictionary<int, DGNotificationCallback>(); 
	            intermediateClass = new AndroidJavaClass("com.demiumgames.notificationmodule.DGIntermediateActivity");
	            notificationManager = new AndroidJavaClass("com.demiumgames.notificationmodule.NotificationFragment");
	            instance = this; 
	        }
	    }

	    void Start()
	    {
	        int number = intermediateClass.GetStatic<int>("number");
	        if (number != -1)
	        {
	            if (callbackDictionary.ContainsKey(number))
	                callbackDictionary[number].Invoke(number);
	        }
	    }

	    public void SetCallbackInt(int id, DGNotificationCallback callBack)
	    {
	        if (callbackDictionary == null)
	            callbackDictionary = new Dictionary<int, DGNotificationCallback>(); 
	        callbackDictionary.Add(id, callBack); 
	    }

	    public void SendNotification(int id, string title, string text, string smallIcon, string largeIcon, int seconds)
	    {

	        this.notificationManager.CallStatic("SendNotification", id, title, text, smallIcon, largeIcon, seconds); 

	    }

	    public void SendNotificationWithAppIcon(int id, string title, string text, string smallIcon, int seconds)
	    {
	        SendNotification(id, title, text, smallIcon, "app_icon", seconds); 
	    }

	    


	    public void CancelNotification(int id, string title, string text, string smallIcon, string largeIcon)
	    {
	        this.notificationManager.CallStatic("CancelNotification", id, title, text, smallIcon, largeIcon);
	        this.notificationManager.CallStatic("CancelNotification", id, title, text, smallIcon, "app_icon");

	    }

		public void CancelNotification(int id){
			this.notificationManager.CallStatic ("CancelNotification", id); 
		}

		public void CancelAllNotifications(){
			this.notificationManager.CallStatic ("CancelAllNotifications"); 
		}



	    void OnApplicationPause(bool pauseStatus)
	    {
	        if (!pauseStatus)
	        {
	            int number = intermediateClass.GetStatic<int>("number");

	            if (number != -1)
	            {
	                if (callbackDictionary.ContainsKey(number))
                        callbackDictionary[number].Invoke(number);
                }
	        }

	    }


	   

	    public static DGNotificationManager Instance
	    {
	        get
	        {
	            return instance;
	        }

	    }
	}
}
