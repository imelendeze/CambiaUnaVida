USE [master]
GO
/****** Object:  Database [CambiaUnaVida]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE DATABASE [CambiaUnaVida]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CambiaUnaVida', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\CambiaUnaVida.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'CambiaUnaVida_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\CambiaUnaVida_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CambiaUnaVida] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CambiaUnaVida].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CambiaUnaVida] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET ARITHABORT OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [CambiaUnaVida] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CambiaUnaVida] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CambiaUnaVida] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CambiaUnaVida] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CambiaUnaVida] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CambiaUnaVida] SET  MULTI_USER 
GO
ALTER DATABASE [CambiaUnaVida] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CambiaUnaVida] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CambiaUnaVida] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CambiaUnaVida] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [CambiaUnaVida]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Adopcion_Usuarios]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adopcion_Usuarios](
	[idAdopcionFK] [int] NOT NULL,
	[idUsuFK] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Adopcion_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idAdopcionFK] ASC,
	[idUsuFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Adopcions]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adopcions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idPeticionAdopcionFK] [int] NOT NULL,
	[idTrabajadorSocialFK] [nvarchar](max) NULL,
	[idVeterinarioFK] [nvarchar](max) NULL,
	[fecha] [datetime] NOT NULL,
	[hora] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_dbo.Adopcions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[nombre] [nvarchar](20) NULL,
	[apellidos] [nvarchar](30) NULL,
	[direccion] [nvarchar](80) NULL,
	[telefono] [nvarchar](20) NULL,
	[sexo] [nvarchar](20) NULL,
	[edad] [int] NOT NULL,
	[ocupacion] [nvarchar](30) NULL,
	[nombreReferencia] [nvarchar](20) NULL,
	[apellidosReferencia] [nvarchar](30) NULL,
	[telefonoReferencia] [nvarchar](20) NULL,
	[direccionReferencia] [nvarchar](80) NULL,
	[emailReferencia] [nvarchar](30) NULL,
	[cedulaProfesional] [nvarchar](30) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CitaAdopcions]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CitaAdopcions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idPeticionAdopcionFK] [int] NOT NULL,
	[idTrabajadorSocialFK] [nvarchar](128) NULL,
	[fecha] [datetime] NOT NULL,
	[hora] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_dbo.CitaAdopcions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CitaVeterinario_Usuarios]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CitaVeterinario_Usuarios](
	[idCitaVeterinarioFK] [int] NOT NULL,
	[idUsuFK] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.CitaVeterinario_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idCitaVeterinarioFK] ASC,
	[idUsuFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CitaVeterinarios]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CitaVeterinarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idAdoptanteFK] [nvarchar](max) NULL,
	[idVeterinarioFK] [nvarchar](max) NULL,
	[idGato] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[hora] [nvarchar](10) NOT NULL,
	[status] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_dbo.CitaVeterinarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gatoes]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gatoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idResponsableFK] [nvarchar](128) NOT NULL,
	[nombre] [nvarchar](20) NOT NULL,
	[edad] [nvarchar](20) NOT NULL,
	[sexo] [nvarchar](15) NOT NULL,
	[foto] [nvarchar](80) NOT NULL,
	[observaciones] [nvarchar](250) NOT NULL,
	[padecimientos] [nvarchar](250) NOT NULL,
	[dieta] [nvarchar](250) NOT NULL,
	[status] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_dbo.Gatoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PeticionAdopcions]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PeticionAdopcions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idAdoptanteFK] [nvarchar](128) NULL,
	[idGatoFK] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[status] [nvarchar](15) NOT NULL,
	[observaciones] [nvarchar](250) NULL,
 CONSTRAINT [PK_dbo.PeticionAdopcions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReporteCitaVeterinarios]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReporteCitaVeterinarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idCitaVeterinarioFK] [int] NOT NULL,
	[observaciones] [nvarchar](400) NOT NULL,
 CONSTRAINT [PK_dbo.ReporteCitaVeterinarios] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ReporteVisitaTrabajadorSocials]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReporteVisitaTrabajadorSocials](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idVisitaTrabajadorSocial] [int] NOT NULL,
	[observaciones] [nvarchar](400) NOT NULL,
 CONSTRAINT [PK_dbo.ReporteVisitaTrabajadorSocials] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VisitaTrabajadorSocial_Usuarios]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitaTrabajadorSocial_Usuarios](
	[idVisitaTrabajadorSocialFK] [int] NOT NULL,
	[idUsuFK] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.VisitaTrabajadorSocial_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idVisitaTrabajadorSocialFK] ASC,
	[idUsuFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VisitaTrabajadorSocials]    Script Date: 13/06/2019 11:46:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitaTrabajadorSocials](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idTrabajadorSocialFK] [nvarchar](max) NULL,
	[idAdoptanteFK] [nvarchar](max) NULL,
	[idGatoFK] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[hora] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_dbo.VisitaTrabajadorSocials] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_idAdopcionFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idAdopcionFK] ON [dbo].[Adopcion_Usuarios]
(
	[idAdopcionFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_idUsuFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idUsuFK] ON [dbo].[Adopcion_Usuarios]
(
	[idUsuFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idPeticionAdopcionFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idPeticionAdopcionFK] ON [dbo].[Adopcions]
(
	[idPeticionAdopcionFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idPeticionAdopcionFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idPeticionAdopcionFK] ON [dbo].[CitaAdopcions]
(
	[idPeticionAdopcionFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_idTrabajadorSocialFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idTrabajadorSocialFK] ON [dbo].[CitaAdopcions]
(
	[idTrabajadorSocialFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idCitaVeterinarioFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idCitaVeterinarioFK] ON [dbo].[CitaVeterinario_Usuarios]
(
	[idCitaVeterinarioFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_idUsuFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idUsuFK] ON [dbo].[CitaVeterinario_Usuarios]
(
	[idUsuFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idGato]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idGato] ON [dbo].[CitaVeterinarios]
(
	[idGato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_idResponsableFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idResponsableFK] ON [dbo].[Gatoes]
(
	[idResponsableFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_idAdoptanteFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idAdoptanteFK] ON [dbo].[PeticionAdopcions]
(
	[idAdoptanteFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idGatoFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idGatoFK] ON [dbo].[PeticionAdopcions]
(
	[idGatoFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idCitaVeterinarioFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idCitaVeterinarioFK] ON [dbo].[ReporteCitaVeterinarios]
(
	[idCitaVeterinarioFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idVisitaTrabajadorSocial]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idVisitaTrabajadorSocial] ON [dbo].[ReporteVisitaTrabajadorSocials]
(
	[idVisitaTrabajadorSocial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_idUsuFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idUsuFK] ON [dbo].[VisitaTrabajadorSocial_Usuarios]
(
	[idUsuFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idVisitaTrabajadorSocialFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idVisitaTrabajadorSocialFK] ON [dbo].[VisitaTrabajadorSocial_Usuarios]
(
	[idVisitaTrabajadorSocialFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_idGatoFK]    Script Date: 13/06/2019 11:46:54 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_idGatoFK] ON [dbo].[VisitaTrabajadorSocials]
(
	[idGatoFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Adopcion_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Adopcion_Usuarios_dbo.Adopcions_idAdopcionFK] FOREIGN KEY([idAdopcionFK])
REFERENCES [dbo].[Adopcions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Adopcion_Usuarios] CHECK CONSTRAINT [FK_dbo.Adopcion_Usuarios_dbo.Adopcions_idAdopcionFK]
GO
ALTER TABLE [dbo].[Adopcion_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Adopcion_Usuarios_dbo.AspNetUsers_idUsuFK] FOREIGN KEY([idUsuFK])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Adopcion_Usuarios] CHECK CONSTRAINT [FK_dbo.Adopcion_Usuarios_dbo.AspNetUsers_idUsuFK]
GO
ALTER TABLE [dbo].[Adopcions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Adopcions_dbo.PeticionAdopcions_idPeticionAdopcionFK] FOREIGN KEY([idPeticionAdopcionFK])
REFERENCES [dbo].[PeticionAdopcions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Adopcions] CHECK CONSTRAINT [FK_dbo.Adopcions_dbo.PeticionAdopcions_idPeticionAdopcionFK]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CitaAdopcions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CitaAdopcions_dbo.AspNetUsers_idTrabajadorSocialFK] FOREIGN KEY([idTrabajadorSocialFK])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[CitaAdopcions] CHECK CONSTRAINT [FK_dbo.CitaAdopcions_dbo.AspNetUsers_idTrabajadorSocialFK]
GO
ALTER TABLE [dbo].[CitaAdopcions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CitaAdopcions_dbo.PeticionAdopcions_idPeticionAdopcionFK] FOREIGN KEY([idPeticionAdopcionFK])
REFERENCES [dbo].[PeticionAdopcions] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CitaAdopcions] CHECK CONSTRAINT [FK_dbo.CitaAdopcions_dbo.PeticionAdopcions_idPeticionAdopcionFK]
GO
ALTER TABLE [dbo].[CitaVeterinario_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CitaVeterinario_Usuarios_dbo.AspNetUsers_idUsuFK] FOREIGN KEY([idUsuFK])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CitaVeterinario_Usuarios] CHECK CONSTRAINT [FK_dbo.CitaVeterinario_Usuarios_dbo.AspNetUsers_idUsuFK]
GO
ALTER TABLE [dbo].[CitaVeterinario_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CitaVeterinario_Usuarios_dbo.CitaVeterinarios_idCitaVeterinarioFK] FOREIGN KEY([idCitaVeterinarioFK])
REFERENCES [dbo].[CitaVeterinarios] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CitaVeterinario_Usuarios] CHECK CONSTRAINT [FK_dbo.CitaVeterinario_Usuarios_dbo.CitaVeterinarios_idCitaVeterinarioFK]
GO
ALTER TABLE [dbo].[CitaVeterinarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CitaVeterinarios_dbo.Gatoes_idGato] FOREIGN KEY([idGato])
REFERENCES [dbo].[Gatoes] ([id])
GO
ALTER TABLE [dbo].[CitaVeterinarios] CHECK CONSTRAINT [FK_dbo.CitaVeterinarios_dbo.Gatoes_idGato]
GO
ALTER TABLE [dbo].[Gatoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Gatoes_dbo.AspNetUsers_idResponsableFK] FOREIGN KEY([idResponsableFK])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Gatoes] CHECK CONSTRAINT [FK_dbo.Gatoes_dbo.AspNetUsers_idResponsableFK]
GO
ALTER TABLE [dbo].[PeticionAdopcions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PeticionAdopcions_dbo.AspNetUsers_idAdoptanteFK] FOREIGN KEY([idAdoptanteFK])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[PeticionAdopcions] CHECK CONSTRAINT [FK_dbo.PeticionAdopcions_dbo.AspNetUsers_idAdoptanteFK]
GO
ALTER TABLE [dbo].[PeticionAdopcions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PeticionAdopcions_dbo.Gatoes_idGatoFK] FOREIGN KEY([idGatoFK])
REFERENCES [dbo].[Gatoes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PeticionAdopcions] CHECK CONSTRAINT [FK_dbo.PeticionAdopcions_dbo.Gatoes_idGatoFK]
GO
ALTER TABLE [dbo].[ReporteCitaVeterinarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReporteCitaVeterinarios_dbo.CitaVeterinarios_idCitaVeterinarioFK] FOREIGN KEY([idCitaVeterinarioFK])
REFERENCES [dbo].[CitaVeterinarios] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReporteCitaVeterinarios] CHECK CONSTRAINT [FK_dbo.ReporteCitaVeterinarios_dbo.CitaVeterinarios_idCitaVeterinarioFK]
GO
ALTER TABLE [dbo].[ReporteVisitaTrabajadorSocials]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ReporteVisitaTrabajadorSocials_dbo.VisitaTrabajadorSocials_idVisitaTrabajadorSocial] FOREIGN KEY([idVisitaTrabajadorSocial])
REFERENCES [dbo].[VisitaTrabajadorSocials] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReporteVisitaTrabajadorSocials] CHECK CONSTRAINT [FK_dbo.ReporteVisitaTrabajadorSocials_dbo.VisitaTrabajadorSocials_idVisitaTrabajadorSocial]
GO
ALTER TABLE [dbo].[VisitaTrabajadorSocial_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.VisitaTrabajadorSocial_Usuarios_dbo.AspNetUsers_idUsuFK] FOREIGN KEY([idUsuFK])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VisitaTrabajadorSocial_Usuarios] CHECK CONSTRAINT [FK_dbo.VisitaTrabajadorSocial_Usuarios_dbo.AspNetUsers_idUsuFK]
GO
ALTER TABLE [dbo].[VisitaTrabajadorSocial_Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.VisitaTrabajadorSocial_Usuarios_dbo.VisitaTrabajadorSocials_idVisitaTrabajadorSocialFK] FOREIGN KEY([idVisitaTrabajadorSocialFK])
REFERENCES [dbo].[VisitaTrabajadorSocials] ([id])
GO
ALTER TABLE [dbo].[VisitaTrabajadorSocial_Usuarios] CHECK CONSTRAINT [FK_dbo.VisitaTrabajadorSocial_Usuarios_dbo.VisitaTrabajadorSocials_idVisitaTrabajadorSocialFK]
GO
ALTER TABLE [dbo].[VisitaTrabajadorSocials]  WITH CHECK ADD  CONSTRAINT [FK_dbo.VisitaTrabajadorSocials_dbo.Gatoes_idGatoFK] FOREIGN KEY([idGatoFK])
REFERENCES [dbo].[Gatoes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VisitaTrabajadorSocials] CHECK CONSTRAINT [FK_dbo.VisitaTrabajadorSocials_dbo.Gatoes_idGatoFK]
GO
USE [master]
GO
ALTER DATABASE [CambiaUnaVida] SET  READ_WRITE 
GO
