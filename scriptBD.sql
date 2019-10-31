USE [master]
GO
/****** Object:  Database [Ajedrez]    Script Date: 31/10/2019 17:24:53 ******/
CREATE DATABASE [Ajedrez]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Ajedrez', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Ajedrez.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Ajedrez_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Ajedrez_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Ajedrez] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Ajedrez].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Ajedrez] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Ajedrez] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Ajedrez] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Ajedrez] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Ajedrez] SET ARITHABORT OFF 
GO
ALTER DATABASE [Ajedrez] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Ajedrez] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Ajedrez] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Ajedrez] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Ajedrez] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Ajedrez] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Ajedrez] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Ajedrez] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Ajedrez] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Ajedrez] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Ajedrez] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Ajedrez] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Ajedrez] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Ajedrez] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Ajedrez] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Ajedrez] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Ajedrez] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Ajedrez] SET RECOVERY FULL 
GO
ALTER DATABASE [Ajedrez] SET  MULTI_USER 
GO
ALTER DATABASE [Ajedrez] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Ajedrez] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Ajedrez] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Ajedrez] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Ajedrez] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Ajedrez', N'ON'
GO
ALTER DATABASE [Ajedrez] SET QUERY_STORE = OFF
GO
USE [Ajedrez]
GO
/****** Object:  Table [dbo].[Jugador]    Script Date: 31/10/2019 17:24:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jugador](
	[idJugador] [int] NOT NULL,
	[nombre] [varchar](50) NULL,
	[apellido] [varchar](50) NULL,
	[username] [varchar](50) NULL,
	[contraseña] [varchar](100) NOT NULL,
	[cantPartidasGanadas] [int] NOT NULL,
 CONSTRAINT [PK_Jugador] PRIMARY KEY CLUSTERED 
(
	[idJugador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partida]    Script Date: 31/10/2019 17:24:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partida](
	[idPartida] [int] NOT NULL,
	[idJugador1] [int] NOT NULL,
	[idJugador2] [int] NOT NULL,
	[idGanador] [int] NULL,
	[tablas] [bit] NULL,
	[fecha] [datetime] NULL,
 CONSTRAINT [PK_Partida] PRIMARY KEY CLUSTERED 
(
	[idPartida] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Jugador] ([idJugador], [nombre], [apellido], [username], [contraseña], [cantPartidasGanadas]) VALUES (1, N'Ezequiel', N'Celuci', N'eceluci', N'eceluci', 0)
INSERT [dbo].[Jugador] ([idJugador], [nombre], [apellido], [username], [contraseña], [cantPartidasGanadas]) VALUES (2, N'Federico', N'Di Paola', N'fdipaola', N'fdipaola', 0)
INSERT [dbo].[Jugador] ([idJugador], [nombre], [apellido], [username], [contraseña], [cantPartidasGanadas]) VALUES (3, N'dewde', N'wedw', N'eceluci1', N'fdipaola', 0)
INSERT [dbo].[Jugador] ([idJugador], [nombre], [apellido], [username], [contraseña], [cantPartidasGanadas]) VALUES (4, N'Facundo', N'Planella', N'fplanella', N'uto', 0)
ALTER TABLE [dbo].[Partida]  WITH CHECK ADD  CONSTRAINT [FK_Partida_Jugador] FOREIGN KEY([idJugador1])
REFERENCES [dbo].[Jugador] ([idJugador])
GO
ALTER TABLE [dbo].[Partida] CHECK CONSTRAINT [FK_Partida_Jugador]
GO
ALTER TABLE [dbo].[Partida]  WITH CHECK ADD  CONSTRAINT [FK_Partida_Jugador1] FOREIGN KEY([idJugador2])
REFERENCES [dbo].[Jugador] ([idJugador])
GO
ALTER TABLE [dbo].[Partida] CHECK CONSTRAINT [FK_Partida_Jugador1]
GO
ALTER TABLE [dbo].[Partida]  WITH CHECK ADD  CONSTRAINT [FK_Partida_Jugador2] FOREIGN KEY([idGanador])
REFERENCES [dbo].[Jugador] ([idJugador])
GO
ALTER TABLE [dbo].[Partida] CHECK CONSTRAINT [FK_Partida_Jugador2]
GO
/****** Object:  StoredProcedure [dbo].[ACTUALIZAR_PARTIDAS_GANADAS]    Script Date: 31/10/2019 17:24:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[ACTUALIZAR_PARTIDAS_GANADAS]
@idJugador int
as
begin
update Jugador set cantpartidasGanadas = cantpartidasGanadas + 1 where idjugador = @idJugador
end
GO
/****** Object:  StoredProcedure [dbo].[CREAR_JUGADOR]    Script Date: 31/10/2019 17:24:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CREAR_JUGADOR]
@nombre varchar(50), @apellido varchar(50), @username varchar(50), @contraseña varchar(100) 
as
begin
declare @id int
set @id = (select isnull(max(idJugador), 0) from jugador) + 1

insert into jugador values (@id, @nombre, @apellido, @username, @contraseña, 0)
end
GO
/****** Object:  StoredProcedure [dbo].[CREAR_PARTIDA]    Script Date: 31/10/2019 17:24:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[CREAR_PARTIDA]
@idJugador1 int, @idJugador2 int, @idGanador int, @tablas bit, @fecha numeric
as
begin
declare @id int
set @id = (select isnull(max(idPartida), 0) from partida) + 1

insert into partida values (@id, @idJugador1, @idJugador2, @idGanador, @tablas, @fecha)
end
GO
/****** Object:  StoredProcedure [dbo].[VALIDAR_LOGIN]    Script Date: 31/10/2019 17:24:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[VALIDAR_LOGIN]
@username varchar(50), @contraseña varchar(100)
AS
BEGIN
select nombre, apellido, username from jugador where username = @username and contraseña = @contraseña
END
GO
/****** Object:  StoredProcedure [dbo].[VALIDAR_USUARIO]    Script Date: 31/10/2019 17:24:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[VALIDAR_USUARIO]
@username varchar(50)
AS
BEGIN
select count(*) from jugador where username = @username
END
GO
USE [master]
GO
ALTER DATABASE [Ajedrez] SET  READ_WRITE 
GO
