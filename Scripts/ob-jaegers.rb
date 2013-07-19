require 'debugger'
require 'tiny_tds'

class JaegerGenerator

	@@adjectives = {
		'Standard' => %W(Rambunctious Golden Forlorn Somber Shining Brilliant Majestic Overwhelming Fertility Courageous Diligent Dynamic Spectacular Wondrous Amazing Incredible Unstoppable Feral Untamed Dynamite Crimson Undaunted Mysterious Fearless Stalwart Audacious Headstrong Erotic Cosmic Scampering Ginger Nimble Legendary Atomic Magnificent Robust Rotund Quivering Sexual Passionate Twitterpated Elongated Secret Surreal Spicy Austistic Lusty Drowsy Drunken Intoxicated Sneaky Creepy Broken Inappropriate Gassy Smothering Crippled Inflamed Flaming Seeping Oozing Buttered Unfettered Steampowered Steamy Depraved Erupting Raging Divine Supple Cursed Sweaty Terrible Pulsating Throbbing Tingle Resourceful),
		'M' => %W(Burly Masculine Swaggering Macho Muscular Virile Bearded Horrid Thundering Bellowing Festering Rumbling),
		'F' => %W(Rainbow Dainty Alluring Ravishing Moist Lactating Ovulating),
		'united kingdom' => %W(Royal Exceedingly\sPolite Eccentric Chivalrous Stuffy),
		'canada' => %W(Royal Exceedingly\sPolite Eccentric),
		'pa' => %W(Keystone),
		'ak' => %W(Kodiak Polar),
		'nj' => %W(Trashy)
	}

	@@nouns = {
		'Standard' => %W(Danger Whippersnapper Guardian Behemoth Minotaur Jellyfish Gorilla Scorpion Meatball Mayhem Seraphim Peril Excellence Blitzkrieg Robo Savior Lion Facepuncher Slasher Cutter Rocket Puncher Hero Ambition Courage Danger Rampage Shocker Avenger Revenger Goiter Blaster Tsunami Tornado Firestorm Tingle Carbuncle Dragon Brawler Slayer Hurricane Dynamite Punisher Asskicker Badass Ruffian Omega Violence Bingo Firestorm Infinity Hallelujah Blackjack Cupcake Sledgehammer Kraken Manticore Ruffian Defiance Majesty Emperor Juggernaut Blizzard Sunshine Typhoon Volcano Eruption Stalker Supernova Asteroid Falcon Hawk Cheetah Narwhal Meteor Comet Racer Vortex Wildebeest Bull Platypus Wombat Marmoset Lemur Kangaroo Ferret Stabber Sabretooth),
		'usa' => %W(Cowboy Liberty Freedom Pioneer Quarterback Thunder Hamburger Hotdog Patriot  Eagle),
		'united kingdom' => %W(Bulldog Tea-Time Sea\sPower Crumpetsnatcher),
		'canada' => %W(Maple\sLeaf Whiskey Slapshot),
		'japan' => %W(Sushi Tokyo Kansai Samurai ),
		'australia' => %W(Kangaroo Dingo Eucalyptus Koala),
		'germany' => %W(Lederhosen),
		'philadelphia' => %W(Cheesesteak Phanatic Rocky Quaker Pretzel Keystone),
		'new york' => %W(Empire Gotham Bronx Broadway Rockefeller Yankee),
		'los angeles' => %W(Hollywood Angel Dodger Earthquake Sunset),
		'chicago' => %W(Blackhawk Gangster Gangbuster South\wSide North\sSide Wrigley Hogbutcher Chitown),
		'atlanta' => %W(),
		'san diego' => %W(Raven Oriole),
		'baltimore' => %W(Mega-Hon),
		'austin' => %W(),
		'houston' => %W(),
		'columbus' => %W(),
		'boston' => %W(Chowder),
		'tx' => %W(Cowboy Ranger Cactus Tumbleweed),
		'fl' => %W(Gator Swampland Everglade),
		'md' => %W(Crabcake),
		'va' => %W(Charleston Chesapeake),
		'nj' => %W(Guido),
		'M' => %W(Beefcake Hunk Lumberjack Stallion Erection Bastard),
		'F' => %W(Valkyrie Maiden Princess Amazon Bloomer Passion Feather Mermaid)
	}

	@@body_parts = {
		'M' => %W{Penis Mustache},
		'F' => %W{Womb Labia Breast},
		'Standard' => %W{Ear Shoulder Nipple Butt Arm Foot Head Groin Fist Elbow Taint}
	}

	@@animals = %W(Tiger Shark Bumblebee Hornet Panda Dolphin Cheetah Leopard Walrus Cobra Scorpion Lion Leopard Manticore Dragon Devil Hawk Eagle Raptor Hummingbird Bison Barracuda)

	@@quantities = %W(Double Triple Quadruple Sixty\sFour Thousand Hundred Million Billion)

	@@weapon_modifiers = %W(Rocket Multiple Breathtaking Wrathful Electric Antimatter Titanium Torrential Finishing Fatal Divine Poisonous Toxic Venomous Secret Affectionate Crushing Unhealthy Detrimental Killer Murderous Adorable Aggressive Sloppy Ill-Advised Majestic Violent Heavenly Critical Invisible Laser Flaming Sexy Nuclear Ancient Three-Way Overly-Elaborate Elaborate Predictable Confusing Ineffective Inefficient)
	@@weapon_suffixes = %W{Whirlwind Storm Flood Inferno Assault Barrage Technique Attack Festival Spasm Seizure Penetation Eruption Raid Volley Formation Overload Thrust Tantrum Armageddeon Orgy }
	@@weapon_nouns = %W(Knife Battle-Axe Sword Arrow Dagger Boomerang Cutlass Halberd Staff Baseball\sBat Dildo Club Cricket\sBat Hockey\sStick Gauntlet Blaster Beam Kick Punch Whip Cudgel Knuckles Missiles Cannon Chainsaw Shield Bomb Black\sHole\sGenerator Slicer Broadsword Laser Machine\sGun Machete Hammer Mallet Flamethrower Scissor Needle Crossbow Frying\sPan Soup\sLadle)
	@@weapon_verbs = %W(Kick Bash Punch Stomp Choke-Hold Chop Pound Headbutt Suplex Dropkick Suckerpunch Caress Uppercut Haymaker Strike Blast Bodyslam Nippletwister Wedgie Bodycheck Tackle Jump-Kick)
	@@weapon_emotions = %W(Justice Happiness Vengeance Knowledge Retribution Blood Oblivion Doom Suffering Orgasmic\sBliss Satisfaction Nothingness Salvation Revenge Wrath Darkness Despair Joyfulness Playfulness Enlightenment Sexuality Judgement Madness Destruction Devastation Damnation Confusion Luck Desperation)

	def new()
		@@nouns['us'] = @@nouns['usa']
		@@nouns['ca'] = @@nouns['canada']
		@@nouns['au'] = @@nouns['usa']
	end 

	def get_ability() 


	end 

	def get_weapon(gender) 
		case rand(9)
		when 0 
			w = "#{@@weapon_modifiers.sample} #{@@weapon_nouns.sample}" 
		when 1
			w = "#{@@body_parts[gender].sample}-Mounted #{@@weapon_nouns.sample}" 
		when 2
			w = "#{@@adjectives['Standard'].sample} #{@@weapon_verbs.sample}"
		when 3
			w = "#{@@adjectives['Standard'].sample} #{@@weapon_nouns.sample}"
		when 4
			w = "#{@@body_parts['Standard'].sample}-Mounted #{@@weapon_nouns.sample}"
		when 5
			w = "#{@@quantities.sample}-#{@@weapon_verbs.sample} #{@@weapon_suffixes.sample}"
		when 6 
			w = "#{@@animals.sample}'s #{(@@weapon_nouns+@@weapon_verbs).sample}"
		when 7 
			w = "#{@@adjectives['Standard'].sample} #{@@body_parts['Standard'].sample} #{@@weapon_verbs.sample}"
		when 8 
			w = "#{@@weapon_emotions.sample} #{@@weapon_verbs.sample}" 
		end
		w += " #{@@weapon_suffixes.sample}" if rand(5)==0
		w += " of #{@@weapon_emotions.sample}" if rand(5)==0
		w
	end 

	def get_name(gender, city, state, country) 
		city ||= ''
		city.downcase!
		state ||=''
		state.downcase!
		country ||=''
		country.downcase!

		allowed_adjectives = @@adjectives[gender].clone 
		allowed_adjectives += @@nouns[country.downcase] if @@nouns[country.downcase]
		if allowed_adjectives.length==0 
			allowed_adjectives = @@adjectives['Standard']
		else 
			allowed_adjectives += @@adjectives['Standard'].sample(allowed_adjectives.length * 2)
		end

		allowed_nouns = @@nouns[gender].clone
		
		allowed_nouns += @@nouns[city.downcase] if @@nouns[city.downcase]
		allowed_nouns += @@nouns[city.downcase] if @@nouns[city.downcase]
		allowed_nouns += @@nouns[city.downcase] if @@nouns[city.downcase]
		allowed_nouns += @@nouns[country.downcase] if @@nouns[country.downcase]
		allowed_nouns += @@nouns[state.upcase] if @@nouns[state.upcase]
		if allowed_nouns.length==0
			allowed_nouns = @@nouns['Standard']
		else 
			allowed_nouns += @@nouns['Standard'].sample(allowed_nouns.length * 2)
		end 

		#puts "Choosing noun from: #{allowed_nouns}"
		if rand(3)==0
			jaeger = "#{allowed_nouns.sample} #{allowed_nouns.sample}"	
		else 
			jaeger = "#{allowed_adjectives.sample} #{allowed_nouns.sample}"
	 	end 
		jaeger.upcase  #+ " (#{gender}, #{city}, #{state}, #{country})"
	end
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


jaeger_gen = JaegerGenerator.new 


1.upto(50) do

	# get a paid member with no jaeger

	sql = "select top 1 id_member, city, state, country, gender, login from member where (timestamp_membership_expiration>getdate() or id_member_type in (6,7,3))  and id_member not in (select id_member from Member_Jaeger) order by id_member desc"
	results = client.execute(sql)
	member  = results.to_a[0]

	jaeger_name = jaeger_gen.get_name(member['gender'], member['city'], member['state'], member['country'])
	ability1 = jaeger_gen.get_weapon(member['gender'])
	ability2 = jaeger_gen.get_weapon(member['gender'])

	sql = "exec JaegerCreate #{member['id_member']}, null, '#{jaeger_name}', '#{ability1.gsub("'","''")}', '#{ability2.gsub("'","''")}', ''"
	#puts sql
	puts "#{member['login']} is being assigned to #{jaeger_name} which uses #{ability1} and #{ability2}"

	client.execute(sql).do

end


=begin
puts "\n== JAEGER NAMES =="
puts jaeger_gen.get_name('M','Philadelphia','PA','USA')
puts jaeger_gen.get_name('F','Philadelphia','PA','USA')
puts jaeger_gen.get_name('F','Houston','TX','USA')
puts jaeger_gen.get_name('F','Chicago','IL', 'USA')
puts jaeger_gen.get_name('M','London','England','United Kingdom')

puts "\n== JAEGER WEAPONS/ABILITIES =="
puts jaeger_gen.get_weapon('M')
puts jaeger_gen.get_weapon('M')
puts jaeger_gen.get_weapon('M')
puts jaeger_gen.get_weapon('F')
puts jaeger_gen.get_weapon('F')
=end


