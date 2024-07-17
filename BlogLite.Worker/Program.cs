using BlogLite.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<QueueReader>();

var host = builder.Build();
host.Run();
