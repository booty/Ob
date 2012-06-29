# OB Forums Spec (ideas)
(currently idea dump, not formatted yet)

Forums can be viewed a number of ways since threads are not strictly tied to a specific forum tab/section. Threads do have a main tab/section that they initially belong to when they’re created (for simplicity and less confusion, it would be best to keep this standard), but may have tags added to them, which will allow them to be listed under custom tabs/sections that have (or don’t have) those tags as content specification options. 

For instance if I wanted to create a custom tab that included all threads in Otaku Chat EXCEPT for ones with the tag “WoW”, I could use that to effectively replace my Otaku Chat tab for a much more pleasant browsing experience.

Or I want to create a tab that specifically pulls in any thread with any of the tags “manga”, “anime”, “hentai”, “furries”, or “yaoi”, I could do that (but seriously, why?)

Or I want to mash up the main tabs that I frequently view into one tab so I don’t have to jump around, I simply create a custom tab that includes all of the sections I want and they will display all threads contained in those sections. 

Obviously, some tags or tag combinations would not pull a lot of threads, if any at all. If this is the case we may want to suggest popular tags to help find more threads. When creating a thread or adding a tag to an existing thread, we’ll provide suggestions based on popular tags that match what they’re typing. Or we can suggest after they submit the thread based on the content of their thread.

There may be cases of tag abuse or spamming, to which there could be a few solutions:
1. Lock tag addition down to everyone but mods and the creator of the thread. Tag suggestions can be made by anyone and then be reviewed by the thread creator or moderators. Said locking could be toggled on or off by mod or creator.
2. Tags can only be added by friends, mods, or the thread creator.
3. Supplemental tags can be added by anyone, but they are only pertinent to the person who added them. They do not affect tab assignment for anyone but the person who added the tag. (Not very easy on the DB though)
4. Supplemental tags can be added by anyone, but they are only pertinent to people on their friends list.

The threads in a custom tab would order by date as the default (though I’m not sure ordering by anything else would be beneficial to a custom tab and would be better served by the search functionality).

Additionally, there could be a new tab that digested threads created or commented in by the user’s friends. This would be really useful for days when the site has low activity, and help improve the visibility of recent posts without having to jump between tabs and refreshing constantly.

Or there could be a tab that digested threads by users within a specified geographic area. This would be awesome for regional food and area restaurants people want to talk about or recommend (because we’re all a bunch of fatties, I see this being of high value).

Threads by ignored users would still appear, but have a highlit background to signify that they’re from an ignored user. Posts inside of threads by ignored users would appear as collapsed divs that can be expanded if clicked upon.

Spoiler/NSFW filters can be turned on for posts that are tagged as such. This would set the style of the post to block out the text and images unless revealed by clicking a button that toggles the style to show the text and images.

Threads over a specific age (probably 2 years) with no activity for a specified time (probably 2 months) will be automatically archived to a separate database. At the beginning of a year, a job will run that creates a database for the previous year. Every night a job will run that checks the date and activity of the threads on the current database, if the thread meets the criteria it is archived. Once a thread has been archived, any responses to it will start a new thread with a reference to the old one. If a reference thread exists already, the responses will go to that thread rather than starting a new one.