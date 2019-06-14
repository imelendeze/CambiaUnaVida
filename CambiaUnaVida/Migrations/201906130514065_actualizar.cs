namespace CambiaUnaVida.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adopcions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idPeticionAdopcionFK = c.Int(nullable: false),
                        idTrabajadorSocialFK = c.String(),
                        idVeterinarioFK = c.String(),
                        fecha = c.DateTime(nullable: false),
                        hora = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.PeticionAdopcions", t => t.idPeticionAdopcionFK, cascadeDelete: true)
                .Index(t => t.idPeticionAdopcionFK);
            
            CreateTable(
                "dbo.PeticionAdopcions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idAdoptanteFK = c.String(maxLength: 128),
                        idGatoFK = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        status = c.String(nullable: false, maxLength: 15),
                        observaciones = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.idAdoptanteFK)
                .ForeignKey("dbo.Gatoes", t => t.idGatoFK, cascadeDelete: true)
                .Index(t => t.idAdoptanteFK)
                .Index(t => t.idGatoFK);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(maxLength: 20),
                        apellidos = c.String(maxLength: 30),
                        direccion = c.String(maxLength: 80),
                        telefono = c.String(maxLength: 20),
                        sexo = c.String(maxLength: 20),
                        edad = c.Int(nullable: false),
                        ocupacion = c.String(maxLength: 30),
                        nombreReferencia = c.String(maxLength: 20),
                        apellidosReferencia = c.String(maxLength: 30),
                        telefonoReferencia = c.String(maxLength: 20),
                        direccionReferencia = c.String(maxLength: 80),
                        emailReferencia = c.String(maxLength: 30),
                        cedulaProfesional = c.String(maxLength: 30),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Gatoes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idResponsableFK = c.String(nullable: false, maxLength: 128),
                        nombre = c.String(nullable: false, maxLength: 20),
                        edad = c.String(nullable: false, maxLength: 20),
                        sexo = c.String(nullable: false, maxLength: 15),
                        foto = c.String(nullable: false, maxLength: 80),
                        observaciones = c.String(nullable: false, maxLength: 250),
                        padecimientos = c.String(nullable: false, maxLength: 250),
                        dieta = c.String(nullable: false, maxLength: 250),
                        status = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.idResponsableFK, cascadeDelete: true)
                .Index(t => t.idResponsableFK);
            
            CreateTable(
                "dbo.Adopcion_Usuarios",
                c => new
                    {
                        idAdopcionFK = c.Int(nullable: false),
                        idUsuFK = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.idAdopcionFK, t.idUsuFK })
                .ForeignKey("dbo.Adopcions", t => t.idAdopcionFK, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.idUsuFK, cascadeDelete: false)
                .Index(t => t.idAdopcionFK)
                .Index(t => t.idUsuFK);
            
            CreateTable(
                "dbo.CitaAdopcions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idPeticionAdopcionFK = c.Int(nullable: false),
                        idTrabajadorSocialFK = c.String(maxLength: 128),
                        fecha = c.DateTime(nullable: false),
                        hora = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.PeticionAdopcions", t => t.idPeticionAdopcionFK, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.idTrabajadorSocialFK)
                .Index(t => t.idPeticionAdopcionFK)
                .Index(t => t.idTrabajadorSocialFK);
            
            CreateTable(
                "dbo.CitaVeterinario_Usuarios",
                c => new
                    {
                        idCitaVeterinarioFK = c.Int(nullable: false),
                        idUsuFK = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.idCitaVeterinarioFK, t.idUsuFK })
                .ForeignKey("dbo.CitaVeterinarios", t => t.idCitaVeterinarioFK, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.idUsuFK, cascadeDelete: true)
                .Index(t => t.idCitaVeterinarioFK)
                .Index(t => t.idUsuFK);
            
            CreateTable(
                "dbo.CitaVeterinarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idAdoptanteFK = c.String(),
                        idVeterinarioFK = c.String(),
                        idGato = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        hora = c.String(nullable: false, maxLength: 10),
                        status = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Gatoes", t => t.idGato, cascadeDelete: false)
                .Index(t => t.idGato);
            
            CreateTable(
                "dbo.ReporteCitaVeterinarios",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idCitaVeterinarioFK = c.Int(nullable: false),
                        observaciones = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CitaVeterinarios", t => t.idCitaVeterinarioFK, cascadeDelete: true)
                .Index(t => t.idCitaVeterinarioFK);
            
            CreateTable(
                "dbo.ReporteVisitaTrabajadorSocials",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idVisitaTrabajadorSocial = c.Int(nullable: false),
                        observaciones = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.VisitaTrabajadorSocials", t => t.idVisitaTrabajadorSocial, cascadeDelete: true)
                .Index(t => t.idVisitaTrabajadorSocial);
            
            CreateTable(
                "dbo.VisitaTrabajadorSocials",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idTrabajadorSocialFK = c.String(),
                        idAdoptanteFK = c.String(),
                        idGatoFK = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        hora = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Gatoes", t => t.idGatoFK, cascadeDelete: true)
                .Index(t => t.idGatoFK);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.VisitaTrabajadorSocial_Usuarios",
                c => new
                    {
                        idVisitaTrabajadorSocialFK = c.Int(nullable: false),
                        idUsuFK = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.idVisitaTrabajadorSocialFK, t.idUsuFK })
                .ForeignKey("dbo.AspNetUsers", t => t.idUsuFK, cascadeDelete: true)
                .ForeignKey("dbo.VisitaTrabajadorSocials", t => t.idVisitaTrabajadorSocialFK, cascadeDelete: false)
                .Index(t => t.idVisitaTrabajadorSocialFK)
                .Index(t => t.idUsuFK);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VisitaTrabajadorSocial_Usuarios", "idVisitaTrabajadorSocialFK", "dbo.VisitaTrabajadorSocials");
            DropForeignKey("dbo.VisitaTrabajadorSocial_Usuarios", "idUsuFK", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ReporteVisitaTrabajadorSocials", "idVisitaTrabajadorSocial", "dbo.VisitaTrabajadorSocials");
            DropForeignKey("dbo.VisitaTrabajadorSocials", "idGatoFK", "dbo.Gatoes");
            DropForeignKey("dbo.ReporteCitaVeterinarios", "idCitaVeterinarioFK", "dbo.CitaVeterinarios");
            DropForeignKey("dbo.CitaVeterinario_Usuarios", "idUsuFK", "dbo.AspNetUsers");
            DropForeignKey("dbo.CitaVeterinario_Usuarios", "idCitaVeterinarioFK", "dbo.CitaVeterinarios");
            DropForeignKey("dbo.CitaVeterinarios", "idGato", "dbo.Gatoes");
            DropForeignKey("dbo.CitaAdopcions", "idTrabajadorSocialFK", "dbo.AspNetUsers");
            DropForeignKey("dbo.CitaAdopcions", "idPeticionAdopcionFK", "dbo.PeticionAdopcions");
            DropForeignKey("dbo.Adopcion_Usuarios", "idUsuFK", "dbo.AspNetUsers");
            DropForeignKey("dbo.Adopcion_Usuarios", "idAdopcionFK", "dbo.Adopcions");
            DropForeignKey("dbo.Adopcions", "idPeticionAdopcionFK", "dbo.PeticionAdopcions");
            DropForeignKey("dbo.PeticionAdopcions", "idGatoFK", "dbo.Gatoes");
            DropForeignKey("dbo.Gatoes", "idResponsableFK", "dbo.AspNetUsers");
            DropForeignKey("dbo.PeticionAdopcions", "idAdoptanteFK", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.VisitaTrabajadorSocial_Usuarios", new[] { "idUsuFK" });
            DropIndex("dbo.VisitaTrabajadorSocial_Usuarios", new[] { "idVisitaTrabajadorSocialFK" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.VisitaTrabajadorSocials", new[] { "idGatoFK" });
            DropIndex("dbo.ReporteVisitaTrabajadorSocials", new[] { "idVisitaTrabajadorSocial" });
            DropIndex("dbo.ReporteCitaVeterinarios", new[] { "idCitaVeterinarioFK" });
            DropIndex("dbo.CitaVeterinarios", new[] { "idGato" });
            DropIndex("dbo.CitaVeterinario_Usuarios", new[] { "idUsuFK" });
            DropIndex("dbo.CitaVeterinario_Usuarios", new[] { "idCitaVeterinarioFK" });
            DropIndex("dbo.CitaAdopcions", new[] { "idTrabajadorSocialFK" });
            DropIndex("dbo.CitaAdopcions", new[] { "idPeticionAdopcionFK" });
            DropIndex("dbo.Adopcion_Usuarios", new[] { "idUsuFK" });
            DropIndex("dbo.Adopcion_Usuarios", new[] { "idAdopcionFK" });
            DropIndex("dbo.Gatoes", new[] { "idResponsableFK" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.PeticionAdopcions", new[] { "idGatoFK" });
            DropIndex("dbo.PeticionAdopcions", new[] { "idAdoptanteFK" });
            DropIndex("dbo.Adopcions", new[] { "idPeticionAdopcionFK" });
            DropTable("dbo.VisitaTrabajadorSocial_Usuarios");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.VisitaTrabajadorSocials");
            DropTable("dbo.ReporteVisitaTrabajadorSocials");
            DropTable("dbo.ReporteCitaVeterinarios");
            DropTable("dbo.CitaVeterinarios");
            DropTable("dbo.CitaVeterinario_Usuarios");
            DropTable("dbo.CitaAdopcions");
            DropTable("dbo.Adopcion_Usuarios");
            DropTable("dbo.Gatoes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.PeticionAdopcions");
            DropTable("dbo.Adopcions");
        }
    }
}
