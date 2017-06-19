namespace TestTask.Shortener.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ShortenerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TestTask.Shortener.DAL.ShortenerContext";
        }

        protected override void Seed(ShortenerContext context)
        {
        }
    }
}
