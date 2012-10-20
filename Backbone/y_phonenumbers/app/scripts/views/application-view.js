phonenumbers.Views.Item = Backbone.View.extend({
    manage:true,
	template: "#item",
	tagName: "tr",
	events: {},
	initialize: function() {
		console.log("item init",this);
		this.model.on("change", function() {
			console.log("change render");
			this.render();
		}, this);
	},
	serialize: function() {
        console.log("serialize called");
		return {
			FirstName: this.model.get("FirstName"),
			LastName: this.model.get("LastName"),
			Login: this.model.get("Login"),
			PhoneNumberUs: this.model.get("PhoneNumberUs")
		};
	},
    beforeRender: function(){
        console.log("item beforeRender",this);
    }
});
phonenumbers.Views.List = Backbone.View.extend({
    manage:true,
	tagName: "table",
    beforeRender: function() {
        // Iterate over the passed collection and create a view for each item.
        this.collection.each(function(item) {
            console.log("beforeRender",item);
            // Pass the sample data to the new SomeItem View.
            this.insertView(new phonenumbers.Views.Item({model:item}));
        }, this);
    },
	render: function(manage) {
        console.log("render",this);
       this.collection.each(function(item) {
            console.log("render",item);
            this.insertView(new phonenumbers.Views.Item({model:item}));
        }, this);
        return manage(this).render();
    },
    initialize: function() {
        console.log("list init");
        this.collection.on("reset", function() {
            console.log("reset",this);
            this.render();
        }, this);

        this.collection.on("add", function(item) {
            console.log("add",this);
            this.insertView(new phonenumbers.Views.Item({model:item})).render();
        }, this);
    }
});