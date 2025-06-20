USE [cenfocinemas-db]
GO

/****** Object:  StoredProcedure [dbo].[RET_ALL_USERS_PR]    Script Date: 19/06/2025 07:42:28 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RET_ALL_USERS_PR]
AS
BEGIN

	SELECT Id,Created,Updated,UserCode,Name,Email,Password,BirthDate,Status
	FROM TBL_User;
	END
GO

