// Globals. Sorry; just deal with it.
var $threads, $forums, $memberPictures, $threadReplies, $memberProfile;

/*
This will be set to true after the initial page load. Initially, 
it's set to false so that the page doesn't involuntarily scroll 
all over the place in a seizure-inducing way as soon as the page loads.
scrollToThing() checks this variable before it does any actual scrolling.
*/
var autoScrollEnabled=false; 

function scrollToThing($thing) {
	if (!autoScrollEnabled) return;
	$('html, body').animate({
		scrollTop: $thing.offset().top
	}, 1000);
}

function updateUiForAuthenticatedUser() {
	$('.auth').fadeIn();
	$('.unauth').fadeOut();
	$('#authentication p.auth').html("You're authenticated as " + $.cookie('login') + '.');
	// todo: show authorizations

	// todo: show private messages

}

function updateUiForUnauthenticatedUser() {
	$('.auth').fadeOut();
	$('.unauth').fadeIn();
}

function validateAuthenticationTokenCookie() {
	// Is there an auth token cookie? If not, return false.
	if ($.cookie('authenticationToken') == null) return false;

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

function getForums(jsonUrl) {
	$forums.find('.apiUrl').attr('href', jsonUrl).html(jsonUrl);
	$forums.find('.loading').fadeIn();
	$.ajax({
		dataType: 'json',
		url: jsonUrl,
		success: function (data) { showForums(data); }
	});
}

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
	if (autoScrollEnabled) scrollToThing($('#threads'));
	autoScrollEnabled=true; // set this to true so that subsequent thread loads autoscroll
	var jsonUrl = '/api/Forums/' + idForum + '?skip=1&take=10&includeSticky=1';
	$threads.find('.loading').fadeIn();
	$threads.find('.apiUrl').attr('href', jsonUrl).html(jsonUrl);
	$.ajax({
		dataType: "json",
		url: jsonUrl,
		success: function (data) { showThreads(data); }
	});
}

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
	$threadReplies.find('.apiUrl').attr('href', jsonUrl).html(jsonUrl);
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

function getMemberPictures(jsonUrl) {
	$memberPictures.find('.loading').fadeIn();
	$memberPictures.find('.apiUrl').attr('href', jsonUrl).html(jsonUrl);
	$.ajax({
		dataType: "json",
		url: jsonUrl,
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
	$memberProfile.find('.apiUrl').html(jsonUrl);
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

/* Page startup */

$(function () {
	// Save ourselves some typing (and maybe some DOM traversals)
	$thread = $('#thread');
	$threads = $('#threads');
	$forums = $('#forums');
	$memberPictures = $('#memberPictures');
	$threadReplies = $('#threadReplies');
	$memberProfile = $('#memberProfile')

	console.log('Hello. Your DOM is ready, sir.');

	// We can do this first, before verifying the auth token (should we?)
	getMemberPictures('api/MemberPictures?skip=0&take=20');

	// See if we're logged in, and hide/show various divs appropriately.
	validateAuthenticationTokenCookie();
	if (weAreAuthenticated()) {
		updateUiForAuthenticatedUser();
	}
	else {
		updateUiForUnauthenticatedUser();
	}

	// Stuff that we load whether or not they're authorized
	getForums('api/Forums/', false);

	// Click handler for "authenticate button"
	$('#AuthenticateButton').click(function(e) {
		e.preventDefault();
		authenticate($('#login').val(), $('#password').val());
	});

	$(document).click(function (e) {
		handleTehClicks(e);
	});


});