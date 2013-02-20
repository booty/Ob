// Globals. Sorry; just deal with it.
var $threads, $thread, $forums, $memberPictures, $threadReplies, $memberProfile;
var $notificationsTab, $privateMessagesTab, $fopsTab, $phoneNumbersTab;

var API_URL_MEMBER_PICTURES = 'api/MemberPictures?skip=0&take=20';
var API_URL_FORUMS = 'api/Forums/';
var API_URL_NOTIFICATIONS = 'api/notifications/?skip=0&take=10';
var API_URL_PRIVATE_MESSAGES = 'api/notifications/?notificationType=messages&skip=1&take=10';
var API_URL_FOPS = 'api/notifications/?notificationType=fops&skip=1&take=10';
var API_URL_PHONE_NUMBERS = 'api/phoneNumbers';

var cacheBuster = new Date().getTime();

/*
This will be set to true after the initial page load. Initially, 
it's set to false so that the page doesn't involuntarily scroll 
all over the place in a seizure-inducing way as soon as the page loads.
scrollToThing() checks this variable before it does any actual scrolling.
*/
var autoScrollEnabled=false; 


/*
Truncates a string in a more human-readable manner, by 
attempting to truncate along whitespace
*/
function friendlyTruncate(text, maxCharsOfTruncatedText, maxCharsToBacktrack) {
	// Nothing to do?
	if ((text === null) || (typeof text == 'undefined')) return '';
	if (text.length <= maxCharsOfTruncatedText) return text;

	// The most chars we're willing to backtrack before we give up 
	maxCharsToBacktrack = maxCharsToBacktrack || 10;

	// Count backwars from the end of the string, looking for blank space
	// A more complete, but perhaps slower, version of this function could look for all white space (tabs, etc.)
	var lastWs = maxCharsOfTruncatedText;
	while ((lastWs > 0) && (lastWs >= (maxCharsOfTruncatedText - maxCharsToBacktrack)) && (text[lastWs] != ' ')) {
		lastWs -= 1;
	}
	
	return text.substring(0, lastWs) + '&hellip;';
}

// Scroll to some element on the page
function scrollToThing($thing) {
	if (!autoScrollEnabled) return;
	$('html, body').animate({
		scrollTop: $thing.offset().top
	}, 1000);
}

// Log out of OB by deleting the relevant cookies
function deauthenticate() {
	$.removeCookie('login');
	$.removeCookie('authenticationToken');
	console.log('Cookies removed.');
	updateUiForUnauthenticatedUser();
}

// Just for convenience, the name of the currently logged-in user
function currentMemberName() {
	return $.cookie('login');
}

// Hide/show appropriate divs, re-fetch forums
function updateUiForAuthenticatedUser() {
	cacheBuster = new Date().getTime();
	$('.auth').fadeIn();
	$('.unauth').fadeOut();
	$('.currentMemberName').html(currentMemberName());
	
	// todo: show notifications
	$('#AuthenticatedMemberTabs a:first').tab('show');
	getNotifications();

	// Re-get the forums (even if we already got them) because unauth'd users don't see all the forums
	console.log('Getting (or re-getting) forums for auth users');
	getForums();
}

// Hide/show appropriate divs, re-fetch forums
function updateUiForUnauthenticatedUser() {
	cacheBuster = new Date().getTime();
	$('.auth').fadeOut();
	$('.unauth').fadeIn();
	// Re-get the forums (even if we already got them) because unauth'd users don't see all the forums
	console.log('Getting (or re-getting) forums for unauth users');
	getForums();
}


// todo: shouldn't be handling UI code in here?
function validateAuthenticationTokenCookie() {
	// Is there an auth token cookie? If not, return false.
	if ($.cookie('authenticationToken') == null) {
		updateUiForUnauthenticatedUser();
		return false;
	}

	// If there is an auth token, let's verify it
	var jqxhr = $.ajax({
		async: false,
		data: {
			authenticationToken : $.cookie('authenticationToken')
		},
		dataType: 'json',
		type: 'POST',
		url: '/api/authenticationTokens/'
	});

	jqxhr.done(function() {
		console.log('auth cookie success');
		updateUiForAuthenticatedUser();
	});

	jqxhr.fail(function() {
		console.log('auth cookie fail');
		updateUiForUnauthenticatedUser();
	});

}

function weAreAuthenticated() {
	return ($.cookie('authenticationToken') != null);
}

function authenticate(login, password) {
	// If there is an auth token, let's verify it
	var jqxhr = $.ajax({
		async: false,
		data: {
			login: login,
			password: password
		},
		dataType: 'json',
		type: 'POST',
		url: '/api/authenticationTokens',
	});

	jqxhr.done(function() {
		console.log('login/pass auth success');
		updateUiForAuthenticatedUser();
	});

	jqxhr.fail(function() {
		console.log('login/pass auth fail');
	});

}

function handleTehClicks(e) {
	console.log('ooh!');
	if (e.target.attributes["data-id-forum"]) {
		e.preventDefault();
		getThreads(e.target.attributes["data-id-forum"].value);
	}
	if (e.target.attributes["data-id-thread"]) {
		e.preventDefault();
		console.log('I should show replies for thread #' + e.target.attributes["data-id-thread"].value);
		getThreadReplies(e.target.attributes["data-id-thread"].value);

	}
	if (e.target.attributes["data-id-member"]) {
		e.preventDefault();
		console.log('I should show member profile information for member #' + e.target.attributes["data-id-member"].value);
		getMember(e.target.attributes["data-id-member"].value);
		// todo: scroll to member
	}
}

/* Retrieve and show a list of the forums */

function getForums() {
	$forums.find('.apiUrl a').attr('href', API_URL_FORUMS).html(API_URL_FORUMS);
	$forums.find('.loading').fadeIn();
	$.ajax({
		dataType: 'json',
		url: API_URL_FORUMS + '?foo=' + cacheBuster,
		success: function (data) { showForums(data); }
	});
}

// note: also always loads threads for forum #1 (gen chat) :-)
function showForums(jsonData) {
	$forums.find('.content').empty();
	$forums.find('.loading').fadeOut();
	for (var i = 0; i < jsonData.length; i++) {
		$forums.find('.content').append(ich.forumTemplate(jsonData[i]));
	}
	getThreads(1);
}

/* Retrieve and show threads in a particular forum */

function getThreads(idForum) {
	// Kludge: we don't want to autoscroll the FIRST time, since this is called on page load.
	//if (autoScrollEnabled) scrollToThing($('#threads'));
	//autoScrollEnabled=true; // set this to true so that subsequent thread loads autoscroll
	var jsonUrl = '/api/Forums/' + idForum + '?skip=1&take=10&includeSticky=1&foo=' + cacheBuster;
	$threads.find('.loading').fadeIn();
	$threads.find('.apiUrl a').attr('href', jsonUrl).html(jsonUrl);
	$.ajax({
		dataType: "json",
		url: jsonUrl,
		success: function (data) { showThreads(data); }
	});
}

/*
Todo: display friendly message when permissionViewThreads or permissionReadReplies
exceeds current user's permission level.  
Todo: Handle when jsonData.threads is null (this happens if perms too low, or if forum simply has no threads
*/
function showThreads(jsonData) {
	$threads.find('.loading').fadeOut();
	$threads.find('.content').empty();
	$threads.find('h2').html('Latest Threads in ' + jsonData.title);
	for (var i = 0; i < jsonData.threads.length; i++) {
		jsonData.threads[i].body = '"' + jsonData.threads[i].body.substring(0, 90) + '..."';
		$threads.find('.content').append(ich.threadTemplate(jsonData.threads[i]));
	}
}

/* Retrieve and show posts in a particular thread */

function getThreadReplies(idThread) {
	$threadReplies.show();
	scrollToThing($('#threadReplies'));
	var jsonUrl = '/api/Threads/' + idThread + '?skip=0&take=10';
	$threadReplies.find('.loading').fadeIn();
	$threadReplies.find('.apiUrl a').attr('href', jsonUrl).html(jsonUrl);
	$.ajax({
		dataType: "json",
		url: jsonUrl,
		success: function (data) { showThreadReplies(data); }
	});
}

function showThreadReplies(jsonData) {
	$threadReplies.find('.loading').fadeOut();
	$threadReplies.find('.content').empty();
	$threadReplies.find('h2').html('Replies to "' + jsonData.subject + '"');
	for (var i = 0; i < jsonData.replies.length; i++) {
		jsonData.replies[i].body = '"' + jsonData.replies[i].body.substring(0, 200) + '..."';
		$threadReplies.find('.content').append(ich.threadTemplate(jsonData.replies[i]));
	}
}

/* Retrieve and show member pictures */

function getMemberPictures() {
	$memberPictures.find('.loading').fadeIn();
	$memberPictures.find('.apiUrl a').attr('href', API_URL_MEMBER_PICTURES).html(API_URL_MEMBER_PICTURES);
	$.ajax({
		dataType: "json",
		url: API_URL_MEMBER_PICTURES,
		success: function (data) { showMemberPictures(data); }
	});
}

function showMemberPictures(jsonData) {
	$memberPictures.find('.loading').fadeOut();
	$memberPictures.find('.content').empty();
	for (var i = 0; i < jsonData.length; i++) {
		$memberPictures.find('.content').append(ich.memberPictureTemplate(jsonData[i]));
	}
}

/* Retrieve and show member profile */

function getMember(idMember) {
	var jsonUrl = '/api/Members/' + idMember;
	$memberProfile.show();
	$memberProfile.find('.apiUrl a').attr('href', jsonUrl).html(jsonUrl);
	$memberProfile.find('.loading').fadeIn();
	$.ajax({
		dataType: "json",
		url: jsonUrl,
		success: function (data) { showMember(data); }
	});
}

function showMember(jsonData) {
	scrollToThing($('#memberProfile'));
	$memberProfile.find('.loading').fadeOut();
	$memberProfile.find('.content').empty();
	$memberProfile.find('h2').html(jsonData.member.login);
	$memberProfile.find('.content').append(ich.memberProfileTemplate(jsonData.member));
}

/* Get and show notifications */

function addRenderedFields(n) {
	if (n.idMemberFrom) n.memberTag = '<span data-id-member="' + n.idMemberFrom + '">' + n.login + '</span>';
	switch (n.eventType) {
		case 'Comment':
			n.renderedSubject = n.memberTag + ' sent you a comment';
			n.renderedBody = '&ldquo;' + n.body + '&rdquo;';
			break;
		case 'FOP Shared':
		case 'FOPs Shared':
			if (n.quantity == 1) {
				n.renderedSubject = n.memberTag + ' shared a Friends-Only Picture with you';
			}
			else {
				n.renderedSubject = n.memberTag + ' shared ' + n.quantity + ' Friends-Only Pictures with you';
			}
			n.renderedBody = '';
			n.class = 'large';
			break;
		case 'Private Message':
			n.renderedSubject = n.memberTag;
			n.renderedBody = friendlyTruncate(n.body, 170, 20);
			break;
		case 'Profile View':
			n.renderedBody = n.memberTag + ' viewed your profile';
			n.compact = true;
			n.class = 'small';
			break;
		case 'Friended':
			n.renderedBody = n.memberTag + ' friended you';
			break;
	}
	if (typeof n.renderedSubject == 'undefined') n.renderedSubject = n.subject;
	return n;
}


function getNotifications() {
	$notificationsTab.find('.loading').fadeIn();
	$notificationsTab.find('.apiUrl a').attr('href', API_URL_NOTIFICATIONS).html(API_URL_NOTIFICATIONS);
	$.ajax({
		dataType: "json",
		url: API_URL_NOTIFICATIONS + '?foo=' + cacheBuster,
		success: function (data) { showNotifications(data); }
	});
}

function showNotifications(jsonData) {
	$notificationsTab.find('.loading').fadeOut();
	$notificationsTab.find('.content').empty();
	
	for (var i = 0; i < jsonData.length; i++) {
		jsonData[i] = addRenderedFields(jsonData[i]);
		$notificationsTab.find('.content').append(ich.notificationTemplate(jsonData[i]));
	}
}

/* Get and show priv. messages */

function getPrivateMessages() {
	$privateMessagesTab.find('.loading').fadeIn();
	$privateMessagesTab.find('.apiUrl a').attr('href', API_URL_NOTIFICATIONS).html(API_URL_NOTIFICATIONS);
	$.ajax({
		dataType: "json",
		url: API_URL_PRIVATE_MESSAGES + '&foo=' + cacheBuster,
		success: function (data) { showPrivateMessages(data); }
	});
}

function showPrivateMessages(jsonData) {
	$privateMessagesTab.find('.loading').fadeOut();
	$privateMessagesTab.find('.content').empty();

	for (var i = 0; i < jsonData.length; i++) {
		jsonData[i] = addRenderedFields(jsonData[i]);
		$privateMessagesTab.find('.content').append(ich.notificationTemplate(jsonData[i]));
	}
}

/* Get and show FOPs */

function getFops() {
	$fopsTab.find('.loading').fadeIn();
	$fopsTab.find('.apiUrl a').attr('href', API_URL_FOPS).html(API_URL_FOPS);
	$.ajax({
		dataType: "json",
		url: API_URL_FOPS + '&foo=' + cacheBuster,
		success: function (data) { showFops(data); }
	});
}

function showFops(jsonData) {
	$fopsTab.find('.loading').fadeOut();
	$fopsTab.find('.content').empty();

	for (var i = 0; i < jsonData.length; i++) {
		jsonData[i] = addRenderedFields(jsonData[i]);
		$fopsTab.find('.content').append(ich.notificationTemplate(jsonData[i]));
	}
}

/* Get and show phone numbers */

function getPhoneNumbers() {

}

function showPhoneNumbers() {

}

/* Page startup */

$(function () {
	// Save ourselves some typing (and maybe some DOM traversals? does jQuery/sizzle.js cache them or something?)
	$thread = $('#thread');
	$threads = $('#threads');
	$forums = $('#forums');
	$memberPictures = $('#memberPictures');
	$threadReplies = $('#threadReplies');
	$memberProfile = $('#memberProfile');
	$notificationsTab = $('#NotificationsTab');
	$privateMessagesTab = $('#PrivateMessagesTab');
	$fopsTab = $('#FopsTab');
	$phoneNumbersTab = $('#PhoneNumbersTab');

	console.log('Hello. Your DOM is ready, sir.');

	// We can do this first, before verifying the auth token (should we?)
	getMemberPictures(API_URL_MEMBER_PICTURES);

	// See if we're logged in, and hide/show various divs appropriately.
	validateAuthenticationTokenCookie();

	$('#AuthenticateButton').click(function(e) {
		e.preventDefault();
		authenticate($('#login').val(), $('#password').val());
	});
	
	$('.deauthenticate').click(function (e) {
		deauthenticate();
	});

	/*
	Click handlers for the authenticated member content tabs
	*/

	$('#NotificationsTab').on('shown', function (e) {
		console.log('Let\'s show notificaosidcoiYEAH');
	});

	$('a[data-toggle="tab"]').on('shown', function (e) {
		/*
		e.target // activated tab
		e.relatedTarget // previous tab
		*/
		// Is there a more jQuery-ish way to 
		switch ($(e.target).data('target')) {
			case '#NotificationsTab':
				getNotifications();
				break;
			case '#PrivateMessagesTab':
				getPrivateMessages();
				break;
			case '#FopsTab':
				getFops();
				break;
			case '#PhoneNumbersTab':
				getPhoneNumbers();
				break;
		}
	});

	$(document).click(function (e) {
		handleTehClicks(e);
	});
	

});