using MySql.Data.EntityFramework;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data.Entity;
using System.Xml;

[DbConfigurationType(typeof(MySqlEFConfiguration))]
public class MyDbContext : DbContext
{
    //public DbSet<MyEntity> MyEntities { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<MyEntity>().ToTable("MyEntities");
        //modelBuilder.Entity<MyEntity>().HasKey(e => e.Id);
    }

    protected override DbConnection CreateConnection(string nameOrConnectionString)
    {
        var connection = new MySqlConnection(nameOrConnectionString);
        connection.Open();
        return connection;
    }
}