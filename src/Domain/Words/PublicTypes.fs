namespace Domain.Words

open Domain.Util
open Domain.Words.Game

// Create New Game //
////////////////////
type GameStarted = { GameId: GameId }

type GameLimitReached = { Message: string }

type StartNewGameError = GameLimitReached of GameLimitReached

type StartNewGame = unit -> AsyncResult<GameStarted, StartNewGameError>
