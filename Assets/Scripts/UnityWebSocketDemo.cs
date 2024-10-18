using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityWebSocket.Demo
{
    public class UnityWebSocketDemo : MonoBehaviour
    {
        [SerializeField] private TMP_Text _headerText;
        [SerializeField] private TMP_Text _webSocketStateText;
        [SerializeField] private TMP_Text _addressText;
        [SerializeField] private Button _connectButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _sendText;
        [SerializeField] private Button _sendStringButton;
        [SerializeField] private Button _sendBytesButton;
        [SerializeField] private TMP_Text _sendCountText;
        [SerializeField] private TMP_Text _receiveCountText;
        [SerializeField] private TMP_Text _logText;
        [SerializeField] private Button _clearButton;
        [SerializeField] private Scrollbar _scrollbar;
        [SerializeField] private string _address = "wss://echo.websocket.org";
        [SerializeField] private string _sendString = "Hello UnityWebSocket!";


        private bool _logMessage = true;
        private string _log = "";
        private int _sendCount;
        private int _receiveCount;
        private Color _green = new(0.1f, 1, 0.1f);
        private Color _red = new(1f, 0.1f, 0.1f);
        private Color _wait = new(0.7f, 0.3f, 0.3f);

        private Color _currentColor;

        private void OnEnable()
        {
            
            _headerText.text = "SDK Version: ";
            _addressText.text = _address;
            _sendText.text = _sendString;

            _connectButton.onClick.AddListener(ConnectToSocket);
            _closeButton.onClick.AddListener(CloseSocket);
            
            _sendStringButton.onClick.AddListener(SendString);
            _sendBytesButton.onClick.AddListener(SendBytes);
            
            _clearButton.onClick.AddListener(ClearLog);
        }

        private void OnDisable()
        {
            _connectButton.onClick.RemoveListener(ConnectToSocket);
            _closeButton.onClick.RemoveListener(CloseSocket);
            
            _sendStringButton.onClick.RemoveListener(SendString);
            _sendBytesButton.onClick.RemoveListener(SendBytes);
            
            _clearButton.onClick.RemoveListener(ClearLog);
        }

        private void Update()
        {
            SetWebSocketText();
            UpdateButtonsState();
            UpdateLog();
        }

        private async void ConnectToSocket()
        {
            
        }

        private void CloseSocket()
        {
            AddLog("Closing...");
        }

        private void SetWebSocketText()
        {
            _webSocketStateText.color = _currentColor;
        }

        private void UpdateButtonsState()
        {
           
        }

        private void UpdateLog()
        {
            _logText.text = _log;
            _sendCountText.text = "Send Count: " + _sendCount;
            _receiveCountText.text = "Receive Count: " + _receiveCount;
        }

        private void ClearLog()
        {
            _log = "";
            _receiveCount = 0;
            _sendCount = 0;
        }

        private void SendString()
        {
            AddLog(string.Format("Send: {0}", _sendString));
            _sendCount += 1;
        }

        private void SendBytes()
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(_sendString);
            AddLog(string.Format("Send Bytes ({1}): {0}", _sendString, bytes.Length));
            _sendCount += 1;
        }

        private void AddLog(string str)
        {
            if (!_logMessage) return;
            if (str.Length > 100) str = str.Substring(0, 100) + "...";
            _log += str + "\n";
            if (_log.Length > 22 * 1024)
            {
                _log = _log.Substring(_log.Length - 22 * 1024);
            }

            _scrollbar.value = 1;
        }

        private void Socket_OnOpen(object sender)
        {
            AddLog($"Connected: {_address}");
        }

        private void Socket_OnMessage(object sender)
        {
            
            _receiveCount += 1;
        }

        private void OnApplicationQuit()
        {
            
        }
    }
}
