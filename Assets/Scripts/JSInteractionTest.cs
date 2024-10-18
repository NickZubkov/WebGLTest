using UnityEngine;
using UnityJSInteraction;

public class JSInteractionTest : MonoBehaviour
{
    void OnEnable()
    {
        // Подписываемся на событие OnJSEventReceived
        JSInteractionManager.OnJSEventReceived += OnMessageFromJS;
    }

    void OnDisable()
    {
        // Отписываемся от события при отключении объекта
        JSInteractionManager.OnJSEventReceived -= OnMessageFromJS;
    }

    private void Start()
    {
        JSInteractionManager.Initialize();
    }

    public void Call()
    {
        // Тестовый вызов метода JavaScript с действием и сообщением
        Debug.Log("Calling JavaScript from Unity...");
        JSInteractionManager.TriggerJSMethod("testAction", "Hello from Unity!");
    }

    // Этот метод будет вызываться, когда придет сообщение из JavaScript
    private void OnMessageFromJS()
    {
        Debug.Log("Received message from JavaScript in Monobeh");
    }
}