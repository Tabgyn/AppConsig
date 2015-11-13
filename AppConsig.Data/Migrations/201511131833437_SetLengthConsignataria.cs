namespace AppConsig.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetLengthConsignataria : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Consignataria", "CNPJ", c => c.String(nullable: false, maxLength: 14, fixedLength: true));
            AlterColumn("dbo.Servidor", "CPF", c => c.String(nullable: false, maxLength: 11, fixedLength: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Servidor", "CPF", c => c.String(nullable: false));
            AlterColumn("dbo.Consignataria", "CNPJ", c => c.String(nullable: false));
        }
    }
}
