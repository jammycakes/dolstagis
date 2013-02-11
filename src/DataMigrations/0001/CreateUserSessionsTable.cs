using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Dolstagis.DataMigrations._0001
{
    [Migration(3)]
    public class CreateUserSessionsTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("UserSessions")
                .WithColumn("SessionID").AsAnsiString(32).PrimaryKey().NotNullable()
                .WithColumn("UserID").AsInt64().NotNullable()
                    .ForeignKey("Users", "UserID").OnUpdate(Rule.Cascade).OnDelete(Rule.Cascade)
                .WithColumn("DateCreated").AsDateTime().NotNullable()
                .WithColumn("DateLastAccessed").AsDateTime().NotNullable();
        }
    }
}
