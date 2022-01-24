using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class CustomTraceHolder
{
    public Guid TraceId { get; set; } = Guid.NewGuid();
}

public class TraceIdMiddleware
{
    private readonly RequestDelegate _next;

    public TraceIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, CustomTraceHolder traceHolder)
    {        
        if (context.Request.Headers.ContainsKey("traceid")) 
        {
            Guid temp;
            if (Guid.TryParse(context.Request.Headers["traceid"].First(), out temp)) 
            {
                traceHolder.TraceId = temp;
            }
        }

        Console.WriteLine($"TraceId set to {traceHolder.TraceId}");
        
        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}