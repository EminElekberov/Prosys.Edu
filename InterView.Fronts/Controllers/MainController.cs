namespace InterView.Front.Controller
{
    public class MainController : Microsoft.AspNetCore.Mvc.Controller
    {
        protected readonly InterView.RemoteServer.RemoteServer _remoteServer;

        public MainController(InterView.RemoteServer.RemoteServer remoteServer)
        {
            _remoteServer = remoteServer;
        }
    }
}
