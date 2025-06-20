USE [cenfocinemas-db]
GO

/****** Object:  StoredProcedure [dbo].[RET_USER_BY_EMAIL_PR]    Script Date: 19/06/2025 09:04:01 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RET_USER_BY_EMAIL_PR]
@P_EMAIL NVARCHAR(30)
AS
BEGIN

SELECT ID,Created,Updated,UserCode,Name,Email,Password,BirthDate,Status
FROM TBL_User
WHERE UserCode = @P_EMAIL;
END
GO


