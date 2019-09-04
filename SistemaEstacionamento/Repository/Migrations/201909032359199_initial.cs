namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.carros",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        placa = c.String(),
                        registro_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.estacionado",
                c => new
                    {
                        id_estacionado = c.Int(nullable: false, identity: true),
                        id_carro = c.Int(nullable: false),
                        id_preco = c.Int(),
                        data_entrada = c.DateTime(nullable: false),
                        data_saida = c.DateTime(nullable: false),
                        duracao = c.String(),
                        tempo_cobrado = c.String(),
                        valor_pagar = c.Decimal(nullable: false, precision: 18, scale: 2),
                        registro_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_estacionado)
                .ForeignKey("dbo.carros", t => t.id_carro, cascadeDelete: true)
                .ForeignKey("dbo.precos", t => t.id_preco)
                .Index(t => t.id_carro)
                .Index(t => t.id_preco);
            
            CreateTable(
                "dbo.precos",
                c => new
                    {
                        id_preco = c.Int(nullable: false, identity: true),
                        preco_hora = c.Decimal(nullable: false, precision: 18, scale: 2),
                        data_inicial = c.DateTime(nullable: false),
                        data_final = c.DateTime(nullable: false),
                        registro_ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_preco);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.estacionado", "id_preco", "dbo.precos");
            DropForeignKey("dbo.estacionado", "id_carro", "dbo.carros");
            DropIndex("dbo.estacionado", new[] { "id_preco" });
            DropIndex("dbo.estacionado", new[] { "id_carro" });
            DropTable("dbo.precos");
            DropTable("dbo.estacionado");
            DropTable("dbo.carros");
        }
    }
}
