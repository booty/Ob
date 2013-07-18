// global vars



$(function () {

	console.log("We're ready to rock some shit.");

	// click handler for tabs
	$('.main-tabs').click(function (e) {

		// does the original click target have a tab assoc?
		var $clickedTab = $(e.target).closest('[data-tab-content-selector]')
		var targetContentSelector = $clickedTab.data('tab-content-selector')
		if (targetContentSelector == null) {	
			console.log('Warn: clicked something in a tab strip, but no content target... don\'t know what tab this belongs to');
			return;
		}

		// get the tab container (where the content is)
		var $activeContentContainer = $(targetContentSelector);
		if ($activeContentContainer == null) {
			console.log("Warn: tab content container not found");
			return 
		}

		// get the tab itself
		$('.main-tabs a').removeClass('active');
		$clickedTab.addClass('active');

		// hide all other tabs in the container
		$('#TabContentDiv').children().hide();
		$activeContentContainer.show().addClass('active');
		

		// show the chosen tab
		console.log('ok!');
	});




});