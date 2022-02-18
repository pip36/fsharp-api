namespace WebApi.Controllers

open Microsoft.AspNetCore.Mvc
open Domain.Words
open WebApi.CompositionRoot
open Domain.Words.Game

type StartNewGameResponse = { GameId: string }

[<ApiController>]
[<Route("[controller]")>]
type GameController(deps: GameControllerDependencies) =
    inherit ControllerBase()

    [<HttpPost>]
    member x.StartNewGame() =
        async {
            let! result = deps.StartNewGame()

            return
                match result with
                | Ok (gameStarted) -> x.Ok { GameId = GameId.stringValue gameStarted.GameId } :> IActionResult
                | Error (e) ->
                    match e with
                    | GameLimitReached (e) -> x.BadRequest(e.Message) :> IActionResult
        }
