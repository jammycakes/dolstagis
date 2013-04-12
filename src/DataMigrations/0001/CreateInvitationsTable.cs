using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentMigrator;
using System.Data;

namespace Dolstagis.DataMigrations._0001
{
    [Migration(5)]
    public class CreateInvitationsTable : Migration
    {
        public override void Up()
        {
            Create.Table("Invitations")
                .WithColumn("InvitationID").AsString(32).NotNullable().PrimaryKey().WithDefaultValue("")
                .WithColumn("InvitingUserID").AsInt64().Nullable()
                    .ForeignKey("Users", "UserID").OnUpdate(Rule.None).OnDelete(Rule.None)
                .WithColumn("InviteeName").AsString(100).NotNullable().WithDefaultValue("")
                .WithColumn("InviteeEmail").AsString(250).NotNullable().WithDefaultValue("")
                .WithColumn("InviteeID").AsInt64().Nullable()
                    .ForeignKey("Users", "UserID").OnUpdate(Rule.None).OnDelete(Rule.None)
                .WithColumn("DateCreated").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentUTCDateTime)
                .WithColumn("DateSent").AsDateTime().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
