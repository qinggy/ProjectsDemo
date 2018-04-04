using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Demo
{
    public class MoveShapeHub : Hub
    {
        // Is set via the constructor on each creation
        private Broadcaster _broadcaster;
        public MoveShapeHub()
            : this(Broadcaster.Instance)
        {
        }
        public MoveShapeHub(Broadcaster broadcaster)
        {
            _broadcaster = broadcaster;
        }

        public void UpdateModel(ShapeModel clientModel)
        {
            clientModel.LastUpdateBy = Context.ConnectionId;
            //update the shape model whitin our broadcaster
            //update clientModel except LastUpdateBy Property
            //Clients.AllExcept(clientModel.LastUpdateBy).updateShape(clientModel);
            // Update the shape model within our broadcaster
            _broadcaster.UpdateShape(clientModel);
        }

        public class ShapeModel
        {
            // We declare Left and Top as lowercase with 
            // JsonProperty to sync the client and server models
            [JsonProperty("left")]
            public double Left { get; set; }
            [JsonProperty("top")]
            public double Top { get; set; }
            // We don't want the client to get the "LastUpdatedBy" property
            [JsonIgnore]
            public string LastUpdateBy { get; set; }
        }
    }
}