//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Beamable.Server.Clients
{
    using System;
    using Beamable.Platform.SDK;
    using Beamable.Server;
    
    
    /// <summary> A generated client for <see cref="Beamable.Microservices.MyMicroservice"/> </summary
    public sealed class MyMicroserviceClient : MicroserviceClient, Beamable.Common.IHaveServiceName
    {
        
        public MyMicroserviceClient(BeamContext context = null) : 
                base(context)
        {
        }
        
        public string ServiceName
        {
            get
            {
                return "MyMicroservice";
            }
        }
        
        /// <summary>
        /// Call the AddMyValues method on the MyMicroservice microservice
        /// <see cref="Beamable.Microservices.MyMicroservice.AddMyValues"/>
        /// </summary>
        public Beamable.Common.Promise<int> AddMyValues(int a, int b)
        {
            object raw_a = a;
            object raw_b = b;
            System.Collections.Generic.Dictionary<string, object> serializedFields = new System.Collections.Generic.Dictionary<string, object>();
            serializedFields.Add("a", raw_a);
            serializedFields.Add("b", raw_b);
            return this.Request<int>("MyMicroservice", "AddMyValues", serializedFields);
        }
    }
    
    internal sealed class MicroserviceParametersMyMicroserviceClient
    {
        
        [System.SerializableAttribute()]
        internal sealed class ParameterSystem_Int32 : MicroserviceClientDataWrapper<int>
        {
        }
    }
    
    [BeamContextSystemAttribute()]
    public static class ExtensionsForMyMicroserviceClient
    {
        
        [Beamable.Common.Dependencies.RegisterBeamableDependenciesAttribute()]
        public static void RegisterService(Beamable.Common.Dependencies.IDependencyBuilder builder)
        {
            builder.AddScoped<MyMicroserviceClient>();
        }
        
        public static MyMicroserviceClient MyMicroservice(this Beamable.Server.MicroserviceClients clients)
        {
            return clients.GetClient<MyMicroserviceClient>();
        }
    }
}
