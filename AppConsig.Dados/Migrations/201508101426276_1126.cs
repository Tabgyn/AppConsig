namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1126 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Foto", c => c.Binary());
            AddColumn("dbo.Usuario", "Facebook", c => c.String(maxLength: 256));
            AddColumn("dbo.Usuario", "Twitter", c => c.String(maxLength: 256));
            AddColumn("dbo.Usuario", "Telefone", c => c.String(maxLength: 256));
            AddColumn("dbo.Usuario", "Celular", c => c.String(maxLength: 256));
            AddColumn("dbo.Usuario", "Endereco", c => c.String(maxLength: 256));
            AddColumn("dbo.Usuario", "EnderecoComplemento", c => c.String(maxLength: 256));
            AlterColumn("dbo.Aviso", "CriadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Aviso", "AtualizadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Perfil", "CriadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Perfil", "AtualizadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Usuario", "Senha", c => c.String(nullable: false));
            AlterColumn("dbo.Usuario", "Email", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Usuario", "CriadoPor", c => c.String(nullable: false));
            AlterColumn("dbo.Usuario", "AtualizadoPor", c => c.String(nullable: false));
            DropColumn("dbo.Usuario", "Login");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "Login", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Usuario", "AtualizadoPor", c => c.String());
            AlterColumn("dbo.Usuario", "CriadoPor", c => c.String());
            AlterColumn("dbo.Usuario", "Email", c => c.String());
            AlterColumn("dbo.Usuario", "Senha", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Perfil", "AtualizadoPor", c => c.String());
            AlterColumn("dbo.Perfil", "CriadoPor", c => c.String());
            AlterColumn("dbo.Aviso", "AtualizadoPor", c => c.String());
            AlterColumn("dbo.Aviso", "CriadoPor", c => c.String());
            DropColumn("dbo.Usuario", "EnderecoComplemento");
            DropColumn("dbo.Usuario", "Endereco");
            DropColumn("dbo.Usuario", "Celular");
            DropColumn("dbo.Usuario", "Telefone");
            DropColumn("dbo.Usuario", "Twitter");
            DropColumn("dbo.Usuario", "Facebook");
            DropColumn("dbo.Usuario", "Foto");
        }
    }
}
