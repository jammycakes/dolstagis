using System.Data;
using FluentMigrator;

namespace Dolstagis.DataMigrations._0001
{
    [Migration(4)]
    public class CreateUserSessionsTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("UserSessions")
                .WithColumn("SessionID").AsAnsiString(32).PrimaryKey().NotNullable()
                .WithColumn("UserID").AsInt64().NotNullable()
                    .ForeignKey("Users", "UserID").OnUpdate(Rule.Cascade).OnDelete(Rule.Cascade)
                .WithColumn("DateCreated").AsDateTime().NotNullable()
                .WithColumn("DateLastAccessed").AsDateTime().NotNullable()
                .WithColumn("IPAddress").AsAnsiString(50).NotNullable()
                .WithColumn("UserAgentID").AsInt32().NotNullable()
                    .ForeignKey("UserAgents", "UserAgentID").OnUpdate(Rule.None).OnDelete(Rule.None);
        }
    }
}
