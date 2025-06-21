-- SP para actualizar película
CREATE PROCEDURE [dbo].[UPD_MOVIE_PR]
    @P_Id int,
    @P_Title nvarchar(50),
    @P_Description nvarchar(100),
    @P_ReleaseDate datetime,
    @P_Genre nvarchar(50),
    @P_Director nvarchar(50)
AS
BEGIN
    UPDATE TBL_Movie 
    SET Updated = GETDATE(),
        Title = @P_Title,
        Description = @P_Description,
        ReleaseDate = @P_ReleaseDate,
        Genre = @P_Genre,
        Director = @P_Director
    WHERE Id = @P_Id;
END
GO
