using Beamable;
using Beamable.Server;
using Beamable.Microservices;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScene : MonoBehaviour
{
    private MicroserviceClient _MyMicroserviceClient;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(message: "Start()");
        SetUpBeamable();
    }

    private async void SetUpBeamable()
    {
        var context = BeamContext.Default;
        await context.OnReady;

        _MyMicroserviceClient = context.Microservices().MyMicroservice();
        var result = await _MyMicroserviceClient.AddMyValues(a: 10, b: 5);

    }

    //private int AddMyValues(int a, int b)
    //{
    //        return a + b;
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
