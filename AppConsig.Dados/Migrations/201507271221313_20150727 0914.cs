namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201507270914 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissao", "Url", c => c.String(maxLength: 256));
            AddColumn("dbo.Permissao", "UrlImagem", c => c.String(maxLength: 256));
            AddColumn("dbo.Permissao", "ParenteId", c => c.Long(nullable: false));
            AddColumn("dbo.Permissao", "Ordem", c => c.Int(nullable: false));
            AlterColumn("dbo.Permissao", "Descricao", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.Permissao", "CriadoPor");
            DropColumn("dbo.Permissao", "DataCriacao");
            DropColumn("dbo.Permissao", "AtualizadoPor");
            DropColumn("dbo.Permissao", "DataAtualizacao");
            DropColumn("dbo.Permissao", "Excluido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissao", "Excluido", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissao", "DataAtualizacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Permissao", "AtualizadoPor", c => c.String());
            AddColumn("dbo.Permissao", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Permissao", "CriadoPor", c => c.String());
            AlterColumn("dbo.Permissao", "Descricao", c => c.String(maxLength: 256));
            DropColumn("dbo.Permissao", "Ordem");
            DropColumn("dbo.Permissao", "ParenteId");
            DropColumn("dbo.Permissao", "UrlImagem");
            DropColumn("dbo.Permissao", "Url");
        }
    }
}
