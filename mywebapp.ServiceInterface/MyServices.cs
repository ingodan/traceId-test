using ServiceStack;
using mywebapp.ServiceModel;
using System.Threading.Tasks;

namespace mywebapp.ServiceInterface;

public class MyServices : Service
{
    public async Task<object> Any(Hello request)
    {
        var traceHolder = base.Request.TryResolveScoped<CustomTraceHolder>();
        System.Console.WriteLine($"Servicestack gets request here, and can use DI to get traceId: {traceHolder.TraceId}");
        var task = Task.Factory.StartNew<Hello>(() => 
            new Hello { Name = $"Hello {request.Name}" }
        );

        return await task;
        //return new Hello {Name = "hi"};
        //return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}