namespace AppConsig.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Consignacao_Consignataria_Servico_Servidor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consignacao",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                        Codigo = c.String(nullable: false),
                        MaximoParcela = c.Int(nullable: false),
                        ValorMinimo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InicioDaVigenciaEm = c.DateTime(nullable: false),
                        FimDaVigenciaEm = c.DateTime(),
                        PermiteDescontoParcial = c.Boolean(nullable: false),
                        PermiteLancamentoManual = c.Boolean(nullable: false),
                        PermiteOutrasOcorrencias = c.Boolean(nullable: false),
                        ConsignatariaId = c.Long(nullable: false),
                        ServicoId = c.Long(nullable: false),
                        CriadoPor = c.String(nullable: false, maxLength: 256),
                        CriadoEm = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(nullable: false, maxLength: 256),
                        AtualizadoEm = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Consignataria", t => t.ConsignatariaId, cascadeDelete: true)
                .ForeignKey("dbo.Servico", t => t.ServicoId, cascadeDelete: true)
                .Index(t => t.ConsignatariaId)
                .Index(t => t.ServicoId);
            
            CreateTable(
                "dbo.Consignataria",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Sigla = c.String(nullable: false),
                        CNPJ = c.String(nullable: false),
                        Codigo = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        TipoRepresentante = c.Int(nullable: false),
                        CriadoPor = c.String(nullable: false, maxLength: 256),
                        CriadoEm = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(nullable: false, maxLength: 256),
                        AtualizadoEm = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servico",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                        TipoServicoRelacao = c.Int(nullable: false),
                        TipoServicoInerente = c.Int(nullable: false),
                        Ordem = c.Int(nullable: false),
                        CriadoPor = c.String(nullable: false, maxLength: 256),
                        CriadoEm = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(nullable: false, maxLength: 256),
                        AtualizadoEm = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servidor",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        CPF = c.String(nullable: false),
                        Matricula = c.String(nullable: false),
                        NascidoEm = c.DateTime(),
                        Foto = c.String(),
                        AdmitidoEm = c.DateTime(),
                        AfastadoEm = c.DateTime(),
                        DepartamentoId = c.Long(nullable: false),
                        CriadoPor = c.String(nullable: false, maxLength: 256),
                        CriadoEm = c.DateTime(nullable: false),
                        AtualizadoPor = c.String(nullable: false, maxLength: 256),
                        AtualizadoEm = c.DateTime(nullable: false),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departamento", t => t.DepartamentoId, cascadeDelete: true)
                .Index(t => t.DepartamentoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Servidor", "DepartamentoId", "dbo.Departamento");
            DropForeignKey("dbo.Consignacao", "ServicoId", "dbo.Servico");
            DropForeignKey("dbo.Consignacao", "ConsignatariaId", "dbo.Consignataria");
            DropIndex("dbo.Servidor", new[] { "DepartamentoId" });
            DropIndex("dbo.Consignacao", new[] { "ServicoId" });
            DropIndex("dbo.Consignacao", new[] { "ConsignatariaId" });
            DropTable("dbo.Servidor");
            DropTable("dbo.Servico");
            DropTable("dbo.Consignataria");
            DropTable("dbo.Consignacao");
        }
    }
}
