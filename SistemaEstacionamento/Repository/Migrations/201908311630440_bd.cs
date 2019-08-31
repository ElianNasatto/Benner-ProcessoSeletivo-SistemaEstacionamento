namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.estacionado", "duracao", c => c.String());
            AlterColumn("dbo.estacionado", "tempo_cobrado", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.estacionado", "tempo_cobrado", c => c.Int(nullable: false));
            AlterColumn("dbo.estacionado", "duracao", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
