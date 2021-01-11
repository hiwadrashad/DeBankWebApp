namespace DeBank.Library.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BankAccounts", name: "User_Id", newName: "Owner_Id");
            RenameIndex(table: "dbo.BankAccounts", name: "IX_User_Id", newName: "IX_Owner_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BankAccounts", name: "IX_Owner_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.BankAccounts", name: "Owner_Id", newName: "User_Id");
        }
    }
}
