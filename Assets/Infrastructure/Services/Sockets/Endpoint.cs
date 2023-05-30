using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Scenes;
using UnityEngine;
using WebSocketSharp;

namespace Infrastructure.Services.Sockets
{
    public class Endpoint : IEndpoint
    {
        private readonly ICoroutineRunner _coroutineRunner;
        public const string UriPath = "ws://185.246.65.199:9090/ws";

        public Endpoint(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;

        public void Try() =>
            _coroutineRunner.StartCoroutine(Test());


        private IEnumerator Test()
        {
            using (var ws = new WebSocket(UriPath))
            {
                ws.EmitOnPing = true;
                ws.OnMessage += (sender, e) =>
                {
                    var body = !e.IsPing ? e.Data : "A ping was received.";
                    Debug.Log("Operation says: " + body);
                };

                ws.OnOpen += (sender, e) =>
                {
                    ws.Send("{operation : getCurrentOdometer}");
                    //ws.Send("{operation: getRandomStatus}");
                    Debug.Log("Connection established");
                };
                
                ws.OnError+= (sender, e) =>
                    Debug.Log("Connection error" + e.Exception + e.Message);
                
                ws.OnClose += (sender, e) =>
                    Debug.Log("Connection closed" + e.Reason + e.Code);
                
                
                ws.Connect();
                
                while (!Input.GetKeyDown(KeyCode.C))
                    yield return null;
                
            }
        }
        
        [Serializable]
        public class PingData
        {
            public string operation;
            public float value;
        }
    }
}