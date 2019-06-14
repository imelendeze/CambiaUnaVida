-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER CambiarEstadoGato 
   ON  dbo.PeticionAdopcions
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @estadoPeticion VARCHAR(15)
	SELECT @estadoPeticion = [status]
	FROM INSERTED

    -- Insert statements for trigger here

	IF @estadoPeticion = 'Esperando cita'
		BEGIN
			Update gato
			set gato.status = 'En proceso'
			from  dbo.Gatoes gato
			inner join inserted i ON gato.id = i.idGatoFK
		END
	ELSE IF @estadoPeticion = 'Rechazada'
		BEGIN
			Update gato
			set gato.status = 'En adopción'
			from  dbo.Gatoes gato
			inner join inserted i ON gato.id = i.idGatoFK
		END
	ELSE IF @estadoPeticion = 'Aceptada'
		BEGIN
			Update gato
			set gato.status = 'Adoptado'
			from  dbo.Gatoes gato
			inner join inserted i ON gato.id = i.idGatoFK
		END	
END
GO
