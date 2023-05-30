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
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPersistentProgress _progress;
        public const string Path = "ws://185.246.65.199:9090/ws";

        public Endpoint(ICoroutineRunner coroutineRunner, IPersistentProgress progress)
        {
            _coroutineRunner = coroutineRunner;
            _progress = progress;
        }

        public void Connect() =>
            _coroutineRunner.StartCoroutine(RunWs());


        private IEnumerator RunWs()
        {
            using (var ws = new WebSocket(Path))
            {
                ws.EmitOnPing = true;
                ws.OnMessage += (sender, e) =>
                {
                    if (!e.IsText) return;
                    Debug.Log($"Operation says: {e.Data}");
                    //if (e.IsText && e.Data.Contains("odometer"))
                        _progress.Odometer = JsonUtility.FromJson<OdometerData>(e.Data).value;
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
                
                while (!Input.GetKeyDown(KeyCode.C))
                    yield return null;
                
            }
        }
        
        [Serializable]
        public class OdometerData
        {
            public string operation;
            public float value;
        }
    }
}