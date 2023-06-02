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

        private WebSocket _ws;

        public Endpoint(ICoroutineRunner coroutineRunner, IPersistentProgress progress)
        {
            _coroutineRunner = coroutineRunner;
            _progress = progress;
        }

        public void Connect() =>
            _coroutineRunner.StartCoroutine(RunWs());

        public void Disconnect() =>
            _ws.Close();

        private IEnumerator RunWs()
        {
            _ws = new WebSocket(_progress.Endpoint());
            _ws.EmitOnPing = true;
            _ws.OnMessage += (sender, e) =>
            {
                if (e.IsPing) Debug.Log($"Ping received");
                if (!e.IsText) return;
                Debug.Log($"Operation says: {e.Data}");
                //if (e.IsText && e.Data.Contains("odometer"))
                _progress.Odometer = JsonUtility.FromJson<OdometerData>(e.Data).odometer;
            };

            _ws.OnOpen += (sender, e) =>
            {
                _progress.ConnectedToServer = true;
                Debug.Log("Connection established.");
            };
                
            _ws.OnError+= (sender, e) =>
                Debug.Log($"Connection error. Exception:{e.Exception}, Message {e.Message}.");
                
            _ws.OnClose += (sender, e) =>
            {
                _progress.ConnectedToServer = false;
                Debug.Log($"Connection closed. Reason {e.Reason}, Code: {e.Code}.");
            };
                
                
            _ws.Connect();

            while (CanRun())
            {
                _ws.Send("{operation: getCurrentOdometer}");
                yield return new WaitForSeconds(15);
            }
                
            _ws.Close();
        }

        private bool CanRun() => 
            !Input.GetKeyDown(KeyCode.C);

        [Serializable]
        public class OdometerData
        {
            public string operation;
            public float odometer;
        }
    }
}