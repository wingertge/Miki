namespace Miki.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReworkGuildDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guild",
                c => new
                    {
                        id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.GuildRole",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        selectable = c.Boolean(nullable: false),
                        automatically_given = c.Boolean(nullable: false),
                        level_required = c.Int(nullable: false),
                        guild_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Guild", t => t.guild_id, cascadeDelete: true)
                .Index(t => t.guild_id);
            
            AddColumn("dbo.LocalExperience", "Guild_Id", c => c.Long());
            CreateIndex("dbo.LocalExperience", "Guild_Id");
            AddForeignKey("dbo.LocalExperience", "Guild_Id", "dbo.Guild", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalExperience", "Guild_Id", "dbo.Guild");
            DropForeignKey("dbo.GuildRole", "guild_id", "dbo.Guild");
            DropIndex("dbo.GuildRole", new[] { "guild_id" });
            DropIndex("dbo.LocalExperience", new[] { "Guild_Id" });
            DropColumn("dbo.LocalExperience", "Guild_Id");
            DropTable("dbo.GuildRole");
            DropTable("dbo.Guild");
        }
    }
}
