namespace Services

open Npgsql.FSharp

module DbService =
    open Domain.Words.Game
    open System

    let getActiveGames connectionString () =
        connectionString
        |> Sql.connect
        |> Sql.query "SELECT id, created_at from public.games WHERE status = 'Active'"
        |> Sql.executeAsync (fun read ->
            { Id = GameId(read.uuid "id")
              CreatedAt = read.dateTime "created_at"
              Status = Active })
        |> Async.AwaitTask

    let x = Guid.NewGuid()

    let storeNewGame connectionString () =
        let id = Guid.NewGuid()

        let game =
            { Id = GameId(id)
              CreatedAt = DateTime.UtcNow
              Status = Active }

        connectionString
        |> Sql.connect
        |> Sql.query "INSERT INTO public.games (id, created_at, status) VALUES (@id, @created_at, @status)"
        |> Sql.parameters [ "id", Sql.uuid id
                            "created_at", Sql.timestamp game.CreatedAt
                            "status", Sql.string "Active" ]
        |> Sql.executeNonQuery
        |> ignore

        async { return game }
