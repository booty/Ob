require 'gmail'

puts 'Logging in...'

Gmail.new('johnedmundrose@gmail.com', 'lweovmbuuexsrkez') do |gmail|
	puts 'Logged in!'

	gmail.peek = true
	mails = gmail.mailbox('[Gmail]/All Mail').emails(:to=>'johnedmundrose+bootycon@gmail.com')

	puts "Looks like there are #{mails.count} mails to process."
	regexp = /you'd like to know (?<name>.+) sent you \$(?<bucks>\d+)\./
	date_regexp = /Date:(?<date>.+)at/
	total_regs = 0

	mails.each { |mail| 
		if (mail.body.parts.count==0) then
			puts "This message (uid: #{mail.uid}) has only one part. It's probably not a purchase."
		else
			body = mail.body.parts[0].body.to_s
			matches = body.match(regexp)
			num_regs = matches[:bucks].to_i / 60
			date = Date.parse(body.scan(date_regexp)[0][0])
			total_regs += num_regs
			puts "gmail uid: #{mail.uid} date: #{date} name: #{matches[:name]} bucks: #{matches[:bucks]} number of people: #{num_regs} (total: #{total_regs} people)"
		end
	}
	puts "Okay, there were #{total_regs} total registrations found."
end

