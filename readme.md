**OtakuBooty v.Next**

# OtakuBooty API

It's designed to be basically REST-y, not that any two people have the same opinions on what exactly that means.

All API methods, besides /AuthenticationToken (which is what you call to *get* a token) require an authentication token.

## API: Authentication

http://api.otakubooty.com/api/authenticationtoken

Parameters
* __login:__ your login, duh
* __password:__ your password, duh
* Returns an authentication token along with other user details. 
* NB: Also sends a cookie called "authenticationToken" on successful auth.
    
Currently, you can pass login and password either as form fields or in the querystring. This will change at some point because obviously, doing it over a querystring is a terrible idea - and doing it *either* way sans SSL is a Bad Idea. But we're keeping it simple for now.

Since this method also sets the authenticationToken cookie, theoretically you can authenticate via the URL and then hit any of the other methods without any further effort, since they'll all look for and respect that cookie.

## API: Using The OB Authentication Token & Other Conventions

Most methods require a valid authentication token so that they know who you are. They look for an authentication token in the following places, in this order:

1. An "ObAuthentication" HTTP request header 
2. Query-string parameter named "authenticationToken"
3. Form field named "authenticationToken"
4. Cookie named "authenticationToken"
 
Most API list calls also support "skip" and "take" arguments to support paging or incremental loading.  

Defaults to the newest 25 notifications (NB: If you really need exactly 25, use the "take" parameter)
   http://api.otakubooty.com/api/notifications  

Gives you the first 10:
   http://api.otakubooty.com/api/notifications
   
Gives you the 51st through 75th notifications: 
   http://api.otakubooty.com/api/notifications?skip=50&take=25
 

## API: Notifications 


http://api.otakubooty.com/api/notifications?type=messages

* __type:__ can be "messages", "comments", "profileviews", "friendings", "fops", "messagesandcomments", or "all". Defaults to "all" if omitted
* __skip:__ for paging. number of records to skip. currently not implemented!
* __take:__ for paging. number of records to take (ie, page size). currently not implemented!


http://api.otakubooty.com/api/privatemessages?type=sent

http://api.otakubooty.com/api/privatemessages?type=received

http://api.otakubooty.com/api/privatemessages (same as type=received)

http://api.otakubooty.com/api/phonenumbers?friendsOnly=true

http://api.otakubooty.com/api/phonenumbers
