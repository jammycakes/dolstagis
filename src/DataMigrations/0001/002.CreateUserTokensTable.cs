using System.Data;
using FluentMigrator;

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
                .WithColumn("Expires").AsDateTime().WithDefault(SystemMethods.CurrentUTCDateTime);
        }
    }
}
