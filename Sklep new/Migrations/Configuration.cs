namespace Sklep_new.Migrations
{
    using Sklep_new.DAL;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Sklep_new.DAL.KursyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "StronaSklep.DAL.KursyContext";
        }

        protected override void Seed(Sklep_new.DAL.KursyContext context)
        {
            KursyInitializer.SeedKursyData(context);
            KursyInitializer.SeedUzytkownicy(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
