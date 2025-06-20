USE [cenfocinemas-db]
GO

-- Stored Procedure para obtener una película por ID
CREATE PROCEDURE [dbo].[RET_MOVIE_BY_ID_PR]
    @P_Id int
AS
BEGIN
    SELECT Id, Created, Updated, Title, Description, ReleaseDate, Genre, Director
    FROM TBL_Movie
    WHERE Id = @P_Id;
END
GO