USE [ob]
GO
/****** Object:  UserDefinedFunction [dbo].[Messages]    Script Date: 10/24/2012 20:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[MessagesPagedFrom](
	@id_member_From int, 
	@skip int=0,
	@take int=99999 )

RETURNS TABLE 
AS
RETURN 
(

select 
	mesPage.*,
	mTo.login,
	mTo.id_picture_member as id_picture_member
from 
(
select
	ROW_NUMBER() over (order by id_message desc) as row_number,
	mes.id_message,
	mes.subject,
	mes.body,
	mes.read_count,
	mes.timestamp_created,
	dbo.datediffex(mes.timestamp_Created, GETDATE()) as message_age,
	mes.timestamp_read,
	dbo.datediffex(mes.timestamp_read, GETDATE()) as read_age,
	mes.delete_sender,
	mes.Delete_Recipient,
	mes.ID_Member_To,
	mes.ID_Message_Reply_To
from 
	message mes
where
	mes.ID_Member_From=@id_member_From
) mesPage
	inner join member mTo on 
			mesPage.id_member_to = mTo.id_member

 where 
	row_number between @skip+1 and @take+@skip
		
	
	--and (@unread_only=0 or timestamp_read is null)
)

go

select * from dbo.MessagesPagedFrom(1238, 1, 10) order by id_message desc

