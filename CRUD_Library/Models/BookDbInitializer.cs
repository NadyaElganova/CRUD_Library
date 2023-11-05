namespace CRUD_Library.Models
{
    public static class BookDbInitializer
    {
        static public void seed(IApplicationBuilder app)
        {
            var result = app.ApplicationServices.CreateScope();
            var context = result.ServiceProvider.GetRequiredService<AppDbContext>();
            if (!context.Categories.Any())
            {
                context.Categories.Add(new Category { Name = "Historical novel"});    
                context.Categories.Add(new Category { Name = "Non-fiction" });
                context.SaveChanges();
            }
            if (!context.Readers.Any())
            {
                context.Readers.Add(new Reader { FIO = "Иванов И.П." });
                context.Readers.Add(new Reader { FIO = "Петров А.С." });
                context.SaveChanges();
            }

        }
    }
}
