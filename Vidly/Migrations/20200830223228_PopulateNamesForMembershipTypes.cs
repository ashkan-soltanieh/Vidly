using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class PopulateNamesForMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MembershipTypes Set Name='Pay as You Go' WHERE Id=1");
            migrationBuilder.Sql("UPDATE MembershipTypes Set Name='Monthly' WHERE Id=2");
            migrationBuilder.Sql("UPDATE MembershipTypes Set Name='Quarterly' WHERE Id=3");
            migrationBuilder.Sql("UPDATE MembershipTypes Set Name='Annually' WHERE Id=4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
