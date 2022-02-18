module Domain.Words.Actions

open Domain.Words.Game
open Domain.Words

type GetActiveGames = unit -> Async<Game list>
type GenerateAnswer = unit -> Async<Answer>
type StoreNewGame = Answer -> Async<Game>

let startNewGame
    (getActiveGames: GetActiveGames)
    (generateAnswer: GenerateAnswer)
    (storeNewGame: StoreNewGame)
    : StartNewGame =
    fun () ->
        async {
            let! activeGames = getActiveGames ()

            match activeGames with
            | [] ->
                let! answer = generateAnswer ()
                let! game = storeNewGame answer

                return Ok({ GameId = game.Id })

            | _ -> return Error(GameLimitReached { Message = "Game limit has been reached." })
        }
