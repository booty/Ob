# Events specification:

Events will be created like unique objects that can be used throughout the site. When an event is created, a thread will automatically be created in the owner’s name, invites will go out to selected members it will have the following properties:
1. EventID
2. Date/Time
3. Location
4. Address - (at minimum require a zipcode)
5. Title
6. Type - (Con, small gathering, house party, meetup)
7. Description
8. Contact Info
9. Fees/Cost
10. Lodging
11. Public/Private
12. URI
13. ThreadID
14. Active - Boolean In case the event is cancelled or rescheduled, active would be False
15. RescheduleRefID - ID of the new event that was created based off of the originally scheduled event. When an event is rescheduled, it copies all of the info over to a new event creation dialogue. The previous event thread is locked and redirects to the rescheduled event.

Members attending the event will have the eventID associated with their attended event table. Members mark themselves as attending by clicking on an “Attending” button at the top of the event page. This will be cool for tracking what events people go to and could have some nice social tool capabilities tied to it. The page will have a different interface from normal forum threads, with a expanding div containing all of the basic info. 

After an event is over, the thread will change into a “Post Event” mode, where the pre event posts will still be accessible, but essentially archived to a separated conversation tab. 

All photos posted or linked to in the thread will be available for viewing through an image browser (as long as they’re linked from a site like picasa or flickr that has an external API, and there aren’t more than a certain limit of photos). 