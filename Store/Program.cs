using WebApplicationL5.Data.EF;
using WebApplicationL5.ModelMappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IOrderModelMapper, OrderModelMapper>();
builder.Services.AddScoped<ICustomerModelMapper, CustomerModelMapper>();

builder.Services.AddDbContext<EFDataContext>();

//.AddRazorRuntimeCompilation()
//.AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamePolicy();
//    options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString |
//                                                   JsonNumberHandling.AllowNamedFloatingPointLiterals;
//    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//    options.JsonSerializerOptions.Converters.Add(new DoubleConverter());
//});

//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{productId?}");

app.Run();