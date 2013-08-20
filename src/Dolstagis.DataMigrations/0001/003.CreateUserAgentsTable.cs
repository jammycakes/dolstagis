using System;
using FluentMigrator;

namespace Dolstagis.DataMigrations._0001
{
    [Migration(3)]
    public class CreateUserAgentsTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("UserAgents")
                .WithColumn("UserAgentID").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("String").AsString(Int32.MaxValue).NotNullable();  // nvarchar(max)
        }
    }
}
