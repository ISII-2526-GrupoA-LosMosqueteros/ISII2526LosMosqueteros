SET IDENTITY_INSERT [dbo].[Fabricantes] ON
INSERT INTO [dbo].[Fabricantes] ([Id], [Nombre]) VALUES (1, N'Phillips')
INSERT INTO [dbo].[Fabricantes] ([Id], [Nombre]) VALUES (2, N'Wurt')
INSERT INTO [dbo].[Fabricantes] ([Id], [Nombre]) VALUES (3, N'Bosch')
SET IDENTITY_INSERT [dbo].[Fabricantes] OFF

SET IDENTITY_INSERT [dbo].[Herramientas] ON
INSERT INTO [dbo].[Herramientas] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (1, N'Destornillador', N'Acero', CAST(12.50 AS Decimal(10, 2)), 1, 3)
INSERT INTO [dbo].[Herramientas] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (2, N'Llave Inglesa', N'Acero', CAST(10.30 AS Decimal(10, 2)), 2, 1)
INSERT INTO [dbo].[Herramientas] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (3, N'Tornillo', N'Acero', CAST(0.50 AS Decimal(10, 2)), 1, 1)
INSERT INTO [dbo].[Herramientas] ([Id], [Nombre], [Material], [Precio], [TiempoReparacion], [FabricanteId]) VALUES (4, N'Tuerca', N'Acero', CAST(0.10 AS Decimal(10, 2)), 1, 3)
SET IDENTITY_INSERT [dbo].[Herramientas] OFF

INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Phone], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1', N'Carlos', N'Gomez', 612345678, N'carlos.gomez', N'CARLOS.GOMEZ', N'carlos.gomez@example.com', N'CARLOS.GOMEZ@EXAMPLE.COM', 1, NULL, N'68E4382A-5EAD-4EE4-AE12-7195321871E5', N'7CCE708A-9B3A-4426-806F-EBB87FCB1CDE', N'612345678', 1, 0, NULL, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Phone], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'2', N'Lucia', N'Martinez', 623456789, N'lucia.martinez', N'LUCIA.MARTINEZ', N'lucia.martinez@example.com', N'LUCIA.MARTINEZ@EXAMPLE.COM', 1, NULL, N'CC99064E-F27D-49B1-9D89-EC50B0E15AB5', N'C0DAC331-04F9-4C4D-8FEF-1807CFBB3237', N'623456789', 1, 0, NULL, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Phone], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3', N'Andres', N'Ruiz', 634567890, N'andres.ruiz', N'ANDRES.RUIZ', N'andres.ruiz@example.com', N'ANDRES.RUIZ@EXAMPLE.COM', 1, NULL, N'619AD214-172D-43D8-97A5-3652A8F95AE2', N'42F93B85-47B2-473E-9219-35B7DA93E779', N'634567890', 1, 0, NULL, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Phone], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4', N'Maria', N'Fernandez', 645678901, N'maria.fernandez', N'MARIA.FERNANDEZ', N'maria.fernandez@example.com', N'MARIA.FERNANDEZ@EXAMPLE.COM', 1, NULL, N'C07A688A-7B8C-44BC-BADA-E843818441E3', N'463E2FC0-5363-400D-BDEF-33140BEB0010', N'645678901', 1, 0, NULL, 0, 0)
INSERT INTO [dbo].[AspNetUsers] ([Id], [Name], [Surname], [Phone], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'5', N'Javier', N'Lopez', 656789012, N'javier.lopez', N'JAVIER.LOPEZ', N'javier.lopez@example.com', N'JAVIER.LOPEZ@EXAMPLE.COM', 1, NULL, N'7286B66E-DF84-4C88-B617-CA366B16D997', N'6C2A4851-ACBF-47A3-8B65-3FB7609596C4', N'656789012', 1, 0, NULL, 0, 0)

SET IDENTITY_INSERT [dbo].[Reparaciones] ON
INSERT INTO [dbo].[Reparaciones] ([Id], [FechaEntrega], [FechaRecogida], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (1, N'2025-10-21 00:00:00', N'2025-10-22 00:00:00', 10.25, 1, 5)
SET IDENTITY_INSERT [dbo].[Reparaciones] OFF

INSERT INTO [dbo].[ReparacionItems] ([ReparacionId], [HerramientaId], [Precio], [Cantidad], [Descripcion]) VALUES (1, 1, 10.20, 2, N'Muy Bonito')

SET IDENTITY_INSERT [dbo].[Alquileres] ON
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (1, N'Calle Mayor 12, Madrid', N'2025-10-14 00:00:00', N'2025-10-20 00:00:00', N'2025-10-15 00:00:00', 5, CAST(120.50 AS Decimal(10, 2)), 0, N'1')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (2, N'Avenida Andalucía 45, Sevilla', N'2025-11-10 00:00:00', N'2025-11-17 00:00:00', N'2025-11-11 00:00:00', 6, CAST(98.75 AS Decimal(10, 2)), 1, N'2')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (3, N'Calle del Carmen 33, Valencia', N'2025-11-25 00:00:00', N'2025-11-30 00:00:00', N'2025-11-26 00:00:00', 4, CAST(150.00 AS Decimal(10, 2)), 2, N'3')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (6, N'Naranja 64', N'2025-10-29 11:44:06', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', 0, CAST(0.00 AS Decimal(10, 2)), 0, N'1')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (7, N'Naranja 64', N'2025-10-29 11:47:19', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', 0, CAST(37.50 AS Decimal(10, 2)), 0, N'1')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (8, N'string', N'2025-10-29 12:22:00', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', 0, CAST(0.00 AS Decimal(10, 2)), 0, N'1')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (9, N'string', N'2025-10-29 12:23:01', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', 0, CAST(0.00 AS Decimal(10, 2)), 0, N'1')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (10, N'string', N'2025-10-29 12:23:11', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', 0, CAST(0.00 AS Decimal(10, 2)), 0, N'1')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (11, N'string', N'2025-10-29 12:24:29', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', 0, CAST(7.50 AS Decimal(10, 2)), 0, N'1')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (12, N'string', N'2025-10-29 12:29:53', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', 0, CAST(15.00 AS Decimal(10, 2)), 0, N'1')
INSERT INTO [dbo].[Alquileres] ([Id], [DireccionEnvio], [FechaAlquiler], [FechaFin], [FechaInicio], [Periodo], [PrecioTotal], [TiposMetodoPago], [ApplicationUserId]) VALUES (13, N'string', N'2025-10-29 12:30:37', N'0001-01-01 00:00:00', N'0001-01-01 00:00:00', 0, CAST(7.50 AS Decimal(10, 2)), 0, N'1')
SET IDENTITY_INSERT [dbo].[Alquileres] OFF

INSERT INTO [dbo].[AlquilarItems] ([AlquilerId], [HerramientaId], [Cantidad], [Precio]) VALUES (6, 1, 3, CAST(0.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[AlquilarItems] ([AlquilerId], [HerramientaId], [Cantidad], [Precio]) VALUES (7, 1, 3, CAST(37.50 AS Decimal(10, 2)))
INSERT INTO [dbo].[AlquilarItems] ([AlquilerId], [HerramientaId], [Cantidad], [Precio]) VALUES (8, 1, 3, CAST(0.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[AlquilarItems] ([AlquilerId], [HerramientaId], [Cantidad], [Precio]) VALUES (9, 1, 3, CAST(0.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[AlquilarItems] ([AlquilerId], [HerramientaId], [Cantidad], [Precio]) VALUES (10, 1, 3, CAST(0.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[AlquilarItems] ([AlquilerId], [HerramientaId], [Cantidad], [Precio]) VALUES (11, 1, 3, CAST(7.50 AS Decimal(10, 2)))
INSERT INTO [dbo].[AlquilarItems] ([AlquilerId], [HerramientaId], [Cantidad], [Precio]) VALUES (12, 1, 3, CAST(15.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[AlquilarItems] ([AlquilerId], [HerramientaId], [Cantidad], [Precio]) VALUES (13, 1, 3, CAST(7.50 AS Decimal(10, 2)))

SET IDENTITY_INSERT [dbo].[Compras] ON
INSERT INTO [dbo].[Compras] ([Id], [DireccionEnvio], [FechaCompra], [PrecioTotal], [TipoMetodoPago], [ApplicationUserId]) VALUES (1, N'Calle Julian Gayarre', N'2025-04-12 00:00:00', CAST(125.00 AS Decimal(10, 2)), 2, NULL)
INSERT INTO [dbo].[Compras] ([Id], [DireccionEnvio], [FechaCompra], [PrecioTotal], [TipoMetodoPago], [ApplicationUserId]) VALUES (2, N'Calle Juan', N'2025-08-10 00:00:00', CAST(10.50 AS Decimal(10, 2)), 1, NULL)
SET IDENTITY_INSERT [dbo].[Compras] OFF

INSERT INTO [dbo].[CompraItems] ([HerramientaId], [CompraId], [Cantidad], [Descripcion], [Precio]) VALUES (1, 1, 10, N'Destronillador de estrella', CAST(125.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[CompraItems] ([HerramientaId], [CompraId], [Cantidad], [Descripcion], [Precio]) VALUES (3, 2, 100, N'Tuercas', CAST(10.00 AS Decimal(10, 2)))
INSERT INTO [dbo].[CompraItems] ([HerramientaId], [CompraId], [Cantidad], [Descripcion], [Precio]) VALUES (4, 2, 1, N'Tornillo', CAST(0.50 AS Decimal(10, 2)))

SET IDENTITY_INSERT [dbo].[Ofertas] ON
INSERT INTO [dbo].[Ofertas] ([Id], [FechaFinal], [FechaInicio], [FechaOferta], [TiposDirigdaOferta], [TiposMetodoPago], [ApplicationUserId]) VALUES (1, N'2025-10-31 00:00:00', N'2025-10-15 00:00:00', N'2025-10-20 00:00:00', 0, 1, N'1')
INSERT INTO [dbo].[Ofertas] ([Id], [FechaFinal], [FechaInicio], [FechaOferta], [TiposDirigdaOferta], [TiposMetodoPago], [ApplicationUserId]) VALUES (2, N'2025-11-30 00:00:00', N'2025-11-10 00:00:00', N'2025-11-15 00:00:00', 1, 1, N'2')
INSERT INTO [dbo].[Ofertas] ([Id], [FechaFinal], [FechaInicio], [FechaOferta], [TiposDirigdaOferta], [TiposMetodoPago], [ApplicationUserId]) VALUES (3, N'2025-12-24 00:00:00', N'2025-12-05 00:00:00', N'2025-12-10 00:00:00', 1, 3, N'2')
INSERT INTO [dbo].[Ofertas] ([Id], [FechaFinal], [FechaInicio], [FechaOferta], [TiposDirigdaOferta], [TiposMetodoPago], [ApplicationUserId]) VALUES (4, N'2026-01-15 00:00:00', N'2025-12-28 00:00:00', N'2026-01-02 00:00:00', 0, 3, N'4')
INSERT INTO [dbo].[Ofertas] ([Id], [FechaFinal], [FechaInicio], [FechaOferta], [TiposDirigdaOferta], [TiposMetodoPago], [ApplicationUserId]) VALUES (5, N'2026-02-06 00:00:00', N'2026-01-20 00:00:00', N'2026-01-25 00:00:00', 1, 1, N'3')
SET IDENTITY_INSERT [dbo].[Ofertas] OFF


INSERT INTO [dbo].[OfertaItems] ([HerramientaId], [OfertaId], [Porcentaje], [PrecioFinal]) VALUES (1, 1, 15, CAST(10.63 AS Decimal(10, 2)))
INSERT INTO [dbo].[OfertaItems] ([HerramientaId], [OfertaId], [Porcentaje], [PrecioFinal]) VALUES (2, 1, 20, CAST(8.24 AS Decimal(10, 2)))
INSERT INTO [dbo].[OfertaItems] ([HerramientaId], [OfertaId], [Porcentaje], [PrecioFinal]) VALUES (1, 4, 5, CAST(11.88 AS Decimal(10, 2)))
INSERT INTO [dbo].[OfertaItems] ([HerramientaId], [OfertaId], [Porcentaje], [PrecioFinal]) VALUES (2, 5, 35, CAST(6.70 AS Decimal(10, 2)))

