define([
	// Application
	"app",
	// Libs
	"backbone"
],

function(app, Backbone){
	var Views = {};

	Views.Item = Backbone.View.extend({
		template: "phone/item",
		tagName: "tr",
		events: {

		},
		initialize: function() {
			console.log("item init",this);
			this.model.on("change", function() {
                console.log("change render");
				this.render();
			}, this);
		},
        serialize: function() {
            return {
                FirstName: this.model.get("FirstName"),
                LastName: this.model.get("LastName"),
                Login: this.model.get("Login"),
                PhoneNumberUs: this.model.get("PhoneNumberUs")
            };
        }
	});
	Views.List = Backbone.View.extend({
		tagName: "table",
        beforeRender: function() {
            // Iterate over the passed collection and create a view for each item.
            this.collection.each(function(item) {
                console.log("beforeRender");
            // Pass the sample data to the new SomeItem View.
            this.insertView(new Views.Item({model:item}));
            }, this);
        },
		render: function(manage) {
            console.log("render",this);
            this.collection.each(function(item) {
                this.insertView(new Views.Item({
                    model: item
                }));
            }, this);
            return manage(this).render();
        },
        initialize: function() {
            this.collection.on("reset", function() {
                console.log("reset",this);
                this.render();
            }, this);

            this.collection.on("add", function(item) {
                this.insertView(new Views.Item({
                    model: item
                })).render();
            }, this);
        }
    });
    return Views;
});
