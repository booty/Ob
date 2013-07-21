require 'debugger'
require 'tiny_tds'


def psyche_eval

	case rand(1..3)
		when 1 
			s = "#{['Parented by', 'Raised by', 'Adopted by', 'Basically kind of hung out with'].sample} #{["robots", "loving parents", "monks", "wolves", "mongooses", "fairly normal parents", "some serious assholes"].sample} in the #{%W(tranquil peaceful idyllic affluent dangerous).sample} #{%W(swamps foothills slums suburbs).sample} of #{%W(Transylvania New\sYork Mexico\sCity Madagascar Tanzania an\saverage\sAmerican\ssmall\stown Hell\sitself Pittsburgh Dayton Seattle).sample}."
		when 2
			s = "#{%W(Abusive Loving Homeless Robot).sample} family provided a strict diet of #{%W(encouragement love daily\sswordfighting\slessons lima\sbeans popcorn organic\svegetables grain\salcohol cocaine unconditional\slove\sand\sacceptance...\sand old\smartial\sarts\smovies).sample} and #{%W(beatings church-going intellectual\sstimulation religion gin tough\slove comic\sbooks).sample}."
		when 3
			s = "Mother was #{["a travelling Gypsy", "a migrant worker", "a loving parent", "a sexy robot", "a secretary", "a famous CEO", "a good example of bio-engineering gone wrong", "a drunken brawler", "the kind of mom you wish you had", "the kind of 'earthy' lesbian that has huge arms and is really into the Indigo Girls"].sample}. Father was #{["a homeless man", "a brillian scientist", "a terrifying cyborg", "one hell of a guy -- had the kind of pecs you'd kill for", "a mild-mannered librarian", "an arms dealer", "a drug lord", "deeply in love with her", "prone to fits of melancholy", "the kind of guy who didn't merely shit the bed - he rolled around in it as well."].sample}."
	end


	case rand(1..3)
		when 1
			s << " #{%W(Not\sreally Greatly Somewhat Not\sall\sthat Deeply Profoundly).sample} #{%W(affected devastated aroused).sample} by the #{%W(death conversion\sto\sIslam painful\sgoiter\sloss cyborg\sconversion murder Kaiju\smurder).sample} of #{["family dog", "their hamster", "some shitty cat", "their favorite hobo lover", "father", "grandmother"].sample} #{["at the age of three", "at one really bizarre county fair", "while masturbating", "when all they wanted was some ice cream", "because it was their idea in the first place", "guys that really knew how to party, in their own way"].sample}."

			s << " At an early age, showed #{["no promise", "great aptitude", "much promise"].sample} for #{["playing the piano", "being a pickpocket", "being able to walk in a striahgt line", "being good at doing giant robot stuff", "not being a total fuck-up"].sample}, but struggled with with #{["discipline", "potty training", "watercolor painting", "basically everything else", "bubble farts", "chapped nipples", "controlling their emotions", "not slaughtering literally every living being they encountered", "drinking more than two or three beers without acting like an asshole", "getting thrown out of pet stores because of inappropriate touching"].sample}."
		when 2
			s << " At an early age, showed #{["no promise", "great aptitude", "much promise"].sample} for #{["playing the piano", "being a pickpocket", "being able to walk in a striahgt line", "being good at doing giant robot stuff", "not being a total fuck-up"].sample}, but struggled with with #{["discipline", "potty training", "watercolor painting", "basically everything else", "bubble farts", "chapped nipple", "boner control", "drinking more than two or three beers without acting like an asshole."].sample}."
		when 3
			s << " #{["Existence", "Life"].sample} #{["was transformed permanently", "didn't really change much", "threw a fucking curveball"].sample} when #{["parents", "goldfish", "friends", "imaginary friends", "nipples"].sample} were #{["eaten", "killed", "kidnapped", "fellated", "seduced"].sample} by #{["ninjas", "assassins", "an evil warlord", "jealous bitches", "their little brother", "creepy street artists", "giant robots, which is kind of a recurring theme here"].sample}."
	end


	s << " It was at the age of #{%W(5 6 19 23).sample} that they #{["reached puberty", "swore revenge", "dedicated themselves to vengeance", "discovered bathing", "had their entire body replaced by cyborg components", "had a sex change", "stopped being a huge pussy"].sample}."


	case rand(1..3)

	when 1
		s << " #{["Decided to spend", "Spent", "Reluctantly spent"].sample} the next several #{%W(hours years seconds).sample} #{["training their guts out", "battling for their life", "fighting for survival", "doing nothing", "masturbating", "sleeping", "trying to get laid", "getting their ass kicked", "fucking people up"].sample} in a #{%W(hidden secret seriously\sshady filthy).sample} #{["gay bath house", "robot repair shop", "abandoned rest stop", "temple", "dojo", "youth hostel", "homeless shelter", "toy store", "comic book store", "basement", "field", "alley", "warehouse", "waterfall"].sample}."
	when 2

		s << " #{%W(Soon Gradually Eventually As\stime\sprogressed).sample}, they #{%W(learned mastered totally\sfailed\sto\slearn\sunlocked).sample} the #{%W(secrets art techniques).sample} of #{["baking", "Hokuto Shinken", "martial arts", "flower arranging", "babysitting", "drinking", "mayhem", "oral hygeine", "sexually transmitted diseases", "flatulence", "sexual touching"].sample}."
	end 


	s << " Each #{%W(withering enchanting grueling sexual suprisingly\sfilthy joyful surpising erotic).sample} #{%W(moment day lesson fuck-up beat-down yeast\sinfection).sample} was a step #{["away from", "toward", "closer to"].sample} #{["the sensitive touching", "the ultimate revenge", "the true mastery of self-control", "being less of an asshole", "becoming a true sexual being, and was all", "the triumph", "the bloody satisfaction", "the kind of shit"].sample} that #{["their heart yearned for", "that their sense of honor demanded", "their parents expected from them", "most people would stay the fuck away from"].sample}."


	case rand(1..2)
	when 1 
		s << "\n\n#{["Was accepted to Harvard", "Became high school valedictorian", "Mentored by the greatest Jaeger pilot of all time", "Befriended a troupe of playful cats", "Actually managed to finally make make a friend", "Slept with a village of hobos", "Played first base for the Mets"].sample} despite #{["being completely blind", "excessive body odor", "a painful recurring prolapse", "a total lack of bowel control", "being addicted to blowjobs", "refusing to talk about anything about collecting Pokemon"].sample} because #{["of their immense determination", "having nothing better to do", "their mother forced them to", "of their burning passion to succeed", "sometimes life is crazy like that", "they were the one foretold by the prophecy", "people figured the world was ending anyway, so who cares?"].sample}."
	when 2
	s << "\n\n#{["Schooled", "Educated", "Truly blossomed", "Found themselves", "Unlocked their inner unicorn"].sample} #{["at Yale", "at Oxford", "at M.I.T.", "at Sweet Valley High", "at Stanford", "in some homeless guy's alleyway", "by the school of life", "in a secret Buddhist temple", "in a den of thieves", "on the streets", "at the school of hard knocks"].sample} where they studied #{["martial arts,", "forbidden knowledge,", "astrophysics,", "whatever the fuck they felt like studying,", "Eastern religion,", "some crazy advanced shit... I don't know, lasers or something...", "the best ways to get laid,", "their own genitals for hours on end,", "economics,", "hand-to-hand combat,", "pretty much anything their voracious mind could consume,"].sample} where it soon became apparent that #{["they were a once-in-a-lifetime talent", "they were a true genius", "absolutely nobody was ever going to give a shit about them", "they were the type of person that never gets invited to parties", "they were probably destined to be homeless", "they were literally drunk the entire time, twenty-four hours a day"].sample}."
	end 


	if rand(3)==0 then

		s << " With a fighting style that combined the #{%W(speed aroma spirit gassy\soutbursts sexual\sprowess lewdness tendency\stoward\sinappropriate\stouching).sample} of #{["a bull", "a seasoned expert", "a tiger", "the toughest son-of-a-bitch you ever met", "a drunken sailor", "a scampering ferret"].sample} with #{%W(depressingly\sinconsistent surprising unbeatable erotic never-before-seen senseless sensual frankly\sunnecessary sizzling drunkenly\sunpredictable).sample} #{%W(speed power levels\sof\sviolence sensuality passion brutality).sample}, termed \"#{%W(Elegant Ferocious Invincible).sample} #{%W(Monkey Tiger Elephant Bastard Falcon Armadillo Strapping\sFieldhand Hulk).sample} Style\", they soon gained the #{%W(attention praise erections sensitive\stouchings admiration).sample} of #{["their master", "some homeless guy", "perverts all over the world", "nudists", "pretentious assholes everywhere", "top-notch motherfuckers from around the world", "instructors", "hippies", "commanders", "pretty much everybody"].sample}."

	end 


	s << "\n\n#{["After becoming a Jaeger Pilot, their", "During one drunken night, they stole a Jaeger and their", "They soon joined the Robot Army and, after sucking and fucking their way into a Jaeger, their"].sample} #{["first", "debut"].sample} fight against a kaiju was a #{["limp-dicked", "fucked-up", "rousing", "disturbingly violent", "delightful", "splendid", "completely insane"].sample} #{["thing that legends are made of", "affair", "crowd-pleaser", "triumph", "success", "failure"].sample} despite #{["killing", "falling in love with", "impregnating", "infuriating", "\"accidentally\" killing", "annoying the shit out of"].sample} #{["their copilot", "their best friend", "their mother", "basically everybody", "a bunch of innocent bystanders", "nearly anybody that gave a shit", "the old lady down the block", "a bunch of nerds", "Whoopi Goldberg", "everybody in the Western Hemisphere"].sample}."

	case rand(1..2)

	when 1

		s << " #{["According to survivors", "Classified intel indicates", "Due to their actions", "Thanks to their efforts"].sample}, most of #{["the city", "the town"].sample} was #{["saved", "destroyed"].sample} and the rest was #{["burned to the ground", "covered in sexual fluids", "covered in blood", "consumed by Biebermania", "incinerated", "pretty happy about it", "pretty fucked-up to begin with so who cares really", "a goddamn mess", "full of ungrateful assholes"].sample}."

	when 2 
		s << " This was achieved despite #{["fighting", "running away from", "seriously fucking up"].sample} a Kaiju #{["with unmatched power and ferocity", "that would have made the toughest hombre you ever met shit his pants in three seconds flat", "that had pretty much no fucking clue at all", "with extremely low self-esteem", "that had absolutely zero friends", "that seemed to be addicted to heroin or something", "with an unexplained fondness for show tunes", "with some of the weakest bullshit attacks ever seen by mankind", "a code name of \"Ass Chuckle\" which meant nobody really took it seriously, despite the fact that it was actually pretty tough", "that had killed some pretty tough bastards in the past"].sample}."
	end 


	case rand(1..2)

	when 1 
		s << "\n\n#{["Following the", "In the aftermath of the", "When the dust settled after the"].sample} #{["heroic but sort of misguided", "grueling", "touching"].sample} #{["battle","Kaiju battle","clusterfuck"].sample}, pilot was #{["angrily", "immediately", "reluctantly"].sample} promoted to #{["Corporal", "General", "Chief Asskicker", "Head Badass", "bat boy for the Mets", "potato peeler, first class", "the general's dogwalker", "the newly-created title of Lord Not-To-Fucked-With"].sample} by #{["their commanding officer", "God himself", "their first grade teacher", "a particularly gentlemanly hobo who wondered that all the ruckus was about", "that guy nobody really talked to but apparently had the power to promote people, so whatever", "some motherfucker with a bunch of medals on his chest", "the President of what remained of the world", "pretty much the only other person that was still alive", "by popular demand", "a guy that wandered in off of the street and was pretty much just into promoting people"].sample}."

	when 2
		s << "\n\n#{["Deciding", "Refusing"].sample} to #{["finally show a regard for common sense or even reality itself", "rest on their laurels", "learn a lesson", "listen to everybody else for a change", "get a clue"].sample}, they #{["re-dedicated", "dedicated"].sample} themselve to #{["drinking a lot", "attempting to pilot their Jaeger sober", "curing AIDS", "partying like a true hero", "shaving their pubes", "becoming even more of a badass", "square dancing", "totally wussing out whenever possible", "opening their hearts to change", "embracing their inner sensuality", "becoming a sassy black woman", "experimenting with foreplay instead of jumping right into \"the good stuff\"", "essentially being a huge pussy"].sample} and #{["has been making some pretty good progress", "are basically doing alright", "and is, frankly, not having a lot of success", "have no fucking idea what they're doing", "as usual, are really shitting the bed", "are kicking ass at that, as we all expected"].sample}."
	end 

	case rand(1..3)

	when 1
		s << "\n\n\"The thing is,\" says #{["a homeless guy we spoke to", "a classmate", "their commander", "their best friend"].sample}, \"They'd be a lot #{["better","worse"].sample} at #{["piloting giant robots", "shit in general", "saving the Earth", "fucking up some fucking Kaiju", "something other than this", "everything", "not completely getting the shit kicked out of them", "embracing my love"].sample} if they #{["stopped being an asshole", "opened up their hearts", "started touching themselves a little more", "hugged a puppy once in a while", "took my dick out of their mouth once in a while"].sample}. #{["But that's what makes a good soldier, right?", "Ah, what are you going to do? Life is crazy like that.", "But ya gotta love that. This is the pilot I want on our side next time the Kaiju attack!"].sample}\""

	when 2
		s << "\n\n#{["Experts", "Haters", "Critics", "Jealous people", "So-called experts"].sample} have described their #{["robot piloting", "Jaeger tactics", "combat aptitude", "sensual kissing", "partying"].sample} style as \"#{["weak-ass", "embarrassing", "amateur", "second-rate"].sample} #{["bullshit", "weaksauce", "dick-slapping", "ass-grabbing"].sample}\", while #{["others have", "mostly everybody else has", "people who actually know what they're talking about have"].sample} described it as \"#{["awesome", "seriously cool", "completely dope", "devastatingly effective", "brutally effecient", "the kind of thing legends are made of", "instant boner material", "the kind of thing you jack off to", "basically their new religion"].sample}\" and \"some of the #{%W(coolest best toughest most\sheroic).sample} #{%W(mayhem stuff Jaeger\spiloting).sample} since #{["my dad", "Godzilla", "Mike Tyson", "Arnold Schwartzeneggar", "Gen. George Patton", "Bruce Lee", "gonhorrea"].sample} #{["ravaged", "invaded", "destroyed", "sexually dominated"].sample} #{["Barcelona", "people left and right", "my ass", "most of my hometown", "nipples everywhere", "all of my friends", "a bus-load of orphaned babies", "your mom", "my face"].sample}.\""
	when 3

		s << "\n\n\"#{["Son, you", "You", "Buddy", "Listen up, rookie", "People need to understand"].sample}, you can #{["read the textbooks", "study the training videos", "beat your dick to YouTube videos"].sample} #{["all you want","all day long"].sample},\" says one #{["commanding officer", "flight instructor", "homeless guy on a park bench"].sample}. \"But when you're actually out there #{["fighting", "with a dick in your ass", "trying to save the Earth", "fighting an unknown giant monster from another fucking dimension", "partying", "with the fate of the world in your hands"].sample}, #{["and half of your robot is on fire and the other half is severely damaged", "and you're completely drunk", "and you have an unexpected orgasm", "and you have a dick in your mouth", "and somebody offers you a beer"].sample} you better forget everything #{["you learned", "they taught you", "Mommy said", "you think you know"].sample} and #{["react", "adapt"].sample} to the #{["new", "life or death", "fucked-up", "crazy"].sample} situation #{["right in front of you", "even if you're pretty sure you're hallucinating", "no matter how high you are", "at hand"].sample}. If you don't, #{["the Earth is fucked", "somebody's going to get pregnant", "everybody's going to think you're a pussy", "basically everybody is going to die", "you're fucked", "you might as well be playing for the Mets"].sample}. #{["And trust me", "Believe me", "Trust me"].sample}, this is one #{["pilot", "soldier", "crazy asshole"].sample} who #{["totally gets", "understands", "has mastered", "will never have any clue about"].sample} that.\""
	end 

	s << "\n\nPsyche eval recommendation: #{["Promote immediately", "May be unfit for duty", "Can excel with a little love", "Needs more nipple touching", "Just wants to be loved", "Does not meet expectations", "A rare blend of sensuality and passion", "Will perform if given enough gummi bears", "Is gonna run this whole goddamn army someday", "Just needs cuddling", "Needs copious amounts of coddling", "Is probably a fucking maniac", "Could save us all someday"].sample}."

	s
end 




# Connect to the OB database
# Connect to database
puts "Opening database connection..."
begin
	client = TinyTds::Client.new(
		:username => 'otakubooty_scripts',
		:password => '3v9v8n39fn',
		:host => 'otakubooty.com',
		:port => '35005')
rescue 
	puts $!
	exit
end 
puts 'Connected to SQL Server.'


1.upto(500) do
	sql = "select top 1 id_jaeger, id_member from Member_Jaeger where PsycheEvaluation is null"
	results = client.execute(sql)
	member = results.to_a[0]

	sql = "update Member_Jaeger set psycheevaluation='#{psyche_eval.gsub("'","''")}' where id_jaeger=#{member['id_jaeger']} and id_member=#{member['id_member']}"

	puts "Member #{member['id_member']}"
	client.execute(sql).do
	
end 