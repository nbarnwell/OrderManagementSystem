using Nancy;

namespace DddCqrsEsExample.Web.Modules
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = _ => View["Index"];
            Get["/NewOrder"] = _ => View["NewOrder"];
            Get["/Order/{id}"] = _ => View["NewOrder"];
        }
    }
}