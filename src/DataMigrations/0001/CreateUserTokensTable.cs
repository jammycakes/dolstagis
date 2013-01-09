using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dolstagis.DataMigrations._0001
{
    [Migration(2)]
    public class CreateUserTokensTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("UserTokens")
                .WithColumn("Token").AsGuid().NotNullable().PrimaryKey().WithDefault(SystemMethods.NewGuid)
                .WithColumn("UserID").AsInt64().NotNullable()
                    .ForeignKey("Users", "UserID").OnUpdate(Rule.Cascade).OnDelete(Rule.Cascade)
                .WithColumn("Action").AsString(32).NotNullable()
                .WithColumn("DateCreated").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime);
        }
    }
}
