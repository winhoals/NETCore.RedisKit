# NETCore.RedisKit
[StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis) extensions. Easy use redis in your asp.net core project.

# Install with nuget

```
Install-Package NETCore.RedisKit
```

# How to use

## Add RedisKit in startup like 

```csharp
public void ConfigureServices(IServiceCollection services)
{
  // Add framework services.
  services.AddMvc();

  services.AddRedisKit(optionsBuilder =>
  {
      optionsBuilder.UseRedis(
        options: new RedisKitOptions()
        {
            EndPoints = "127.0.0.1:6379"
        },
        isShowLog: true);
  });
}

```

## Use RedisService like 

```csharp
private readonly IRedisService _RedisService;
public HomeController(IRedisService redisService)
{
    _RedisService = redisService;
}

public IActionResult Index()
{
    _RedisService.StringSet("hello", "world");
    return View();
}

```

# LICENSE

MIT
