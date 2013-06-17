$(function () {

	console.log("We're ready to rock some shit.");

	// click handler for tabs
	$('.tabs').click(function (e) {

		// does the original click target have a tab assoc?
		var targetContentSelector = $(e.target).data("tab-content-selector");
		if (targetContentSelector == null) {
			console.log('Warn: clicked something in a tab strip, but no content target');
			return;
		}

		// get the tab container
		var $contentContainer = $($(e.currentTarget).data('tabs-container-selector'));
		if ($contentContainer == null) {
			console.log("Warn: tab content container not found");
			return 
		}
		// get the tab this belongs to
		var $activeTab = $(targetContentSelector);
		if ($activeTab == null) {
			console.log("Warn: active tab not found");
			return;
		}

		if ($activeTab.hasClass('active')) {
			console.log('Active tab already active, exciting!');
			return;
		}

		// hide all other tabs in the container
		var children = $contentContainer.children()
		$(children).removeClass('active');
		$activeTab.addClass('active');

		// show the chosen tab
		console.log('ok!');
	});




});