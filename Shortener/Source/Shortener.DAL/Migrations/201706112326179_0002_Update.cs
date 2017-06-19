namespace TestTask.Shortener.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0002_Update : DbMigration
    {
        private const string IndexName = "IX_Links_Original";

        public override void Up()
        {
            AlterColumn("dbo.UserLinks", "CreateDate", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.UserLinks", "ClickCount", c => c.Int(nullable: false, defaultValueSql: "0"));
            AlterColumn("dbo.Links", "ClickCount", c => c.Int(nullable: false, defaultValueSql: "0"));

            Sql(String.Format(@"CREATE NONCLUSTERED INDEX [{0}]
                               ON [dbo].[Links] ([Id])
                               INCLUDE ([OriginalLink])", IndexName));

            Sql(String.Format(@"CREATE SEQUENCE SQNLink  
                                START WITH 1
                                INCREMENT BY 1"));
        }

        public override void Down()
        {
            DropIndex("dbo.Links", IndexName);
        }
    }
}
