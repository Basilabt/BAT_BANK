//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();




using BAT_BANK_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//  Secret key for JWT validation (must be the same as used for token generation)
var secretKey = "Hi_Im_Basel_AbuTaleb_This_is_a_training_project_for_my_training_at_****(^_^)"; ; 

//  Add authentication services for JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, //  Check signature
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), //  Use same key as in TokenService
            ValidateIssuer = false, //  Don't require a specific issuer (optional)
            ValidateAudience = false, //  Don't require a specific audience (optional)
            ValidateLifetime = true, //  Reject expired tokens
            ClockSkew = TimeSpan.Zero //  Prevent token expiration delays
        };
    });

//  Add Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI only in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //  Enable JWT Authentication
app.UseAuthorization();  //  Enable Authorization

app.MapControllers();

clsWindowsServices.startAPITrackingWindowsService();

app.Run();
