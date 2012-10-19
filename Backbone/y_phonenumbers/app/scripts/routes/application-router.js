phonenumbers.Router = Backbone.Router.extend({
	routes: {
      "": "index"
    },
    initialize: function(){
		console.log("router init");
    },
    index: function() {

      console.log("index");
      var list = new phonenumbers.Collections.Phonelist();
      // Create a layout and associate it with the main div
      var listView = new phonenumbers.Views.List({
          collection: list
        });
      console.log("listview: ",listView);
      $("#list").empty().append(listView.el);
      // Render to the DOM
      list.fetch({
          success: function(model,response) {
            console.log("success!",model,response);
          },
          error: function(response){
            console.log("error",response);
          }
      });
      console.log("fetching",list);
	}

});
