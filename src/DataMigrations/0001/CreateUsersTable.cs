using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentMigrator;
using System.Data;

namespace Dolstagis.DataMigrations._0001
{
    [Migration(1)]
    public class CreateUsersTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("UserID").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserName").AsString(20).NotNullable().WithDefaultValue("")
                .WithColumn("EmailAddress").AsString(250).NotNullable().WithDefaultValue("")
                .WithColumn("PasswordHash").AsString(100).NotNullable().WithDefaultValue("")
                .WithColumn("DisplayName").AsString(100).NotNullable().WithDefaultValue("");
        }
    }
}
