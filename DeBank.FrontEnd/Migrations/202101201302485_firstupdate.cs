namespace DeBank.FrontEnd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstupdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        DateOfCreation = c.DateTime(nullable: false),
                        Info_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Information", t => t.Info_id)
                .Index(t => t.Info_id);
            
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IBAN = c.String(),
                        DateOfCreation = c.DateTime(nullable: false),
                        info_id = c.String(maxLength: 128),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Information", t => t.info_id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .Index(t => t.info_id)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        Firstname = c.String(),
                        Addition = c.String(),
                        Lastname = c.String(),
                        Postalcode = c.String(),
                        Streetname = c.String(),
                        Streetnumber = c.String(),
                        City = c.String(),
                        Telephonenumber = c.String(),
                        Emailadress = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Info_id", "dbo.Information");
            DropForeignKey("dbo.BankAccounts", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.BankAccounts", "info_id", "dbo.Information");
            DropIndex("dbo.BankAccounts", new[] { "Owner_Id" });
            DropIndex("dbo.BankAccounts", new[] { "info_id" });
            DropIndex("dbo.Users", new[] { "Info_id" });
            DropTable("dbo.Information");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.Users");
        }
    }
}
