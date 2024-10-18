using System;
using UnityEngine;
using System.Runtime.InteropServices;
using AOT;

namespace UnityJSInteraction
{
    internal static class JSInteractionManager
    {
        // Event to notify subscribers when a message is received from JavaScript
        public static event Action OnJSEventReceived;

        // JavaScript method to call from Unity
        [DllImport("__Internal")]
        public static extern void CallJSMethod(string action, string message);

        // JavaScript method to set event listener
        [DllImport("__Internal")]
        public static extern void SetJSEventListener(JSEventCallback callback);

        // Delegate for receiving events from JavaScript
        public delegate void JSEventCallback();

        private static bool isInitialized = false;

        // Initialize the listener only once
        public static void Initialize()
        {
            if (!isInitialized)
            {
                Debug.Log("Initializing JSInteractionManager and setting event listener.");
                SetJSEventListener(OnJSEvent);
                isInitialized = true;
            }
        }

        // Callback for receiving events from JavaScript
        [MonoPInvokeCallback(typeof(JSEventCallback))]
        public static void OnJSEvent()
        {
            Debug.Log("Received message from JS");

            // Invoke the event to notify subscribers
            OnJSEventReceived?.Invoke();
        }

        // Method to trigger JavaScript method call
        public static void TriggerJSMethod(string action, string message)
        {
            if (!isInitialized)
            {
                Debug.Log("JSInteractionManager is not initialized, initializing now.");
                Initialize();
            }

            Debug.Log("Calling JavaScript method: " + action + " with message: " + message);
            CallJSMethod(action, message);
        }
    }
}