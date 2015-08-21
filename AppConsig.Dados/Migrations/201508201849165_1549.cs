namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1549 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Auditoria",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UsuarioId = c.Long(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Acao = c.String(),
                        NomeTabela = c.String(),
                        RegistroId = c.Long(nullable: false),
                        ValorOriginal = c.String(),
                        ValorNovo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Auditoria");
        }
    }
}
