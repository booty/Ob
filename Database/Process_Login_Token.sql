USE [ob]
GO
/****** Object:  StoredProcedure [dbo].[Process_Login_Token]    Script Date: 06/28/2012 23:11:25 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO

/*
If ID_Member is supplied, then it's a FORCED login... just login that ID_Member no matter what
That is used for new account activation purposes

result codes
0=no error, login successful
1=too many login attempts
2=member banned
3=ip banned
4=member not activated
5=login info not found
*/

ALTER  PROCEDURE [dbo].[Process_Login_Token]
	@Login char(25) output,
	@Pword char(25),
	@ID_Member_Login_Method int,
	@URL varchar(100),
	@IP_Address char(15),
	@RecordLogin bit=1,
	@id_member int=null,
	@login_token varchar(36)=null
AS

--declare @id_member_status int
declare @permission_level int
declare @active_subscription int
declare @id_account_status int

--see if this IP address is banned
If Exists (select 1  from ip_address_banned where @ip_address like ip_address + '%')
	begin
	print 'ip banned'
	select result=3
	--return 3
	end

If (@login='Starfire' and getdate()<'6/11/2009') 
	begin
	print 'Temp ban'
	select result=99
	end


if @id_member is not null 
	begin
	print 'process_login_rs: id_member was supplied'
	end
else
	begin
	--if there's a login token supplied, log in using that.
	if @login_token=''
		begin
		print 'process_login_token: logging in via user/pass'
		--write this attempt to the login_attempt log
		Insert Into
			Login_Attempt
			(IP_Address, Login)
		Values
			(@IP_Address, @Login)
		
		--see if there have been too many attempts from this address (200 in past 24 hours)
		--if so, exit
		declare @attempts int
		select @attempts=sum(1) from login_attempt where ip_address=@ip_address and datediff(hh, timestamp_created, getdate()) < 48
		if @attempts > 400
			begin
			print 'too many login attempts'
			select 1 as result
			--return 1
			end
		
		--see if this member exists, get the permission level
		select @id_member=id_member from member where login=@login and password=@pword

		if @id_member is not null
			begin
			print 'Process_login_token: login via user/pass was successful, creating token'
			exec login_token_create @id_member=@id_member, @ip_address=@ip_address, @login_token=@login_token output
			end
		end
	else
		begin
		print 'process_login_token: logging in via token'
		exec login_token_check @login_token=@login_token, @ip_address=@ip_address, @id_member=@id_member output
		end
		
	end


if @id_member is null
	begin
	print 'not found'
	select result =5
	--return 5
	end

print 'process_login_rs: login successful'
exec member_permission_level @id_member, @permission_level output, null, null, null, null, @active_subscription output

SELECT
	ID_Account_Status,
	 id_member, 
	Login,
	Gender,
	m.id_member_status,
	m.id_member_type,
	member_type_description,
	member_status_description,
	cast(coalesce(permission_ads_approve,0) as bit) as permission_ads_approve,
	cast(coalesce(permission_forums_moderate,0) as bit) as permission_forums_moderate,
	cast(coalesce(permission_admin,0) as bit) as permission_admin,
	cast(coalesce(permission_uberadmin,0) as bit) as permission_uberadmin,
	cast(coalesce(permission_approve_pics_profiles,0) as bit) as permission_approve_pics_profiles,
	permission_csr,
	permission_csr_admin,
	Convert(int,	(Case
				when dbo.Age( dob, getdate() ) >= 18  then 1
				else 0 
				End)) as Adult,
	id_pirate_ninja,
	Likes_Males,
	Likes_Females,
	Age_Range_High,
	Age_Range_Low,
	activation_timestamp,
	email,
	last_login,
	zip,
	latitude,
	longitude,
	case
		when lifetime_member=1 then 9999
		when timestamp_membership_expiration>getdate() then datediff(dd,getdate(),timestamp_membership_expiration) 
		else 0
	end as days_remaining,
	mt.lifetime_member,
	case
		when m.id_member_status=1 then 2
		when activation_timestamp is null then 4
		else 0 
	end as result,
	@permission_level as permission_level,
	@active_subscription as active_subscription,
	last_login_previous,
	case
		when ability_item_slots_expiration>getdate() then 1
		else 0
	end as ability_item_slots,

	case
		when ability_anonymous_browse_expiration>getdate() then 1
		else 0
	end as ability_anonymous_browse,

	case
		when ability_see_fop_views_expiration>getdate() then 1
		else 0
	end as ability_see_fop_views,

	case
		when ability_see_moderation_expiration>getdate() then 1
		else 0
	end as ability_see_moderation,

	case
		when ability_temporary_mod_expiration>getdate() then 1
		else 0
	end as ability_temporary_mod,

	case
		when ability_comic_sans_insurance_expiration>getdate() then 1
		else 0
	end as ability_comic_sans_insurance,

	case
		when ability_comic_sans_expiration>getdate() then 1
		else 0
	end as ability_comic_sans,

	case
		when ability_secret_profile_fields_expiration>getdate() then 1
		else 0
	end as ability_secret_profile_fields,

	case
		when ability_picture_slots_expiration>getdate() then 1
		else 0
	end as ability_picture_slots,

	case
		when ability_cursed_item_immunity_expiration>getdate() then 1
		else 0
	end as ability_cursed_item_immunity,
	m.last_notification_seen,
	m.last_message_seen,
	@login_token as login_token

FROM
	Member m,
	Member_Type mt,
	Member_Status ms
WHERE
	M.id_member_type = mt.id_member_type
	AND m.id_member_status = ms.id_member_status
	--either there's a login/pw match, OR a @id_member was supplied for a forced login
	AND ((   m.Login = @Login  AND m.Password = @Pword )  or (id_member=@ID_Member) )


--was the shit not found?
if @@Rowcount=0
	begin
	print 'member not found'
	select 5 as result
	--return 5
	end

--BEGIN inactive member handling code
if exists ( select 1 from member where id_member_status=8 and id_member=@id_member)
	begin
	update member set id_member_status=0 where id_member=@id_member
	exec message_insert null, 1238, @id_member, 'Welcome Back!', '[template=welcomeback.html]', 0, 4, null, null, null
	
	end
--END inactive member handling code


--mark the member as active
exec member_last_active_update @id_member

IF @RecordLogin=1
	begin
	print 'process_login_rs: recording login'
	Insert Into
		Member_Login
		(ID_Member, ID_Member_Login_Method, ID_Account_Status, URL)
	Values
		(@ID_Member, @ID_Member_Login_Method, 1, @URL)
	Update
		Member
	Set
		Last_Login_Previous = Last_Login,
		Last_Login = getdate(),
		Last_Login_IP_Address = @IP_Address
	Where
		ID_Member = @ID_Member

	if @permission_level >= 2 
		begin
		print 'generating invites...'
		exec invite_create @id_member	--, @permission_level
		end
	END
