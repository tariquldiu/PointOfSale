namespace SignUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SupplierV2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(),
                        ContactNo = c.String(),
                        Company = c.String(),
                        CompanyAddress = c.String(),
                        FactoryAddress = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Suppliers");
        }
    }
}
