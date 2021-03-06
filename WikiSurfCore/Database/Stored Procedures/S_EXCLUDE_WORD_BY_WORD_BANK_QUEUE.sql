USE [WikiSurf]
GO
/****** Object:  StoredProcedure [dbo].[S_EXCLUDE_WORD_BY_WORD_BANK_QUEUE]    Script Date: 2/20/2021 2:38:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[S_EXCLUDE_WORD_BY_WORD_BANK_QUEUE] 
	-- Add the parameters for the stored procedure here
	@WordBankQueueId uniqueidentifier,
	@SessionId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Declare @WordBankId uniqueidentifier

	SELECT @WordBankId = wb.WordBankId 
	from WordBanks wb
	inner join WordBankQueues wbq on wbq.WordBankId = wb.WordBankId
	where wbq.WordBankQueueId = @WordBankQueueId

	--Exclude word from being chosen. Dont like errors XD
	UPDATE WordBanks SET Exclude = 1
	where @WordBankId = WordBankId
	
	declare @newWordId uniqueidentifier, @newWord varchar(max)
	 
	--Retrieve a new word
	SELECT top 1 @newWordId = WordBankId, @newWord = Word
	FROM WordBanks 
	WHERE Exclude = 0
	order by NEWID()

	UPDATE WikiSessionWordbanks SET WordBankId = @newWordId
	where WikiSessionId = @SessionId AND WordBankId = @WordBankId

	--It has been processed ;)
	UPDATE WordBankQueues SET IsProcessed = 1, ProcessedDate = GETDATE(), WordBankId = @newWordId
	where @WordBankQueueId = @WordBankQueueId
	
	SELECT WordBankId, Word 
	from WordBanks where @newWordId = WordBankId
END
