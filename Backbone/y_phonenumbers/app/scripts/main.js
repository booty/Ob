window.phonenumbers = {
    Models: {},
    Collections: {},
    Views: {},
    Routers: {},
    init: function () {
        console.log('Hello from Backbone!');
        this.router = new phonenumbers.Router();
        Backbone.history.start();
    }
};

$(document).ready(function () {
    var app = {
        // The root path to run the application.
        root: "/"
    };

    // Localize or create a new JavaScript Template object.
    var JST = window.JST = window.JST || {};
    Backbone.LayoutManager.configure({
        manage: true,
        paths: {
            layout: "templates/layouts/",
            template: "templates/"
        },
        fetch: function(path) {
            path = path + ".html";
            if (!JST[path]) {
                $.ajax({ url: app.root + path, async: false }).then(function(contents) {
                    JST[path] = _.template(contents);
                });
            }
            return JST[path];
        }
    });
   phonenumbers.init();
});
