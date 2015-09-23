namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0914 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Auditoria");
            CreateTable(
                "dbo.DetalheAuditoria",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Propriedade = c.String(nullable: false),
                        ValorOriginal = c.String(),
                        ValorNovo = c.String(),
                        Auditoria_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Auditoria", t => t.Auditoria_Id)
                .Index(t => t.Auditoria_Id);
            
            AddColumn("dbo.Auditoria", "Usuario", c => c.String());
            AddColumn("dbo.Auditoria", "DataEvento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Auditoria", "TipoEvento", c => c.Int(nullable: false));
            AddColumn("dbo.Auditoria", "NomeEntidade", c => c.String(nullable: false));
            AlterColumn("dbo.Auditoria", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Auditoria", "RegistroId", c => c.String(nullable: false));
            AlterColumn("dbo.Orgao", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Orgao", "Descricao", c => c.String(maxLength: 100));
            AlterColumn("dbo.SistemaFolha", "Nome", c => c.String(maxLength: 100));
            AlterColumn("dbo.Perfil", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Usuario", "Nome", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Usuario", "Sobrenome", c => c.String(maxLength: 100));
            AlterColumn("dbo.Usuario", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Usuario", "Facebook", c => c.String(maxLength: 100));
            AlterColumn("dbo.Usuario", "Twitter", c => c.String(maxLength: 100));
            AlterColumn("dbo.Usuario", "Telefone", c => c.String(maxLength: 100));
            AlterColumn("dbo.Usuario", "Celular", c => c.String(maxLength: 100));
            AlterColumn("dbo.Usuario", "Endereco", c => c.String(maxLength: 100));
            AlterColumn("dbo.Usuario", "EnderecoComplemento", c => c.String(maxLength: 100));
            AddPrimaryKey("dbo.Auditoria", "Id");
            DropColumn("dbo.Auditoria", "UsuarioId");
            DropColumn("dbo.Auditoria", "SessionId");
            DropColumn("dbo.Auditoria", "DataCriacao");
            DropColumn("dbo.Auditoria", "Acao");
            DropColumn("dbo.Auditoria", "Controle");
            DropColumn("dbo.Auditoria", "NomeTabela");
            DropColumn("dbo.Auditoria", "ValorOriginal");
            DropColumn("dbo.Auditoria", "ValorNovo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Auditoria", "ValorNovo", c => c.String());
            AddColumn("dbo.Auditoria", "ValorOriginal", c => c.String());
            AddColumn("dbo.Auditoria", "NomeTabela", c => c.String());
            AddColumn("dbo.Auditoria", "Controle", c => c.String());
            AddColumn("dbo.Auditoria", "Acao", c => c.String());
            AddColumn("dbo.Auditoria", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Auditoria", "SessionId", c => c.String());
            AddColumn("dbo.Auditoria", "UsuarioId", c => c.Long(nullable: false));
            DropForeignKey("dbo.DetalheAuditoria", "Auditoria_Id", "dbo.Auditoria");
            DropIndex("dbo.DetalheAuditoria", new[] { "Auditoria_Id" });
            DropPrimaryKey("dbo.Auditoria");
            AlterColumn("dbo.Usuario", "EnderecoComplemento", c => c.String(maxLength: 256));
            AlterColumn("dbo.Usuario", "Endereco", c => c.String(maxLength: 256));
            AlterColumn("dbo.Usuario", "Celular", c => c.String(maxLength: 256));
            AlterColumn("dbo.Usuario", "Telefone", c => c.String(maxLength: 256));
            AlterColumn("dbo.Usuario", "Twitter", c => c.String(maxLength: 256));
            AlterColumn("dbo.Usuario", "Facebook", c => c.String(maxLength: 256));
            AlterColumn("dbo.Usuario", "Email", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Usuario", "Sobrenome", c => c.String(maxLength: 256));
            AlterColumn("dbo.Usuario", "Nome", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Perfil", "Nome", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.SistemaFolha", "Nome", c => c.String());
            AlterColumn("dbo.Orgao", "Descricao", c => c.String());
            AlterColumn("dbo.Orgao", "Nome", c => c.String(nullable: false));
            AlterColumn("dbo.Auditoria", "RegistroId", c => c.Long(nullable: false));
            AlterColumn("dbo.Auditoria", "Id", c => c.Guid(nullable: false));
            DropColumn("dbo.Auditoria", "NomeEntidade");
            DropColumn("dbo.Auditoria", "TipoEvento");
            DropColumn("dbo.Auditoria", "DataEvento");
            DropColumn("dbo.Auditoria", "Usuario");
            DropTable("dbo.DetalheAuditoria");
            AddPrimaryKey("dbo.Auditoria", "Id");
        }
    }
}
