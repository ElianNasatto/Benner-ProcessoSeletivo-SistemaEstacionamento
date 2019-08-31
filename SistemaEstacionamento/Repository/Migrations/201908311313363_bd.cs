namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.estacionado", "id_preco", "dbo.precos");
            DropIndex("dbo.estacionado", new[] { "id_preco" });
            AlterColumn("dbo.estacionado", "id_preco", c => c.Int());
            CreateIndex("dbo.estacionado", "id_preco");
            AddForeignKey("dbo.estacionado", "id_preco", "dbo.precos", "id_preco");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.estacionado", "id_preco", "dbo.precos");
            DropIndex("dbo.estacionado", new[] { "id_preco" });
            AlterColumn("dbo.estacionado", "id_preco", c => c.Int(nullable: false));
            CreateIndex("dbo.estacionado", "id_preco");
            AddForeignKey("dbo.estacionado", "id_preco", "dbo.precos", "id_preco", cascadeDelete: true);
        }
    }
}
