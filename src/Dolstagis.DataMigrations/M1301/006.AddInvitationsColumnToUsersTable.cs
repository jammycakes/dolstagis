using FluentMigrator;

namespace Dolstagis.DataMigrations.M1301
{
    [Migration(6)]
    public class AddInvitationsColumnToUsersTable : AutoReversingMigration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("Invitations").AsInt32().NotNullable().WithDefaultValue(0);
        }
    }
}
