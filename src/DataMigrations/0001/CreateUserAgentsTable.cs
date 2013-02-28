using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dolstagis.DataMigrations._0001
{
    [Migration(4)]
    public class CreateUserAgentsTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("UserAgents")
                .WithColumn("UserAgentID").AsInt32().Identity().PrimaryKey().NotNullable()
                .WithColumn("String").AsString(Int32.MaxValue).NotNullable();   // nvarchar(max)
        }
    }
}
