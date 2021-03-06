# GameService

An ASP.NET Core Web API for playing Connect Four, Tic Tac Toe, and Wordle through an API.

User inputs the move for the game, and the API does the game calculations in its model, and outputs the proper response, e.g. Illegal move, user wins, draw.
All games have automated option, where you can play against the machine.


You can start a game by calling the Post method **NewGame**. You must pass in a JSON object with two parameters. Game, where you input a string with the name of the game,
and Machine, where you input a bool whether you want to play against the machine or not.

For example:
```
{
    "Game":"ConnectFour",
    "Machine": true
}
```
The API will return a GUID in the message parameter. That will be the identifier for the instance of the game.

You can make a move by calling the Post method **TakeTurn**. You must pass in a JSON object with three parameters. Game, where you input a string with the name of the game 
you are making a move in, Guid, where you input a string of the game GUID, and Move, where you input a string of the move.

For example:
```
{
    "Game":"ConnectFour",
    "Guid": "7d4ddff5-0378-48c1-9a5c-db569e5a9944",
    "Move":"4"
}
```

The API will output a response of three parameters. Valid, a bool whether the game or move was valid, Message, a string with the appropriate message, and OpponentsMove, 
a string with the move the machine made in your game, if applicable.

For example:
```
{
    "valid": true,
    "message": "Move successful",
    "opponentsMove": "4"
}
```

For Wordle, playing with the machine just causes the machine to take all the guesses for the word, and will ouput all of its guesses.

For Connect Four and Tic Tac Toe, the machine plays by checking that if it can win this turn, it will go there, then by checking that if it can block you from
winning next turn, it will go there, if not, it will go in a random spot. In a future update, I may make a more sophisticated algorithm for the machine play.

For Wordle, the machine plays by first taking a random guess, then uses the response to build a regex to search through all possible answers, and picks a random
word from the remaining possible answers. It repeats this process, and continuously improves the regex until it finds the word, or gets six wrong guesses 
(it has never lost yet). Note: I didn't write the Wordle model myself; I got it from a friend. But I did create the algorithm for Wordle's automated play.
 
