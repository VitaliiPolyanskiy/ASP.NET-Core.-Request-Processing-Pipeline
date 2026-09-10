using RequestProcessingPipeline;

var builder = WebApplication.CreateBuilder(args);

// Усі сесії працюють поверх об'єкта IDistributedCache, і 
// ASP.NET Core надає вбудовану реалізацію IDistributedCache
builder.Services.AddDistributedMemoryCache(); // Додаємо IDistributedMemoryCache
builder.Services.AddSession();  // Додаємо сервіси сесії

var app = builder.Build();

app.UseSession();   // Додаємо middleware-компонент для роботи з сесіями

// Додаємо middleware-компоненти в конвеєр обробки запиту
app.UseFromTwentyToHundred(); // 20-100
app.UseFromElevenToNineteen(); // 11-19
app.UseFromOneToTen(); // 1-9

app.Run();