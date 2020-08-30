using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class PopulateCustomerAndMovie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Customers (Name, IsSubscribedToNewsLetter, MembershipTypeId) VALUES ('John Roberts', 0, 2)");
            migrationBuilder.Sql("INSERT INTO Customers (Name, IsSubscribedToNewsLetter, MembershipTypeId) VALUES ('Mary Stocks', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Movies (Name) VALUES ('Shrek')");
            migrationBuilder.Sql("INSERT INTO Movies (Name) VALUES ('Wall-e')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Customers");
            migrationBuilder.Sql("DELETE FROM Movies");
        }
    }
}
