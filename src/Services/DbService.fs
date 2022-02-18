namespace Services

open Npgsql.FSharp

module DbService =
    open Domain.Words.Game
    open System

    let getActiveGames connectionString () =
        connectionString
        |> Sql.connect
        |> Sql.query "SELECT id, created_at, answer from public.games WHERE status = 'Active'"
        |> Sql.executeAsync (fun read ->
            { Id = GameId(read.uuid "id")
              CreatedAt = read.dateTime "created_at"
              Status = Active
              Answer = Answer(read.string "answer") })
        |> Async.AwaitTask

    let storeNewGame connectionString answer =
        let id = Guid.NewGuid()

        let game =
            { Id = GameId(id)
              CreatedAt = DateTime.UtcNow
              Status = Active
              Answer = answer }

        connectionString
        |> Sql.connect
        |> Sql.query
            "INSERT INTO public.games (id, created_at, status, answer) VALUES (@id, @created_at, @status, @answer)"
        |> Sql.parameters [ "id", Sql.uuid id
                            "created_at", Sql.timestamp game.CreatedAt
                            "status", Sql.string "Active"
                            "answer", Sql.string (Answer.value game.Answer) ]
        |> Sql.executeNonQuery
        |> ignore

        async { return game }
