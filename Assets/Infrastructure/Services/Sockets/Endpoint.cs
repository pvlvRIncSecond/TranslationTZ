using System;
using System.Collections;
using Infrastructure.Scenes;
using Infrastructure.Services.Progress;
using UnityEngine;
using WebSocketSharp;

namespace Infrastructure.Services.Sockets
{
    public class Endpoint : IEndpoint
    {
        private const float Frequency = 15f;

        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPersistentProgress _progress;

        private WebSocket _ws;
        private Coroutine _connectionCoroutine;
        private Coroutine _reconnectCoroutine;


        public Endpoint(ICoroutineRunner coroutineRunner, IPersistentProgress progress)
        {
            _coroutineRunner = coroutineRunner;
            _progress = progress;

            _progress.OnServerChanged += Reconnect;
        }

        public void Connect() =>
            _connectionCoroutine = _coroutineRunner.StartCoroutine(RunWs());

        public void Disconnect()
        {
            if (_connectionCoroutine != null) _coroutineRunner.StopCoroutine(_connectionCoroutine);
            _ws?.Close();
        }

        public void Reconnect()
        {
            _ws?.Close();
            SetupWs();
        }

        private IEnumerator RunWs()
        {
            SetupWs();

            while (true)
            {
                try
                {
                    if (!_ws.IsAlive)
                    {
                        _ws.Connect();
                        if (_ws.IsAlive)
                        {
                            _ws.Send("{operation: getCurrentOdometer}");
                        }
                        else
                        {
                            Debug.Log($"Attempting to reconnect in {Frequency} s");
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.Log($"Failed to connect to {_progress.Endpoint()}");
                    Debug.Log(e.ToString());

                    _ws.Close();

                    SetupWs();
                }

                yield return new WaitForSeconds(Frequency);
            }
        }

        private void SetupWs()
        {
            _ws = new WebSocket(_progress.Endpoint());
            _ws.OnOpen += ws_OnOpen;
            _ws.OnMessage += ws_OnMessage;
            _ws.OnError += ws_OnError;
            _ws.OnClose += ws_OnClose;
        }

        private void ws_OnClose(object sender, CloseEventArgs e)
        {
            _progress.ConnectedToServer = false;
            Debug.Log($"Connection closed. Reason {e.Reason}, Code: {e.Code}.");
        }

        private void ws_OnError(object sender, ErrorEventArgs e) =>
            Debug.Log($"Connection error. Exception:{e.Exception}, Message {e.Message}.");

        private void ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsPing) Debug.Log($"Ping received");
            if (!e.IsText) return;

            Debug.Log($"Operation says: {e.Data}");

            if (e.Data.Contains("currentOdometer"))
                _progress.Odometer = JsonUtility.FromJson<OdometerData>(e.Data).odometer;
            if (e.Data.Contains("value"))
                _progress.Odometer = JsonUtility.FromJson<OdometerData>(e.Data).value;
        }

        private void ws_OnOpen(object sender, EventArgs e)
        {
            _progress.ConnectedToServer = true;
            Debug.Log("Connection established.");
        }


        private bool CanRun() =>
            !Input.GetKeyDown(KeyCode.C);

        [Serializable]
        public class OdometerData
        {
            public string operation;
            public float odometer;
            public float value;
        }
    }
}