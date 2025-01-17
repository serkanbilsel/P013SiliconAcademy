using Microsoft.AspNetCore.Authentication.Cookies; // Oturum i�lemleri i�in bu k�t�phaneleri elle ekledik.

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>// oturum i�lemleri i�in
{
    x.LoginPath = "/Admin/Login"; // oturum a�mayan kullan�c�lar�n giri� i�in g�nderelice�i adres
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccesDenied"; // yetkilendirme ile ekrana eri�im hakk� olmayan kullan�c�lar�n g�nderilece�i sayfa
    x.Cookie.Name = "Administrator"; //  Olu�acak cookie ismi
    x.Cookie.MaxAge = TimeSpan.FromDays(1); // Olu�acak cooki'nin ya�am s�resi 1 g�n.
});
//Uygulama admin paneli i�in admin yetkilendirme aarlar�

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin")); // admin paneline giri� yapma yetkisine sahip olanlar� bu  kuralla kontrol edece�iz.
    x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User")); // admin d���nda yetkilendirme kullan��rsak bu kural� kullanabiliriz(siteye �ye giri�i yapanlar� �n y�zde bir paneldepanele eri�tirme gibi)
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication(); // D�kkat! �nce UseAuthentication sat�r� gelmeli sonra Use Authorization

app.UseAuthorization();
app.MapControllerRoute(
          name: "admin",
          pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
        );


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
