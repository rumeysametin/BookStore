using BookStore.Business;
using BookStore.Data;
using BookStore.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using BookStore.Data.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<BookLogic>();
builder.Services.AddHttpClient<BookInfoService>(); // BookInfoService için HttpClient ekle
builder.Services.AddScoped<BarcodeService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>    //Kimlik doðrulama þemasý, bu þemaya oluþan tokený ekleyince apileri daha kolay test edilir.
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title ="Kütüphane docs" , Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Token",
        Type =Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme ="Bearer",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
             {
                 {
                       new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             }
                         },
                         new string[] {}

                 }
             });
});
builder.Services.AddDbContext<BookStoreContext>(options =>
options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Database=bookdb;password=1234; Trust Server Certificate=true",
o => o.SetPostgresVersion(new Version(11, 15))));

//builder.Services.AddCors(policyBuilder =>
//    policyBuilder.AddDefaultPolicy(policy =>
//        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
//);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<BookStoreContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "yourAudience",
        ValidIssuer = "yourIssuer",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSuperSecretKeyMustBeAtLeast86CharactersLong"))
    };
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();  // sýralamasý önemli

app.UseRouting();

//app.UseCors(builder => builder
//.AllowAnyHeader()
//.AllowAnyMethod()
//.SetIsOriginAllowed((host) => true)
//.AllowCredentials());

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
