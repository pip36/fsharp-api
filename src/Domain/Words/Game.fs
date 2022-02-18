namespace Domain.Words.Game

open System

type GameId = GameId of Guid

module GameId =
    let stringValue (GameId id) = id.ToString()

type GameStatus =
    | Active
    | Finished

type Answer = Answer of string

module Answer =
    let value (Answer id) = id.ToString()

type Game =
    { Id: GameId
      CreatedAt: DateTime
      Status: GameStatus
      Answer: Answer }
