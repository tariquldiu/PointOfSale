namespace SignUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Models : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankDetails",
                c => new
                    {
                        BankId = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        BankBranch = c.String(),
                        BankAccountNo = c.String(),
                        BankAccountType = c.String(),
                        PaymentType = c.String(),
                        BankTransactionNo = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BankId);
            
            CreateTable(
                "dbo.Categorys",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        CategoryType = c.String(),
                        Unit = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        InvoiceNo = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransVat = c.String(),
                        TransDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionQty = c.Int(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Order_OrdedrId = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_OrdedrId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CustomerId)
                .Index(t => t.Order_OrdedrId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrdedrId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        OrderNo = c.String(),
                        OrderQty = c.Int(nullable: false),
                        OrderTotal = c.String(),
                        Status = c.Boolean(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrdedrId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductNo = c.String(),
                        ProductName = c.String(),
                        Description = c.String(),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarkupPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductQty = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categorys", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        UserRole = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.DamagedProducts",
                c => new
                    {
                        PurchesReturnId = c.Int(nullable: false, identity: true),
                        PurchesDate = c.DateTime(nullable: false),
                        PurchesReturnDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PurchesAmmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reason = c.String(),
                        Size = c.String(),
                        ProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        StockId = c.Int(nullable: false),
                        Orders_OrdedrId = c.Int(),
                        Stockins_StockinId = c.Int(),
                    })
                .PrimaryKey(t => t.PurchesReturnId)
                .ForeignKey("dbo.Orders", t => t.Orders_OrdedrId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stockins", t => t.Stockins_StockinId)
                .Index(t => t.ProductId)
                .Index(t => t.Orders_OrdedrId)
                .Index(t => t.Stockins_StockinId);
            
            CreateTable(
                "dbo.Stockins",
                c => new
                    {
                        StockinId = c.Int(nullable: false, identity: true),
                        StockinNo = c.String(),
                        DateReceived = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Qty = c.Int(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Orders_OrdedrId = c.Int(),
                    })
                .PrimaryKey(t => t.StockinId)
                .ForeignKey("dbo.Orders", t => t.Orders_OrdedrId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Orders_OrdedrId);
            
            CreateTable(
                "dbo.SalesReturns",
                c => new
                    {
                        SalesReturnId = c.Int(nullable: false, identity: true),
                        ReturnDate = c.DateTime(nullable: false),
                        PurchesDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Size = c.String(),
                        Reason = c.String(),
                        ReturnAmmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        StockinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SalesReturnId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Stockins", t => t.StockinId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ProductId)
                .Index(t => t.StockinId);
            
            CreateTable(
                "dbo.Summarys",
                c => new
                    {
                        SummaryId = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        InvoiceNo = c.String(),
                        PaymentType = c.String(),
                        TransDiscount = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountTendered = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Change = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Boolean(nullable: false),
                        BankId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SummaryId)
                .ForeignKey("dbo.BankDetails", t => t.BankId, cascadeDelete: true)
                .Index(t => t.BankId);
            
            CreateTable(
                "dbo.WastageExchanges",
                c => new
                    {
                        WastageExchangeId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Brand = c.String(),
                        Unit = c.String(),
                        Size = c.String(),
                        PriductId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.WastageExchangeId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.CustomerId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WastageExchanges", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.WastageExchanges", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Summarys", "BankId", "dbo.BankDetails");
            DropForeignKey("dbo.SalesReturns", "StockinId", "dbo.Stockins");
            DropForeignKey("dbo.SalesReturns", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SalesReturns", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.DamagedProducts", "Stockins_StockinId", "dbo.Stockins");
            DropForeignKey("dbo.Stockins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Stockins", "Orders_OrdedrId", "dbo.Orders");
            DropForeignKey("dbo.DamagedProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.DamagedProducts", "Orders_OrdedrId", "dbo.Orders");
            DropForeignKey("dbo.Transactions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Transactions", "Order_OrdedrId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categorys");
            DropForeignKey("dbo.Transactions", "CustomerId", "dbo.Customers");
            DropIndex("dbo.WastageExchanges", new[] { "Product_ProductId" });
            DropIndex("dbo.WastageExchanges", new[] { "CustomerId" });
            DropIndex("dbo.Summarys", new[] { "BankId" });
            DropIndex("dbo.SalesReturns", new[] { "StockinId" });
            DropIndex("dbo.SalesReturns", new[] { "ProductId" });
            DropIndex("dbo.SalesReturns", new[] { "CustomerId" });
            DropIndex("dbo.Stockins", new[] { "Orders_OrdedrId" });
            DropIndex("dbo.Stockins", new[] { "UserId" });
            DropIndex("dbo.DamagedProducts", new[] { "Stockins_StockinId" });
            DropIndex("dbo.DamagedProducts", new[] { "Orders_OrdedrId" });
            DropIndex("dbo.DamagedProducts", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "SupplierId" });
            DropIndex("dbo.Transactions", new[] { "Order_OrdedrId" });
            DropIndex("dbo.Transactions", new[] { "CustomerId" });
            DropIndex("dbo.Transactions", new[] { "UserId" });
            DropTable("dbo.WastageExchanges");
            DropTable("dbo.Summarys");
            DropTable("dbo.SalesReturns");
            DropTable("dbo.Stockins");
            DropTable("dbo.DamagedProducts");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Transactions");
            DropTable("dbo.Categorys");
            DropTable("dbo.BankDetails");
        }
    }
}
