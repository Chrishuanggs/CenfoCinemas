USE [cenfocinemas-db]
GO

-- Stored Procedure para obtener un usuario por ID
CREATE PROCEDURE [dbo].[RET_USER_BY_ID_PR]
    @P_Id int
AS
BEGIN
    SELECT Id, Created, Updated, UserCode, Name, Email, Password, BirthDate, Status
    FROM TBL_User
    WHERE Id = @P_Id;
END
GO