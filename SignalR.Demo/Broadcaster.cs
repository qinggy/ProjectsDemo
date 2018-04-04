using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace SignalR.Demo
{
    public class Broadcaster
    {
        private readonly static Lazy<Broadcaster> _instance = new Lazy<Broadcaster>(() => new Broadcaster());
        // We're going to broadcast to all clients a maximum of 25 times per second
        private readonly TimeSpan BroadcastInterval = TimeSpan.FromMilliseconds(40);
        private readonly IHubContext _hubContext;
        private Timer _broadcastLoop;
        private SignalR.Demo.MoveShapeHub.ShapeModel _model;
        private bool _modelUpdated;

        public Broadcaster()
        {
            // Save our hub context so we can easily use it 
            // to send to its connected clients
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<MoveShapeHub>();
            _model = new SignalR.Demo.MoveShapeHub.ShapeModel();
            _modelUpdated = false;
            // Start the broadcast loop
            _broadcastLoop = new Timer(BroadcastShape, null, BroadcastInterval, BroadcastInterval);
        }

        public void BroadcastShape(object state)
        {
            // No need to send anything if our model hasn't changed
            if (_modelUpdated)
            {
                // This is how we can access the Clients property 
                // in a static hub method or outside of the hub entirely
                _hubContext.Clients.AllExcept(_model.LastUpdateBy).updateShape(_model);
                _modelUpdated = false;
            }
        }

        public void UpdateShape(SignalR.Demo.MoveShapeHub.ShapeModel clientModel)
        {
            _model = clientModel;
            _modelUpdated = true;
        }

        public static Broadcaster Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}