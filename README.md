# LD51 Source - Title: Cookin' Bash 10

## Orientation
This is the source code for TwoWolf's Ludum Dare 51st Entry. This includes everything in the Assets folder including external plugins, packages, sprites, and external files such as Photoshop/Aesprite. All this does not go over 20mb so I did not bother erasing them.

### This is my 51th entry for Ludum Dare: Cookin Bash 10! (No, I'm not ashamed.)

It's a small bite-sized game where you cook various meals and serve customers and clean after them. You get paid once they eat and leave. Be quick though, since you have rent and employees to pay. Otherwise you'll go bankrupt! But earn more than $300 and you'll be able to pay your own house rent!

And as per theme, your cooks, waiters, and customers finish performing various actions every 10 seconds.

## How to Play

So, for the first time in my dare history I made a game that does not use a keyboard. Complicated controls was always one of the main drawbacks of my game, so this time I made the controls extremely easier and made it mouse-only, L-Click only to be exact. 

And also for the first time, I will not disclose any how-to-play other than this. This game becomes massively easy when you already know what goes where, so I wanted to create at least a semblance of a challenge. You already know you only have to l-click to progress, and you will receive mouse hover hints. Technically that is what you only need to beat the game. Try to treat it as a clicker adventure game where you know nothing at first but eventually find things out!

If you played at least a single cooking dash game in your life, it will come relatively easy to you (although the mechanics can be a little different, given the theme.) If you didn't (which I doubt), I'll be honored to be your first cooking dash and you can determine whether you like the genre or not! 

The game loops itself should you game over (when money goes below 0) and you can replay by clicking again should you finish the game. It's a bite-sized game that wouldn't take too long to see the ending. 

## Creation Process

### Brainstorming

I'll be honest, for some reason, I wasn't very tense this round. You could say that I was relaxed, but also that I was in the mindset of sort of "let's wing it and see what happens."

Turns out a little bit of stress is good for your brain to get working. I've had the hardest time coming up with an idea for this round. I've spent at least hours just thinking and getting a snack, normally I have to spend around 2 hours.

I did manage to narrow it down to 2 genres. It was either a robot game where each robot takes its turn every 10 seconds, or this. I still don't think that was a bad idea, it would've been fun if I went that route.

### Issues

While I do use Unity Engine, I still prefer my own atlas creator. Maybe if I get around to it I'll introduce my tool to this community and share it with others, because it's so much more convenient and faster making and organizing sprite based art and animation. 

I also have a few Unity scripts that imports the file I export with my own tool. Turns out though there were way too many serious bugs with that tool that I wasn't able to catch. So I spent another 3 hours fixing the bug instead of creating the game. It was too essential for my workflow and I couldn't get anything done without it. 

I did eventually fix the bug but at this point I was pretty sure I couldn't make it compo this round. I already stalled way too much and progress was becoming way too slow. 

### Sprites

Luckily I did manage to short-sprint making a lot of sprites to get the feel of the game. 

Yes, I'm one of those people who work on the art first. I'm not sure if it's more efficient, it just seems that's how I naturally roll. Maybe I think I have no time to prototype since there's only 2~3 days to work with and I have to change sprites and animation stats so many times in multiple places. 

I create sprites with aesprite and export the png to my tool, which is designed to automatically detect changes in textures. 

![alt text](https://static.jam.host/raw/1a6/32/z/524b9.png)
![alt text](https://static.jam.host/raw/1a6/32/z/524ba.png)
![alt text](https://static.jam.host/raw/1a6/32/z/524bb.png)

I also have a custom tile asset script I use in Unity to create tiles faster. I might share that too one day! Sprite prototyping is one of my fortes, or at least where I have the most efficient workflow compared to other areas. So despite all the drawbacks I mentioned earlier, I was able to create this at the end of the first day:

![alt text](https://static.jam.host/raw/1a6/32/z/524c5.png)

Now obviously I did not finish my entire art in just one day. As with always we always have to go back and forth with game design, art, and programming. It's just a general phase thing however. Normally prototype programming coems first, for me in Ludum Dares it's the other way around, that's all.

### Scripting

I have to admit, I was not expecting this phase to move so slow. But it did this round. I wouldn't consider myself weak at programming but now I understand that I grossly underestimated at least two things in this phase.

#### Scalability? In my max 3-days long Ludum Dare?

First of all, the logic that goes into this simple game is a state-machine hell. You can sort of infer from my github that I use the interface pattern for all my objects. All my game utilities, whether it's the fridge or the chopping board or even the tables, all have the same base interface class. My logic for doing so was that I can make creating new utilities much easier without having to reuse code or create different algorithms for character-utility interaction.

The utilities were similar enough for this pattern to work, but also different enough that I had to change the base interface methods all the time and reimplement them again and again. I'm guessing now, that I should've used multiple interfaces instead or even dynamically cast my classes whenever there's class specific logic. I did eventually do that here and there at the end... because although that's not the "programmatically acceptable way," I just couldn't.

But I wonder now if all that was overthinking or overdoing in my part. The programming took way too long than I expected, the pattern did help eventually but only at the last moments where I had to make more of the same utilities with its own functionality. But by then the time was ticking towards the end. Have I been focusing too much on scalability with a code that does not have to scale in the future?

This is the infamous code that normally makes things easier but made it harder for 2~3 days of solitude coding instead:

![alt text](https://static.jam.host/raw/1a6/32/z/52546.png)
![alt text](https://static.jam.host/raw/1a6/32/z/52548.png)

Believe it or not I spent way too much time in both of these scripts.

#### Maintainability? In my max 3-days long Ludum Dare?

This one's easy. I did a really strange and new thing this run: I used no global values. None. 

Normally I have a couple or more singletons which contain all the essential and widely-used data of the game. And/or I attach a script to the root of my hierarchy (which is how I tend to organize my objects, normally) and then let it get components from children and expose it via static variables, so that anyone who needs it can refer to it, anywhere. 

But this round, I used absolutely none. This was such a big mistake, because while I created maintainable, encapsulated code... I wonder why I did that for code that does not need to be maintained. 

#### Moral of the story? 

Do not write maintainable, scalable, reusable code for a program that does not have to be scaled/maintained. Get things done fast during the dare, no need to write perfect code. 

### Overall

I did manage to finish despite things moving particularly slow this ludum dare. It was an interesting theme to say the least, and the programming (including the importer bugs) made me want to give up at times but I did manage to finish a working small game eventually! 

Normally I do have time to create my own sounds. I do compose music in my spare time so I always leave room to insert my own compositions. And I did, but you'll notice I reused the sfx and the bgm is very short (and honestly not of quality.) Also there's a looping issue if you lose game focus. I did fix this bug in my last game but I forgot to bring those changes here. I forgot, and there was no way of knowing.

Hopefully things move a lot quicker next time and I learn from my lessons. There was so much more I wanted to add and I'm confused about the time it has taken to create this small game. Maybe there's more to cooking dash than I realized.

But all in all, I finished the game! And I'm glad I didn't throw in the towel. I made it to the very end again this year.
