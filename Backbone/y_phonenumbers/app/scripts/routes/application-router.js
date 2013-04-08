'use strict';
var phonenumbers = window.phonenumbers || {};
var Backbone = window.Backbone || {};
var $ = window.jQuery || {};
var privatemessages = window.privatemessages || {};
phonenumbers.Router = Backbone.Router.extend({
	routes: {
      '': 'index',
      'pm': 'pm'
    },
    initialize: function(){
		console.log('router init');
    },
    index: function() {
      console.log('index');
      var list = new phonenumbers.Collections.Phonelist();
      // Create a layout and associate it with the main div
      var listView = new phonenumbers.Views.List({
          collection: list
        });
      console.log('listview: ',listView);
      $('#list').empty().append(listView.el);

      // Render to the DOM
      list.fetch({
          success: function(model,response) {
            console.log('success!',model,response);
          },
          error: function(response){
            console.log('error',response);
          }
      });
      console.log('fetching',list);
	},
    pm: function () {
        console.log('pm');
        var messageList = new privatemessages.Collections.MessageCollection();
        var currentMessageView = new privatemessages.Views.messagesView({
            collection: messageList
        });
        console.log('messageview', currentMessageView);
        $('#list').empty().append(currentMessageView.el);
        messageList.fetch({
            success: function(model,response) {
                console.log('success!',model,response);
            },
            error: function(response) {
                console.log('error',response);
            }
        });
        console.log('fetching', messageList)
    }

});
