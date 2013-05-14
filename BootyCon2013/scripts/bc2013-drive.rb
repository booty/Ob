require 'rubygems'
require 'google_drive'
require 'tiny_tds'
require 'sequel'
require 'json'

#OUTPUT_PATH_FOR_JS_FILE = 'C:\inetpub\wwwroot\bootycon.org\www\js\registration-count2.js'
#OUTPUT_PATH_FOR_ASP_FILE = 'C:\inetpub\wwwroot\otakubooty.com\include\bootycon.asp'

OUTPUT_PATH_FOR_JS_FILE = 'registration-count2.js'
OUTPUT_PATH_FOR_ASP_FILE = 'bootycon.asp'


# Connect to the OB database
# Connect to database
puts "Opening database connection..."
begin
	client = TinyTds::Client.new(:username => 'otakubooty_scripts',:password => '3v9v8n39fn',:host => 'otakubooty.com',:port => '35005'	)
rescue 
	puts "Establishing database connection", "#{$!}"
	exit
end 
puts 'Connected to SQL Server.'

# Logs in.
# You can also use OAuth. See document of
# GoogleDrive.login_with_oauth for details.
puts 'Authenticating with Google...'
session = GoogleDrive.login('johnedmundrose@gmail.com','nlejzwrekuikmpmx')

# First worksheet of
# https://docs.google.com/spreadsheet/ccc?key=pz7XtlQC-PYx-jrVMJErTcg
puts "Getting worksheet..."
ws = session.spreadsheet_by_key("0Aj92QkDmSqctdGpZTFEyNlFKV1Zjb01BSlRNRWRsbUE").worksheets[0]

row = 3
ob_names = []
ob_id_members = []
registrations = []
spreadsheet_updated = false


while (ws[row,1]!='') and (row<100) do
	reg = {
		:PayPalName => ws[row,1], 
		:Login => ws[row,2],
		:DatePaid => ws[row,3],
		:IdMember => ws[row,4],
		:DateSavedToDatabase => ws[row,5]
	}

	registrations << reg 

	ob_names << reg[:Login]
	ob_id_members << reg[:IdMember]

	# look up id_member if none there
	puts reg
	if (reg[:IdMember].to_s.chomp=='') then 
		puts 'New registration!'
   	login = client.escape(reg[:Login])
		results = client.execute("select id_member from MemberLoginsAll where login='#{login}'")
		results_a = results.to_a
		results.cancel # clear the first pending result

		if results_a.length==0 then 
			puts "login '#{reg[:Login]}' not found in OB database"
		else
			# save it to OB's database
			#puts "#{reg[:Login]}... rows:#{results.affected_rows}"
			reg[:IdMember] = results_a[0]["id_member"]
			
			sql = "if not exists (select 1 from BootyCon_Registration where id_member=#{reg[:IdMember]})
			insert into BootyCon_Registration (id_member, bootycon_year, registration_date) values (#{client.escape(reg[:IdMember].to_s)}, 2013, '#{client.escape(reg[:DatePaid])}')"
			puts "#{reg[:Login]}... saving to database"  #{sql}"
			client.execute(sql).do

			# save it back to google
			ws[row,4] = reg[:IdMember]
			ws[row,5] = DateTime.now.to_s 

			spreadsheet_updateqd=true
		end
	end 
	row += 1
end

puts "Got #{registrations.length} registrations."

if spreadsheet_updated then
	puts 'Saving spreadsheet...'
	ws.save
	puts 'Saved.'
end

## write it to the ASP file (total attendees + list of all)
File.open(OUTPUT_PATH_FOR_ASP_FILE,'w') do |f|
	f.puts '<%'

	# array of logins
	ob_names.reverse! # list is oldest-first, let's make it newest-first
	s = "dim BootyconTotalRegs: BootyconTotalRegs=#{ob_names.length}\n"
	puts s
	f.puts s
	foo = ob_names.map { |k|
		"\"#{k.gsub('"','""')}\""
	}.join(",")
	s = "dim BootyconLogins: BootyconLogins = Array(#{foo})\n"
	puts s
	f.puts s

	# array of id_members
	ob_id_members.reverse!
	foo =  ob_id_members.map { |k|
		"\"#{k}\""
	}.join(",")
	s = "dim BootyconIdMembers: BootyconIdMembers = Array(#{foo})\n"
	puts s 
	f.puts s

	f.puts '%>'
end

## write it to the JS file (total attendees + list of all)
File.open(OUTPUT_PATH_FOR_JS_FILE,'w') do |f|
	 registrations.reverse! # make it newest-first
	 s = "var BootyconRegistrations=#{registrations.to_json};"
	 puts s 
	 f.puts s 
end
