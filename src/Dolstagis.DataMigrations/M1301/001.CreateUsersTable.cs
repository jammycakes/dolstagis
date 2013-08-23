using FluentMigrator;

namespace Dolstagis.DataMigrations.M1301
{
    [Migration(1)]
    public class CreateUsersTable : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("UserID").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("UserName").AsString(20).NotNullable().WithDefaultValue("")
                .WithColumn("EmailAddress").AsString(250).NotNullable().WithDefaultValue("")
                .WithColumn("PasswordHash").AsString(100).NotNullable().WithDefaultValue("")
                .WithColumn("DisplayName").AsString(100).NotNullable().WithDefaultValue("")
                .WithColumn("IsSuperUser").AsBoolean().NotNullable().WithDefaultValue(false);

            Insert.IntoTable("Users")
                .Row(new {
                    UserName = "admin",
                    EmailAddress = "code@jamesmckay.net",
                    PasswordHash = "",
                    DisplayName = "Site admin",
                    IsSuperUser = true
                });
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
