USE [WikiSurf]
GO
/****** Object:  StoredProcedure [dbo].[S_WORD_BANK_QUEUE_COMPLETE]    Script Date: 2/20/2021 2:36:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[S_WORD_BANK_QUEUE_COMPLETE] 
	-- Add the parameters for the stored procedure here
	@WordBankQueueId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE WordBankQueues SET
	IsProcessed = 1,
	ProcessedDate = GETDATE()
	where WordBankQueueId = @WordBankQueueId

	select 
		WordBankQueueId,
		wbq.WordBankId,
		IsProcessed,
		ProcessedDate,
		CreatedDate,
		wb.Exclude,
		wb.WordIndex
	from WordBankQueues wbq
	inner join WordBanks wb on wb.WordBankId = wbq.WordBankId
	where WordBankQueueId = @WordBankQueueId
END
