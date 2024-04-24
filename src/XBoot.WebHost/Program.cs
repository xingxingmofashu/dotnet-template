using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net;
using XBoot.Composables;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<XBootOptions>(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("MSSQL");
});

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        //options.Filters.Add<IExceptionFilter>();
    })
     .AddJsonOptions(options =>
     {
         options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
     });

builder.Services.AddHttpContextAccessor();

var groups = new List<(string route, string group, List<string> files)>
{
    ("Playground", "Playground-API", new()),
};

if (builder.Environment.IsEnvironment("Local") || builder.Environment.IsEnvironment("Development"))
{
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(swgger =>
    {
        foreach (var item in groups)
        {
            swgger.SwaggerDoc(item.Item1, new OpenApiInfo
            {
                Version = item.Item1,
                Title = item.Item2,
                Description = $"be based on {item.Item1} API document description",
                Contact = new OpenApiContact
                {
                    Name = $"xingxingmofashu",
                    Email = $"xingxingmofashu@outlook.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                }
            });
            foreach (var xmlItem in item.Item3)
            {
                swgger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlItem), true);
            }
        }
        //Add security definition
        swgger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "please input token,formatter Bearer xxxxxxxx (Please note that there must be a space in the middle)",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        //Add security requirements
        swgger.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme{
                    Reference =new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id ="Bearer"
                    }
                },new string[]{ }
            }
        });
    });
}

builder.Services.AddSqlSugar();

// HTTPS enables response compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

var app = builder.Build();

app.UseResponseCompression();
app.UseForwardedHeaders();

if (app.Environment.IsEnvironment("Local") || builder.Environment.IsEnvironment("Development"))
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var (route, group, files) in groups)
        {
            c.SwaggerEndpoint($"/swagger/{route}/swagger.json", group);
        }
        c.DocExpansion(DocExpansion.None);
    });
}



app.UseAuthentication();
app.UseFileServer();


app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Server", Dns.GetHostName());

    await next();

    if (context.Response.StatusCode != 404)
    {
        return;
    }

    if (context.Request.Method != HttpMethod.Get.Method)
    {
        return;
    }

    var responseFile = async (string filename) =>
    {
        context.Response.ContentType = "text/html";
        context.Response.StatusCode = 200;
        await context.Response.SendFileAsync(app.Environment.WebRootPath + filename);
    };
    await responseFile("/index.html");
    
});

var allowOrigions = builder.Configuration.GetSection("AllowOrigins").Value?.Split(',');
if (allowOrigions != null && allowOrigions.Length > 0)
{
    app.UseCors(o => o.WithOrigins(allowOrigions).AllowAnyMethod().AllowAnyHeader().AllowCredentials());
}

app.MapControllers();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
