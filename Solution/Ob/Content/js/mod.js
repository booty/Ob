function modTemplateReplace(sourceText, idPost, tabNumber, idPostModerate, pageNumber, modLog)  {
	var result = new String(sourceText);
	result = replaceSubstring(result,'[ID_POST]',idPost);
	result = replaceSubstring(result,'[TAB]', tabNumber);
	result = replaceSubstring(result,'[PAGE]', pageNumber);
	result = replaceSubstring(result,'[ID_POST_MODERATE]', idPostModerate);
	result = replaceSubstring(result,'[MOD_LOG]', modLog);
	return result;
}

if (typeof ob_mod_template=='undefined') {
	// 
	var ob_mod_template = '<table><tr><td valign="top" width="38%" style="font-size:7pt; padding-left:4px; background:url(images/forum/grad_green.jpg)"><h3 style=""background-color:white; margin:3px; padding:1px; text-align:center"">Positive Flags</h3>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=22">!!!</a><br>';
	ob_mod_template += 'Achewood: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=317"> GOF</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=320"> Old School</a><br>';
	ob_mod_template += 'Achewood: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=323"> Special (Guitar)</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=322"> Special (Shoes)</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=91">Aim for the Top</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=23">Badass</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=130"> Grabass</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=35">Booty Approves</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=125">Bonk Approves</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=110">Capital Posting</a><br>';
	ob_mod_template += 'Chicken Dance: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=341"> Dining</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=325"> Office</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=329"> Yard</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=24">Cookin\'</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=34">Comedy Cold</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=100">Daaaaaamn</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=195">Destroyed 1 </a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=196">2</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=49">Devastated</a><br>';
	ob_mod_template += 'Disgaea: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=219">Passed</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=283">Digstroyed</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=200">Done</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=14">Droppin\' Knowledge #1</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=77">#2</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=132"> #3</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=52">Evidence of Funny</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=113">Fierce</a><br>';
	ob_mod_template += 'Firefly: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=89">My Bunk</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=169">Cunning</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=170">Going Mad</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=107">Fucking Metal</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=129"> Fullmetal Alcoholic</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=93">Geek Cred</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=26">Gleaming</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=11">Great Posting, Ace</a><br>';
	ob_mod_template += 'GGX: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=185">Johnny</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=186">Baiken</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=187">Chipp</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=188">Ky</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=189">Millia</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=190">Robo Ky</a><br>';
	ob_mod_template += 'GGX: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=191">Slayer #1</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=192">#2</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=193">#3</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=194">Sol #1</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=204">#2</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=206">Test.</a> / ';
	// 
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=208">Jam</a><br>';
	ob_mod_template += 'He\'ll... <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=166">Save</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=277">Rape</a> Every One Of Us<br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=154">Hockey Guys</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=155">#2</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=156">#3</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=157">#4</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=158">#5</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=159">#6</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=160">#7</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=313"> #8</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=349"> #9</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=350"> #10</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=37">How Awesome?</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=104">Awesome?</a> <br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=84">Hummina Hummina</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=108">I\'d Hit It...</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=131">I Love It</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=65">Impressive Posting</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=149">Jawsome<br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=167">Legendary</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=71">LOLercoaster</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=310"> Ming Applauds</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=301"> Mission Accomplished</a><br>';
	ob_mod_template += 'NBA Jam <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=145">On Fire</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=148">Boomshaka...</a><br>';
	ob_mod_template += 'LOL: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=10">So Funny</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=288">Zelda</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=39">No You Did Not</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=64">Now That\'s Science</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=67">On a Roll</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=66">Parodactyl</a><br>';
	ob_mod_template += 'Perfect Win: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=223"> Ryu</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=210"> Dan</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=20">Posting Superstar</a><br>';
	ob_mod_template += 'Puke <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=178">One</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=281">Two</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=171">Punisher</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=298"> Rad</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=116">Reincarnated</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=69">Serious Business</a><br>';
	ob_mod_template += 'Sexy: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=315"> Girlcock</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=296"> Panties</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=290"> Sweet Tacos</a><br>';
	ob_mod_template += 'Splooge: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=339"> Orig</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=343"> Trans/L</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=344"> Trans/R</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=345"> Huge</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=42">Shamon</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=216"> Stellar Posting</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=70">Super Poster Squad</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=72">Super Poster In Training</a><br>';
	ob_mod_template += 'That\'s A Fact: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=295"> Violence_Jack</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=311"> Jack Bauer</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=118">This Is My Bag</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=168">This Thread Is Now Awesome</a><br>';
	ob_mod_template += 'Time For: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=179">Hookers and Gin / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=222">Lesbos+PBR</a><br>';
	ob_mod_template += 'Time For: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=285">Jugwine+Hobos / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=286">Natty+Bro<br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=106">True American Hero</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=99">Truly Outrageous</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=221"> Unleash the FOPs</a><br>';
	ob_mod_template += 'Venture Bros: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=309"> Epic</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=316"> Team Venture</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=308"> Knife</a><br>';
	ob_mod_template += 'Venture Bros: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=302"> Punch</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=307"> Fire</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=303"> Team Booty</a><br>';
	
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=50">Robo Babies</a><br>';
	ob_mod_template += 'ROFLcopters: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=87">Original</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=88">Shotgun!</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=138">Whoa!</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=324"> Word Up</a><br>';
	ob_mod_template += 'XTREME: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=15">Extreme Posting</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=218"> Radical</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=214"> You Stay Classy!</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=134">Zelda: Roofies</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=135"> Dong</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=136"> Hello Kitty</a><br>';
	
	ob_mod_template += '<h3 style=""margin:3px; padding:1px; text-align:center"">Audio</h3>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=336">SMB (Power Up)</a><br>';
	
	ob_mod_template += '</td>';
	
	ob_mod_template += '<td valign="top" width="38%" style="font-size:7pt; padding-left:4px; background:url(images/forum/grad_orange.jpg)"><h3 style=""margin:3px; padding:1px; text-align:center"">Negative/Misc. Flags</h3>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=17">..!?</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=119">Acid Burned</a><br>';
	ob_mod_template += 'Akira: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=326"> Bike</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=327"> Shot</a><br>';
	ob_mod_template += 'Arrested: '
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=287">Envelope</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=291"> Feather</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=312"> Grief</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=294"> Leather</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=340"> Brain Explodes</a><br>';
	ob_mod_template += 'Bridget: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=180">#1</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=181">#2</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=182">#3</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=184">#4</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=44">Burned 1</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=86">Burned 2</a><br>';
	ob_mod_template += 'Captain Obvious: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=112">Common</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=111">Good Job</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=103">Chow, Bitches</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=199">Clowned</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=146">Creepy Stalker Approves</a><br>';
	ob_mod_template += 'Dead Horse: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=220">Just A Horse</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=314"> Animated</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=126">Delaware\'d</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=164">Denied</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=197">May 1</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=198">May 2</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=202">Don\'t Try It (Nomad)</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=165">Don\'t Worry, Mama Will...</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=96">Drama Bomb</a>/ ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=97">Stop</a> /';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=98">Grow Up</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=163">Freak! The Mundanes</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=127"> Enter, Motherfucker...</a><br>';
	ob_mod_template += 'Euro Playfull: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=36">#1</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=54">#2</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=201">#3</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=122">Exiled to Free Chat</a><br>';
	ob_mod_template += 'Fist: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=176">Already Dead</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=347">  Already Gay</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=217"> RATATAT</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=280">For Us, Any Exchange With Girls..</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=55">Get Over Here - On Topic</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=282">Get Up To Speed</a><br>';
	ob_mod_template += 'Goodbye: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=57">Razor</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=57">You Lame Fuck</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=279">Google It</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=115">Gorg</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=121">Gorn Half Savage</a> /';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=120">No text</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=141">Hero - Fade To Red</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=117">I Have Sex!</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=212"> I Do Cocaine!</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=213"> I Recommend Literacy</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=151">In Denial</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=59">It Lives</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=162">I See What You Did There</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=203">Less Yappin\'...</a><br>';
	ob_mod_template += 'Mod Fav: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=60">Sunset</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=61">Biz Suits</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=215">Fuckin\'</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=304"> Venture</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=177">Neeerddds</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=75">No Baseball</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=105">No Baseball</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=224">No Shit, Sherlock</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=144">Not Gay At All</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=40">OMG Yay For You</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=29">Philosophisin\'</a><br>';
	ob_mod_template += 'Phoenix Wright: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=172">Objection!</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=173">Miles</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=174">Hold It</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=226"> Pot, Kettle, Black</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=209"> With Text</a><br>';
	ob_mod_template += 'Plane: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=143">Nice Job</a> /';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=142">Well That Worked</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=128">Read Rule #1</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=114">Read The Thread</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=6">Retarded</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=68">Sabotaged</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=124">Secret Is Learn To Spell</a><br>';
	ob_mod_template += 'Shut Up, Starfire: <a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=51">Church</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=225"> Spider</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=102">Shut Up About Ani-Date</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=205">Superman: Not Communicating</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=92">Teenagers are Honey</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=153">That\'s Just Your Opinion</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=137">That Word, I Do Not Think...</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=207">Tits or GTFO</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=73">Threadjack</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=53">Try Again, With Words</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=74">Wat</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=94">Weaksauce</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=16">Well English, You</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=56">What the fuck</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=43">Need A Hug</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=150">Hug #2 <BR>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=123">Wildstar, Pimp Slapped</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=133">You Couldn\'t Just Leave It Alone</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=284">You Don\'t Know Me!</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=83">You Killed The Thread</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=147">Your Links Don\'t Work</a><br>';
	
	
	ob_mod_template += '<h3 style=""margin:3px; padding:1px; text-align:center"">Audio</h3>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=299">I\'m Black Ya\'ll</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=289">Price Is Wrong</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=348"><img src="images/forum/dot.png"> Oh No You Didn\'t</a> / ';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=342">Record Skip</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=335">SMB (Game Over)</a><br>';
	
	
	
	
	ob_mod_template += '<h3 style=""margin:3px; padding:1px; text-align:center"">Thunder Dome Only</h3>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=63">Nothing\'s Crueler</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=62">Nobody Cares</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=292"> Stop Posting</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=152">You Suck And Are Gay</a>';
	
	ob_mod_template += '</td><td valign="top" width="24%" style="font-size:7pt"><h3 style=""margin:3px; padding:1px; text-align:center"">Other Flags</h3>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=3">Please Review</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=1">Remove Flags</a><br>';
	ob_mod_template += '<h3 style=""margin:3px; padding:1px; text-align:center"">General Options</h3>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=1011">Make 18+</a><br>';
	ob_mod_template += '<a href="of.asp?id_post=[ID_POST]&tab=[TAB]&action=st&page=[PAGE]&idpms=[ID_POST_MODERATE]&pms=1010">Make All-Ages</a><br>';
}

if (typeof ob_mod_template_ubermod=='undefined') {
		var ob_mod_template_ubermod='';
	}
if (typeof ob_mod_template_ubermod_parent=='undefined') {
		var ob_mod_template_ubermod_parent='';
	}
	
var ob_mod_template_close = '<h3 style=""margin:3px; padding:1px; text-align:center"">Mod Log</h3>[MOD_LOG]</td></tr></table>';
