'use strict';
var privatemessages = window.privatemessages || {};
var Backbone = window.Backbone || {};
privatemessages.Views.messageView = Backbone.View.extend({
    manage: true,
    template: 'message',
    tagName: 'ul',
    events: {},
    initialize: function () {
        this.model.on('change', function () {
            this.render();
        }, this);
    },
    serialize: function () {
        // return the data for this message for display in template
        return {
            title: this.model.get('title'),
            sender: this.model.get('idMemberSender'),
            body: this.model.get('body')
        };
    }
});
privatemessages.Views.messagesView = Backbone.View.extend({
    manage: true,
    tagName: 'div',
    beforeRender: function () {
        var item = this.collection.at(1);
        this.insertView(new privatemessages.Views.messageView({model:item}));
    },
    render: function(manage) {
        console.log('render',this);
        this.collection.each(function(item) {
            console.log('render',item);
            this.insertView(new privatemessages.Views.messageView({model:item}));
        }, this);
        return manage(this).render();
    },
    initialize: function () {
        console.log('message init');
    }
});
