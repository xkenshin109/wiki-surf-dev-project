USE [WikiSurf]
GO
/****** Object:  StoredProcedure [dbo].[S_INSERT_WIKI_SECTION]    Script Date: 2/20/2021 2:37:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[S_INSERT_WIKI_SECTION]
	-- Add the parameters for the stored procedure here
	@WikiPageId uniqueidentifier,
	@ToCLevel int,
	@Level varchar(max),
	@Line varchar(max),
	@Number varchar(max),
	@Index varchar(max),
	@FromTitle varchar(max),
	@ByteOffset int,
	@Anchor varchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @id uniqueidentifier

	select @id = WikiSectionsId
	from WikiSections
	where [Level] = @Level and @ToCLevel = ToCLevel and Line = @Line and Number = @Number and FromTitle = @FromTitle and Anchor = @Anchor 

	if(@id is null)
	begin
		set @id = NEWID()
		INSERT INTO WikiSections(
			WikiSectionsId,
			WikiPageId,
			ToCLevel,
			[Level],
			Line,
			Number,
			[Index],
			FromTitle,
			ByteOffset,
			Anchor)
		VALUES(
			@id,
			@WikiPageId,
			@ToCLevel,
			@Level,
			@Line,
			@Number,
			@Index,
			@FromTitle,
			@ByteOffset,
			@Anchor
		)
	end

	select * 
	from WikiSections 
	where WikiSectionsId = @id

END
