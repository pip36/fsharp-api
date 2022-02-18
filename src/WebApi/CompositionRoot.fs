namespace WebApi

module CompositionRoot =
    open Domain.Words
    open Services
    open Microsoft.Extensions.DependencyInjection
    open Microsoft.Extensions.Configuration

    type GameDependencies = { StartNewGame: StartNewGame }

    let AddGameDependencies (services: IServiceCollection, configuration: IConfiguration) =
        let connectionString =
            configuration.GetConnectionString("Games")

        let getActiveGames =
            DbService.getActiveGames (connectionString)

        let generateAnswer = DictionaryService.getWord

        let storeNewGame =
            DbService.storeNewGame (connectionString)

        let startNewGame =
            Actions.startNewGame getActiveGames generateAnswer storeNewGame

        services.AddSingleton<GameDependencies>({ StartNewGame = startNewGame })
