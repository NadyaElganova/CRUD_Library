namespace CRUD_Library.Meddleware
{
    public class KeyMiddleware
    {
        private RequestDelegate next;
        public KeyMiddleware(RequestDelegate next) 
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = context.Request.Query["key"];
            if(true)
            {
                await next.Invoke(context);
            }
            else
            {
                await context.Response.WriteAsync("goodbye");
            }
        }


    }
}
