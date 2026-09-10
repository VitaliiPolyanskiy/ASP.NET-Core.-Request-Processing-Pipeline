namespace RequestProcessingPipeline;

public class FromTwentyToHundredMiddleware
{
    private readonly RequestDelegate _next;

    public FromTwentyToHundredMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? token = context.Request.Query["number"]; // Отримуємо число з контексту запиту

        if (!int.TryParse(token, out int number))
        {
            // Видаємо остаточну відповідь клієнту
            await context.Response.WriteAsync("Incorrect parameter");
            return;
        }

        number = Math.Abs(number);

        if (number < 20)
        {
            // Передаємо контекст запиту наступному компоненту
            await _next.Invoke(context);
        }
        else if (number > 100)
        {
            // Видаємо остаточну відповідь клієнту
            await context.Response.WriteAsync("Number greater than one hundred");
        }
        else if (number == 100)
        {
            // Видаємо остаточну відповідь клієнту
            await context.Response.WriteAsync("Your number is one hundred");
        }
        else
        {
            string[] tens = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number % 10 == 0)
            {
                // Видаємо остаточну відповідь клієнту
                await context.Response.WriteAsync($"Your number is {tens[number / 10 - 2]}");
            }
            else
            {
                // Передаємо контекст запиту наступному компоненту
                await _next.Invoke(context);

                // Отримуємо число від компонента FromOneToTenMiddleware
                string? result = context.Session.GetString("number");

                // Видаємо остаточну відповідь клієнту
                await context.Response.WriteAsync($"Your number is {tens[number / 10 - 2]} {result}");
            }
        }
    }
}