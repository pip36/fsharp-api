namespace WebApi

module CompositionRoot =
    open Domain.Words
    open Services

    let connectionString =
        "Host=localhost; Database=testdb; Username=postgres; Password=postgres; Port=5438"

    let getActiveGames =
        DbService.getActiveGames (connectionString)

    let storeNewGame =
        DbService.storeNewGame (connectionString)

    let startNewGame =
        Actions.startNewGame getActiveGames storeNewGame

    type GameControllerDependencies = { StartNewGame: StartNewGame }
