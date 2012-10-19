
window.phonenumbers = {
  Models: {},
  Collections: {},
  Views: {},
  Routers: {},
  init: function() {
    console.log('Hello from Backbone!');
    this.router = new phonenumbers.Router();
    Backbone.history.start();
  }
};

$(document).ready(function(){
	Backbone.LayoutManager.configure({
		manage: true
	});
	phonenumbers.init();
});
