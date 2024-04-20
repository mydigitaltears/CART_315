<h1>Xavier Touikan - Cart 315 Journal<h1>
<h2> Week 5 (Vampire Survivor Analysis)<h2>

<p>
Vampire Survivor is a 2022 roguelite video game that took the world by storm because of it's simplicity and addictive gameplay.
It combines many existing and established ideas in a way that felt new and refreshing at the time it came out.

When you first play the game, nothing needs to be explained to the player. You move the character by using the arrow keys and
you realize that the character attacks periodically. All you need to do is move and position yourself to make your attacks hit the enemies.

The enemies come from every border of the screen and walk towards you. You need to evade them while positioning yourself to attack them.
This simple gameplay combines elements of casual games because of the low number of inputs with elements of challenging roguelite games
where precision is important and the stakes feel high.

We could argue that the game failed to keep me entertained for an extended amount of time, but I still played it for nearly 10 hours.
For what it's worth, this game did it's job for it's very small price of 5$. I would love to create games that are also limited to a
very small amounf of inputs (in this case 4), but that also provide depth in the gameplay (in this case the precision of the movements).
<p>
<img title="a title" alt="Alt text" src="https://github.com/mydigitaltears/CART_315/assets/25814675/c25743dc-b307-480d-a31e-db79892db78c">


<h2> Week 7 (Conceptualizing prototypes)<h2>

<p>
I wanted to explore the notion of impossible games, or games that you have to push the boundaries of what the game allows you to do in order to win it. Games that are mechanically hard or that could encourage thinking outside of the box to beat them!

The first idea I had was a rhythm game in a factory setting with conveyor belts. The player operates different machines with different keys and as the game goes the player has to keep track of more and more timings.

Another idea I had was a multiple answer conversation game based in a fictional language. With every answer you give you can see the reaction of the other character, this way can understand what the words mean to some extent. The kind of storytelling of a fictional world in this reminds me of what has been done with the game: Papers, Please!

I also thought of exploring games using the microphone, where the player would give instructions to a secret agent or general directions over the phone to complete some kind of objective.


I then decided to prototype my first idea using three different rhythms of blocks falling down. After some playtesting I was able to get the hand of the timings so it was not to hard, but the goal would be to extend this concept into as many different keys as possible. After seeing other people playtest it, it seemed very hard to keep track of the timings, which can be a good or bad thing. The game seemed also quite alienating. It could actually be something to explore further given the plan of making it in a factory setting. Altough I was telling people that nothing new was going to come after the 3rd kind of timing starts appearing, people kept playing it for a while just zoning out trying to hit the blocks.

![movie_001-ezgif com-video-to-gif-converter](https://github.com/mydigitaltears/CART_315/assets/25814675/3b561f25-e0c8-4032-8079-7261e9c75c52)

<p>

<h2> Week 8 (Conceptualizing prototypes #2)<h2>

<p>
The next idea I wanted to explore was a card castle building game. I used unity's built-in physics system to create the card prefabs and it works very well. Since the player can't hold 
two cards at the same time like someone would do in real-life, once the player is dragging a card they can press spacebar to freeze it in place for 5 seconds. This allows the player to drag another card next to it to form the card castle or it can be used to freeze the card horizontally when it's on the ground to place it easily on top of the other cards.

While the mechanics of the game definitely works, I have no idea on how to start iterating and add ideas to it. I don't think it would be a very fun game to play.
<p>

<h2> Final Game: Hooked!<h2>

<p>
The final idea I decided to settle on was a 2D platformer that uses a grappling hook as a special movement mechanic. I realized quickly how complicated it can be to code a grappling hook with basic movement. A way to make the grappling hook more realistic is to cut the ability to drift in the air while the player is hooked, so basically only the inertia and momentum are being applied when the player is hooked and we get the pandulum motion that I desired. Altough the grappling hook was working like I desired, I knew that the game would be frustrating if the player had 0 control while being hooked so I had the idea of implementing a dash.

The dash ended up working perfectly with the hook because the horizontal velocity is translated into a circular motion and allows to also gain a lot of vertical momentum. At this point,
the player could attach their hook anywhere the mouse was pointing to. After some playtesting in class, multiple people recommended me to change the hook so it can only attach to platforms or walls. 

At this point the game is working well with keyboard and mouse, but I realised a lot of people prefer to play platformers with a controller. I had no idea how to translate the precision of clicking at a point with the mouse into something a controller could do. The solution I came up with was to change how the hook works. Instead of hooking at the point where the mouse is clicked, the player throws a ball in the direction of the mouse and will attach to ground if it connects. Then I was easily able to make the game compatible with a controller. 

After a lot of testing, I wanted the game to have as few controls as possible. So basically the player can move left or right, jump, throw the hook and dash while being hooked. For the keyboard controls the player moves with the A and D key, jumps with SPACE, throws the ball in the mouse direction with LEFT CLICK and dash with also A and D while being hooked. For the controller I used a bit of a different system. The player moves with the LEFT JOYSTICK, jumps with A, throws the ball with the RIGHT TRIGGER in the direction of the LEFT JOYSTICK and dash by pushing the RIGHT JOYSTICK left or right. At first I though it would be better to separate the movement and the aim into two different joystick, like it would be with keyboard and mouse, but I realised that it was actually less intuitive than using one joystick for both actions.

For the level design, my first idea was to create a fauddy-esque level similar to a game like 'Jump King'. These games are designed to be very hard and challenging, so my level was hard to evaluate through playtesting. Presonally I could beat the game easily because I've played it a lot at this point, but everyone else was struggling. I decided to also try another idea similar to traditional platformers with levels and rooms that would be easier. This other idea also worked out well so I think the game could go in either direction. 
<p>
