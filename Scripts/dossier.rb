require 'debugger'
require 'tiny_tds'

class DossierGenerator
	@@support_preamble = ["Job is to support", "Primary role is supporting", "Likes to help", "Avoids direct combat and prefers to support"]
	@@attack_preamle = ["Role is", "Job is", "Primary function is"]
	@@attack_type = %W(overwhelming strategic surgical furious sporadic intense lightning-like)
	@@attack_type2 = %W(attack destroy pulverize confuse upset embarrass annihilate disable insult)
	@@attack_whatever = %W(unleash supply perform)
	@@support_functions = %W(augmenting multiplying fortifying improving upgrading)
	@@support_target_jaeger = %W(defensive offensive weapons long-range\sdetection offensive sexual party sensual)
	@@support_target_capabilities = %W(capabilities power)
	@@support_type = %W(mid-air underwater deep-space on-the-fly)
	@@support_action = %W(counseling\sservices blowjobs massages tax\sadvice repair\sservices)
	@@support_adjective = %W(helpful courageous much-needed ferocious perilous suicidally-insane tricky)
	@@support_targets2 = %W(friendly\sforces other\sJaegers other\sJaeger\spilots civilians soldiers basically\sanybody\swho\sasks)
	@@battle_conditions = %W(confusion mayhem melancholy frenzy heartbreak)
	@@battle_types = %W(sexual night-time high-speed covert undercover full-force desperate)
	@@battle_types2 = %W(combat brawls battles lovemaking skirmishes)
	@@secrets = %W(Top-secret Classified)
	@@rumors = %W(rumors whispers scuttlebutt)
	@@rumor_types = %W(an\sexperimental a\sgame-changing a\sfucked-up an\sunbelievable)
	@@rumor_types2 = %W(new secret ancient truly\sastonishing)
	@@weapon_types = ["nuclear weapon", "aggressive handjob technique", "doomsday device", "atomic missile", "fried chicken recipe", "boner pill", "concentrated energy blast", "violent sexual onslaught"]
	@@weapon_purpose = %W(end hopefully\send prolong ensure decisively\send)
	@@weapon_intent = %W(struggle\sagainst\sKaiju inflation the\sdesignated\shitter\srule Obamacare this\swar\sfor\shuman\ssurvival psoraisis erectile\sdysfunction)
	@@strengths_or_weaknesses = %W(alcohol party\sdrugs melancholy expressing\sits\sfeelings harsh\scriticism emotions bad\sadvice)
	@@strengths_or_weaknesses_adjectives = %W(ice-based fire-based violent gentle sensitive)
	@@strengths_or_weaknesses_nouns = %W(attacks Kaiju caresses enemies enemy\sattacks)
	@@pilot_tendencies = %W(heroic drunken fun-loving seriously\sbad-ass terrifying admirable erotic smoking\shot)
	@@times = %W(In\sthe\spast At\stimes Sometimes Often Frequently)
	@@perhaps = %W(Perhaps As\swe\sall\sknow Certainly We\scan\ssafely\ssay)
	@@famous = %W(famous infamous heroic questionable legendary)
	@@operations_prefix = %W(Secret Erotic Rainbow Heroic Unyielding Frisky Flagrant Final Magical Highway Unstoppable Resilient Defiant Stalwart)
	@@operations = %W(Hercules Hogwarts Sword Cumberbatch Diddler Eruption Rumble Nightmare Freedom Ragnarok Asshole Derelict Magical Unicorn Asskicking Storm Rimjob Panty\sRaid)
	@@operation_quantities = ["several hundred", "several thousand", "countless", "many", "two or three", "a whole bunch of", "nearly all of the Earth's"]
	@@operation_recipients_good = ["orphans", "children", "homeless drunks", "Shake Shacks", "abortion clinics", "unwed mothers", "cancer patients", "schoolchildren", "psychopaths", "condemned criminals", "innocent bystanders", "kittens", "puppies", "leading scientists"]
	@@operation_recipients_bad = ["a fellow Jaeger pilot", "a small amount of time", "a single flower", "some dude's record collection", "money on car insurance", "its reputation", "this one guy who turned out to not even really be that important", "the oldest remaining public library in Topeka", "this one guy nobody even likes", "a couple of beat-up Chevy Malibus", "several terabytes of pornography"]
	@@places = %W(Tokyo New\sYork Los\sAngeles Sydney London Paris Topeka Texas Mexico Brazil Antarctica Philadelphia Washington\sD.C. Baltimore Atlanta Citizen's\sBank\sPark Yankee\sStadium Chicago)
	@@people = ["other Jaeger pilots", "commanding officers", "the pilots' parents", "homeless men", "media pundits", "spiritual advisors", "experienced Jaeger pilots"]
	@@skill_relations = ["wished that the pilots would improve their", "envied the pilots'", "complimented the pilots'", "expressed concern over the pilots'", "heaped praise upon the pilots'", "been awestruck by the pilots'"]
	@@skills = ["oral hygiene","sexual health","lovemaking abilities","intensity","dedication to partying","general cleanliness","dedication to asskicking","alcohol tolerance","choice of personal grooming habits","ability to chug a six-pack","masturbation habits","combat effectivness"]
	@@battle_hardships = ["absorbing huge amounts of damage", "being slightly buzzed", "being completely drunk", "being half-asleep", "facing attack from all angles", "confronting multiple Kaiju", "still thinking about that blow job from last night", "feeling exhausted", "looking like fucking morons"]
	@@battle_abilities = ["look really cool", "perform handjobs", "unleash devastating attacks", "protect their friends", "maintain an emotionless expression", "yell like a motherfucker", "remain calm", "party", "show no signs of last night's anal pounding", "boost the morale of everybody around them", "provide inspirational speeches", "fuck shit up"]
	@@attack_ing = %W(pulverizing destroying fucking\sup murdering slaughtering fingerbanging fucking\sup speed-humping)
	@@kaiju_targets = %W(brains faces limbs feelings emotions genitals reproductive\sorgans assholes friends children heads)
	@@operation_objectives = ["end this war", "capture a living Kaiju", "relieve boredom", " ...well, for no apparent reason, but it looked really cool", "blow off some steam", "get laid", "figure out what all of the buttons on the Jaeger's control panel actually did"]
	@@meaningless_preambles = ["One thing is certain:", "Make no doubt about it:", "Everybody agrees:"]
	@@purposes = ["have one purpose, which is to", "live to", "like nothing better than to", "have an unequaled ability to"]
	@@attack = %W(crush kick punch fingerbang break smash destroy eradicate piss\sall\sover take\sa\shuge\sshit\son explode aggressively\sfuck)
	@@pilot_relations = ["who have been friends since childhood","who are simmering with sexual tension","who everybody is pretty sure are direct blood relatives","who are bitter ex-lovers","who nobody has ever fucking heard of before"]

	def dossier
		d = ""

		case rand(4)
			when 0
				d = "#{@@support_preamble.sample} #{@@support_targets2.sample} by #{@@support_functions.sample} their #{@@support_target_jaeger.sample} #{@@support_target_capabilities.sample}." 
			when 1 
				d = "#{@@attack_preamle.sample} to #{@@attack_whatever.sample} #{@@attack_type.sample} offense #{%W(intended designed).sample} to #{@@attack_type2.sample} the Kaiju by #{@@attack_ing.sample} their #{@@kaiju_targets.sample}."
			when 2
				d = "#{@@attack_preamle.sample} is to #{@@attack_type2.sample} Kaiju and supply #{@@support_adjective.sample} #{@@support_action.sample} to #{@@support_targets2.sample}."
			when 3 
				d = "#{@@attack_preamle.sample} is to supply #{@@support_adjective.sample} #{@@support_action.sample} to #{@@support_targets2.sample}, even during the #{@@battle_conditions.sample} of #{@@battle_types.sample} #{@@battle_types2.sample}."
		end 

		case rand(3)
			when 0 
				d += " #{@@secrets.sample} #{@@rumors.sample} indicates this Jaeger is part of Operation #{@@operations_prefix.sample} #{@@operations.sample}, a plan to use #{@@rumor_types.sample}, #{@@rumor_types2.sample} #{@@weapon_types.sample} to #{@@weapon_purpose.sample} #{@@weapon_intent.sample}."
		end


		case rand(8)
			when 0
				d += " May be vulnerable to #{@@strengths_or_weaknesses.sample}."
			when 1 
				d += " Impervious to #{@@strengths_or_weaknesses.sample}."
			when 2
				d += " Cannot be damaged by #{@@strengths_or_weaknesses_adjectives.sample} #{@@strengths_or_weaknesses_nouns.sample}."
		end 
	


		case rand(5)
			when 0 
				d += " During the Battle Of #{@@places.sample}, pilots displayed an unprecedented ability to #{@@battle_abilities.sample} while simultaneously displaying outstanding #{@@skills.sample}."
			when 1
				d += " #{@@perhaps.sample} that one of its most #{%w(controversial celebrated infamous famous).sample} moments of the war occured during Operation #{@@operations_prefix.sample} #{@@operations.sample} when the pilots sacrificed #{@@operation_quantities.sample} #{@@operation_recipients_good.sample} in order to save #{@@operation_recipients_bad.sample}."
			when 3
				d += " #{@@perhaps.sample} historians will eventually #{%W(salute forgive celebrate).sample} the pilots for their role in Operation #{@@operations_prefix.sample} #{@@operations.sample}, during which most of #{@@places.sample} was destroyed in an effort to #{@@operation_objectives.sample}."
		end


		case rand(3)
			when 0 
				d += " #{@@times.sample}, #{@@people.sample} have #{@@skill_relations.sample} #{@@skills.sample}."
		end 

		case rand(3)
			when 0
				d += " One #{["known","rumored","inconvenient"].sample} drawback of this Jaeger type is that #{["it may","it tends to","under very rare circumstances it will"].sample} #{["explode randomly, killing everybody inside,", "cause incurable cancer", "act like a total asshole","perform like a real pussy","blind everybody in a one-mile radius, pretty much ruining their lives,", "really bum you out"].sample} #{["every so often","pretty much any time you even look at it","just when the pilots are starting to have fun","if either pilot is Mexican, which is really racist if you think about it","if the pilots don't really have their shit together","unless both pilots have been circumcised by the same doctors"].sample}."
		end 


		case rand(3)
			when 0 
				d += " #{@@meaningless_preambles.sample} these pilots are #{@@pilot_tendencies.sample} and #{@@purposes.sample} #{@@attack.sample} some Kaiju #{@@kaiju_targets.sample}."
			when 1 
				d += " #{@@meaningless_preambles.sample} these pilots are #{@@pilot_tendencies.sample} and nobody questions their dedication to #{@@skills.sample}."
			when 2
				d += " The only question facing these pilots, #{@@pilot_relations.sample}, is this: can they #{@@attack.sample} the Kaiju without #{@@attack_ing.sample} each other first?"
		end 

		d
	end 


	@@colors = %W(gold silver purple blue green orange yellow teal)
	@@color_applications = %W(details highlights accents)
	@@color_adjectives = %W(shimmering matte glossy)
	@@inspirations = ["a Russian tank", "the most bad-ass Mustang you've ever seen", "", "an exotic race car", ""]

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





dossier = DossierGenerator.new

1.upto(500) do
	sql = "select top 1 id_jaeger from Jaeger where dossier is null"
	results = client.execute(sql)
	member = results.to_a[0]

	print dossier.dossier
	sql = "update Jaeger set dossier='#{dossier.dossier.gsub("'","''")}' where id_jaeger=#{member['id_jaeger']}"

	client.execute(sql).do
	puts ' ...done'
end 