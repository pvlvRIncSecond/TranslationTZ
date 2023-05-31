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
        private const string Path = "ws://185.246.65.199:9090/ws";
        
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPersistentProgress _progress;

        private bool _run = true;

        public Endpoint(ICoroutineRunner coroutineRunner, IPersistentProgress progress)
        {
            _coroutineRunner = coroutineRunner;
            _progress = progress;
        }

        public void Connect() =>
            _coroutineRunner.StartCoroutine(RunWs());

        public void Disconnect() =>
            _run = false;

        private IEnumerator RunWs()
        {
            using (WebSocket ws = new WebSocket(Path))
            {
                ws.EmitOnPing = true;
                ws.OnMessage += (sender, e) =>
                {
                    if (e.IsPing) Debug.Log($"Ping received");
                    if (!e.IsText) return;
                    Debug.Log($"Operation says: {e.Data}");
                    //if (e.IsText && e.Data.Contains("odometer"))
                        _progress.Odometer = JsonUtility.FromJson<OdometerData>(e.Data).odometer;
                };

                ws.OnOpen += (sender, e) =>
                {
                    _progress.ConnectedToServer = true;
                    Debug.Log("Connection established.");
                };
                
                ws.OnError+= (sender, e) =>
                    Debug.Log($"Connection error. Exception:{e.Exception}, Message {e.Message}.");
                
                ws.OnClose += (sender, e) =>
                {
                    _progress.ConnectedToServer = false;
                    Debug.Log($"Connection closed. Reason {e.Reason}, Code: {e.Code}.");
                };
                
                
                ws.Connect();

                while (CanRun())
                {
                    ws.Send("{operation: getCurrentOdometer}");
                    yield return new WaitForSeconds(15);
                }
                
                ws.Close();
            }
        }

        private bool CanRun() => 
            !Input.GetKeyDown(KeyCode.C) && _run;

        [Serializable]
        public class OdometerData
        {
            public string operation;
            public float odometer;
        }
    }
}