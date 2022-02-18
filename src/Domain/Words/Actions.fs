module Domain.Words.Actions

open Domain.Words.Game
open Domain.Words

type GetActiveGames = unit -> Async<Game list>
type StoreNewGame = unit -> Async<Game>

let startNewGame (getActiveGames: GetActiveGames) (storeNewGame: StoreNewGame) : StartNewGame =
    fun () ->
        async {
            let! activeGames = getActiveGames ()

            match activeGames with
            | [] ->
                let! game = storeNewGame ()

                return Ok({ GameId = game.Id })

            | _ -> return Error(GameLimitReached { Message = "Game limit has been reached." })
        }
